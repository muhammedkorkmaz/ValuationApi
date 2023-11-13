using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ValuationApi.Controllers;
using ValuationApi.Models;
using ValuationApi.Repository;

namespace ValuationApi.Test;

public class VesselControllerTest
{
    VesselController _controller;
    IVesselRepository _service;
    ILogger<VesselController> _logger;
    IValidator<Vessel> _vesselValidator;

    public VesselControllerTest()
    {
        var serviceProvider = new ServiceCollection()
                                .AddLogging()
                                .BuildServiceProvider();

        var factory = serviceProvider.GetService<ILoggerFactory>();

        _vesselValidator = new VesselValidator();
        _service = new VesselRepository();
        _logger = factory.CreateLogger<VesselController>();

        _controller = new VesselController(_logger, _service, _vesselValidator);

    }

    [Fact]
    public void Get()
    {
        var result = _controller.Get();

        //Assert
        Assert.IsType<OkObjectResult>(result.Result);

        var list = result.Result as OkObjectResult;

        Assert.IsType<List<Vessel>>(list.Value);

        var vessels = list.Value as List<Vessel>;

        Assert.Equal(1, vessels.Count);
    }

    [Fact]
    public void Post()
    {
        Vessel vessel = new Vessel()
        {
            IMO = "IMO1234567",
            VesselType = 0,
            YearOfBuild = 2022,
            Size = 27000
        };

        _controller.Post(vessel);
        var result = _controller.Get();

        var list = result.Result as OkObjectResult;

        var vessels = list.Value as List<Vessel>;

        Assert.Equal(1, vessels.Count);
    }

    [Fact]
    public void Put()
    {
        Vessel vessel = new Vessel()
        {
            IMO = "IMO1234567",
            VesselType = 0,
            YearOfBuild = 2022,
            Size = 27000
        };

        _controller.Put(vessel);
        var result = _controller.Get();

        var list = result.Result as OkObjectResult;

        var vessels = list.Value as List<Vessel>;

        Assert.Equal(1, vessels.Count);
    }
}
