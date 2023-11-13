using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ValuationApi.Controllers;
using ValuationApi.Models;
using ValuationApi.Repository;

namespace ValuationApi.Test;

public class ValuationControllerTest
{
    ValuationController _controller;
    ILogger<ValuationController> _logger;
    IMemoryCache _memoryCache;

    IValuationRepository _service;
    ICoefficientRepository _coefficientRepository;
    IVesselRepository _vesselRepository;

    public ValuationControllerTest()
    {
        var serviceProvider = new ServiceCollection()
                                .AddLogging()
                                .AddMemoryCache()
                                .BuildServiceProvider();


        var factory = serviceProvider.GetService<ILoggerFactory>();

        _memoryCache = serviceProvider.GetService<IMemoryCache>();
        _logger = factory.CreateLogger<ValuationController>();

        _service = new ValuationRepository();
        _coefficientRepository = new CoefficientRepository();
        _vesselRepository = new VesselRepository();

        _controller = new ValuationController(_logger, _memoryCache, _vesselRepository, _service, _coefficientRepository);

    }

    [Fact]
    public void Get()
    {
        var result = _controller.Get();

        //Assert
        Assert.IsType<OkObjectResult>(result.Result);

        var list = result.Result as OkObjectResult;

        Assert.IsType<List<Valuation>>(list.Value);

        var coefficients = list.Value as List<Valuation>;

        Assert.Equal(0, coefficients.Count);
    }
}
