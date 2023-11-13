using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ValuationApi.Helper;
using ValuationApi.Models;
using ValuationApi.Repository;

namespace ValuationApi.Controllers;

/// <summary>
/// Valuation controller class
/// </summary>
[ApiController]
[Route("[controller]")]
public class ValuationController : ControllerBase
{
    private const string CACHEKEY = "Valuation";
    private readonly ILogger<ValuationController> _logger;
    private readonly IMemoryCache _memoryCache;
    private readonly IVesselRepository _vesselRepository;
    private readonly IValuationRepository _valuationRepository;
    private readonly ICoefficientRepository _coefficientRepository;

    private List<Valuation> valuations;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="memoryCache"></param>
    /// <param name="vesselRepository"></param>
    /// <param name="valuationRepository"></param>
    /// <param name="coefficientRepository"></param>
    public ValuationController(ILogger<ValuationController> logger, IMemoryCache memoryCache, IVesselRepository vesselRepository, IValuationRepository valuationRepository, ICoefficientRepository coefficientRepository)
    {
        _logger = logger;
        _memoryCache = memoryCache;
        _vesselRepository = vesselRepository;
        _valuationRepository = valuationRepository;
        _coefficientRepository = coefficientRepository;
        valuations = new();
    }

    /// <summary>
    /// Valuation vessel methot.
    /// </summary>
    /// <param name="IMOIds"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName("ValuateVessels")]
    public ActionResult<List<Valuation>> Post(string[] IMONumbers)
    {
        try
        {
            if (IMONumbers == null || IMONumbers.Length == 0)
            {
                return BadRequest("IMOIds cant be null!");
            }

            // We kept the coefficient in database but also could be parameterize in a general parameter table and keep in cache it periodically
            var coefficients = _coefficientRepository.GetAllCoefficients();

            if (coefficients == null || coefficients.Count == 0)
            {
                return BadRequest("There is no any Coefficient to proceed");
            }

            // This value will return to the client
            valuations = new();

            foreach (var IMO in IMONumbers)
            {
                // We have to make database request to validate the vessel. Validations can be when storing the vessels on the database
                // but conditions can change and we can need invalid vessels data too. I dont know the system exactly.
                var vessel = _vesselRepository.GetVessel(IMO);

                if (vessel == null)
                {
                    return BadRequest($"There is no vessel with {IMO} IMO number.");
                }

                // Check the cache with IMOId
                if (_memoryCache.TryGetValue(IMO, out List<Valuation>? cachedValuations))
                {
                    // Check if any coefficients changed
                    if (Validations.IsAnyCoeficientChanged(cachedValuations, coefficients))
                    {
                        valuate(vessel, coefficients);
                    }
                    else
                    {
                        valuations.AddRange(cachedValuations);
                    }
                }
                else // If there is no value in the cache, check the database 
                {
                    var existingValuation = _valuationRepository.GetValuations(IMO);

                    // No valuation in database. Run valuation and keep on database and cache 
                    if (existingValuation == null || existingValuation.Count == 0)
                    {
                        valuate(vessel, coefficients);
                    }
                    else // Valuation is only on the database. Keep on cache too.
                    {
                        // Check if any coefficients changed
                        if (Validations.IsAnyCoeficientChanged(existingValuation, coefficients))
                        {
                            valuate(vessel, coefficients);
                        }
                        else
                        {
                            cahcheTheValuations(IMO, existingValuation);
                            valuations.AddRange(existingValuation);
                        }
                    }
                }
            }

            return Ok(valuations);
        }
        catch (Exception ex)
        {
            const string msg = "Error while executing the valuation proccess.";
            _logger.LogError(ex, msg);
            return StatusCode((int)HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Get all valuations
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ActionName("GetValuations")]
    public ActionResult<List<Valuation>> Get()
    {
        try
        {
            return Ok(_valuationRepository.GetAllValuations());
        }
        catch (Exception ex)
        {
            const string msg = "Error while getting the valuations.";
            _logger.LogError(ex, msg);
            return StatusCode((int)HttpStatusCode.InternalServerError, ex);
        }
    }

    private void valuate(Vessel vessel, List<Coefficient> coefficients)
    {
        var newValuations = ValuationProcessor.Valuate(coefficients, vessel);
        _valuationRepository.AddValuation(newValuations);

        cahcheTheValuations(vessel.IMO, newValuations);

        valuations.AddRange(newValuations);
    }

    private void cahcheTheValuations(string IMO, List<Valuation> valuations)
    {
        // Set cache options
        var cacheExpOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(10),// 10 minutes
            Priority = CacheItemPriority.Normal
        };

        _memoryCache.Set(IMO, valuations, cacheExpOptions);
    }
}
