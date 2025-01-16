using Brady.Application.Interfaces;
using Brady.Application.Services;
using Brady.Domain.Options;
using Brady.Infrastructure.Interfaces;
using Brady.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
           .Build();


var services = new ServiceCollection();


services.Configure<ApplicationOptions>(opt => configuration.GetSection("FilePaths").Bind(opt));

services.AddSingleton<IReferenceDataRepository, ReferenceDataRepository>();
services.AddTransient<IGenerationOutputRepository, GenerationOutputRepository>();
services.AddTransient<IGenerationReportRepository, GenerationReportRepository>();

services.AddTransient<ICalculationService, CalculationService>();
services.AddTransient<IGenerateReportService, GenerateReportService>();



IServiceProvider build = services.BuildServiceProvider();

var reportService = build.GetRequiredService<IGenerateReportService>();
int reportSettings = configuration.GetSection("FilePaths").Get<ApplicationOptions>()!.Timeout;


while (true)
{
    if(reportService.CreateReport())
    {
        Console.WriteLine("Created new report");
    }
    await Task.Delay(reportSettings);
}