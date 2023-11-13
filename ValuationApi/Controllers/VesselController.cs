using System;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ValuationApi.Helper;
using ValuationApi.Models;
using ValuationApi.Repository;

namespace ValuationApi.Controllers;

/// <summary>
/// Vessel controller class
/// </summary>
[ApiController]
[Tags("Admin Operations")]
[Route("[controller]")]
public class VesselController : ControllerBase
{
    private readonly ILogger<VesselController> _logger;
    private readonly IVesselRepository _vesselRepository;
    private readonly IValidator<Vessel> _vesselValidator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="vesselRepository"></param>
    public VesselController(ILogger<VesselController> logger, IVesselRepository vesselRepository, IValidator<Vessel> vesselValidator)
    {
        _logger = logger;
        _vesselRepository = vesselRepository;
        _vesselValidator = vesselValidator;
    }

    /// <summary>
    /// Save vessel to db
    /// </summary>
    /// <param name="vessel"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName("AddVessel")]
    public ActionResult Post(Vessel vessel)
    {
        try
        {
            var valdationResult = _vesselValidator.Validate(vessel);

            if (!valdationResult.IsValid)
            {
                return BadRequest(valdationResult.Errors);
            }

            // Check if there is a vessel with same IMO
            var existingVessel = _vesselRepository.GetVessel(vessel.IMO);
            if (existingVessel != null && !string.IsNullOrEmpty(existingVessel.IMO))
            {
                return BadRequest($"There is an record with {vessel.IMO} IMO");
            }

            _vesselRepository.AddVessel(vessel);

            return Ok();
        }
        catch (Exception ex)
        {
            const string msg = "Error while adding the vessel.";
            _logger.LogError(ex, msg);
            return StatusCode((int)HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Update vessel
    /// </summary>
    /// <param name="vessel"></param>
    /// <returns></returns>
    [HttpPut]
    [ActionName("UpdateVessel")]
    public ActionResult Put(Vessel vessel)
    {
        try
        {
            var valdationResult = _vesselValidator.Validate(vessel);

            if (!valdationResult.IsValid)
            {
                return BadRequest(valdationResult.Errors);
            }

            _vesselRepository.Update(vessel);

            return Ok();
        }
        catch (Exception ex)
        {
            const string msg = "Error while updating the vessel.";
            _logger.LogError(ex, msg);
            return StatusCode((int)HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Gets all vessels from db
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ActionName("GetVsssels")]
    public ActionResult<List<Vessel>> Get()
    {
        try
        {
            return Ok(_vesselRepository.GetAllVessels());
        }
        catch (Exception ex)
        {
            const string msg = "Error while getting the vessels.";
            _logger.LogError(ex, msg);
            return StatusCode((int)HttpStatusCode.InternalServerError, ex);
        }
    }
}
