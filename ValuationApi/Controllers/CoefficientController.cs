using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ValuationApi.Models;
using ValuationApi.Repository;

namespace ValuationApi.Controllers;

/// <summary>
/// Coefficients controller class
/// </summary>
[ApiController]
[Tags("Admin Operations")]
[Route("[controller]")]
public class CoefficientController : ControllerBase
{
    private readonly ILogger<CoefficientController> _logger;
    private readonly IMemoryCache _memoryCache;
    private readonly ICoefficientRepository _coefficientRepository;
    private readonly IValuationRepository _valuationRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="coefficientRepository"></param>
    public CoefficientController(ILogger<CoefficientController> logger, IMemoryCache memoryCache, ICoefficientRepository coefficientRepository, IValuationRepository valuationRepository)
    {
        _logger = logger;
        _memoryCache = memoryCache;
        _coefficientRepository = coefficientRepository;
        _valuationRepository = valuationRepository;
    }

    /// <summary>
    /// Save coefficients to db. If there is others, they will be deleted
    /// </summary>
    /// <param name="coefficients"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName("AddCoefficients")]
    public ActionResult Post(List<Coefficient> coefficients)
    {
        try
        {
            _coefficientRepository.AddCoefficients(coefficients);

            // Clearing the memory
            if (_memoryCache is MemoryCache cache)
            {
                cache.Clear();
            }

            // Inactivate all valuations in db
            _valuationRepository.InActivateAllValuations();

            return Ok();
        }
        catch (Exception ex)
        {
            const string msg = "Error while adding the coefficients.";
            _logger.LogError(ex, msg);
            return StatusCode((int)HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Gets all coefficients from db
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ActionName("GetCoefficients")]
    public ActionResult<List<Coefficient>> Get()
    {
        try
        {
            return Ok(_coefficientRepository.GetAllCoefficients());
        }
        catch (Exception ex)
        {
            const string msg = "Error while getting the coefficients.";
            _logger.LogError(ex, msg);
            return StatusCode((int)HttpStatusCode.InternalServerError, ex);
        }
    }
}
