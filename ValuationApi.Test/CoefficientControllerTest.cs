using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ValuationApi.Controllers;
using ValuationApi.Models;
using ValuationApi.Repository;

namespace ValuationApi.Test;

public class CoefficientControllerTest
{
    CoefficientController _controller;
    ILogger<CoefficientController> _logger;
    IMemoryCache _memoryCache;

    ICoefficientRepository _service;
    IValuationRepository _valuationRepository;

    public CoefficientControllerTest()
    {
        var serviceProvider = new ServiceCollection()
                                .AddLogging()
                                .AddMemoryCache()
                                .BuildServiceProvider();


        var factory = serviceProvider.GetService<ILoggerFactory>();

        _memoryCache = serviceProvider.GetService<IMemoryCache>();

        _logger = factory.CreateLogger<CoefficientController>();
        _service = new CoefficientRepository();
        _valuationRepository = new ValuationRepository();

        _controller = new CoefficientController(_logger, _memoryCache, _service, _valuationRepository);

    }

    [Fact]
    public void Get()
    {
        var result = _controller.Get();

        //Assert
        Assert.IsType<OkObjectResult>(result.Result);

        var list = result.Result as OkObjectResult;

        Assert.IsType<List<Coefficient>>(list.Value);

        var coefficients = list.Value as List<Coefficient>;

        Assert.Equal(0, coefficients.Count);
    }

    [Fact]
    public void Post()
    {
        List<Coefficient> coefficients = new()
        {
            new Coefficient()
            {
                Type="Fair Market Value",
                A=0.11,
                B=0.001,
                Constant=3
            },
            new Coefficient()
            {
                Type="Operating Costs",
                A=0.12,
                B=0.021,
                Constant=4
            },
            new Coefficient()
            {
                Type="Scrap Price",
                A=0.13,
                B=0.002,
                Constant=56
            }
        };

        _controller.Post(coefficients);
        var result = _controller.Get();

        var list = result.Result as OkObjectResult;

        var vessels = list.Value as List<Coefficient>;

        Assert.Equal(3, vessels.Count);
    }
}
