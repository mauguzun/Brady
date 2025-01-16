using Brady.Application.Interfaces;
using Brady.Application.Services;
using Brady.Domain.Options;
using Brady.Infrastructure.Interfaces;
using Brady.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


const string sectionName = "ApplicationOptions";

var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
           .Build();


var services = new ServiceCollection();
services.Configure<ApplicationOptions>(opt => configuration.GetSection(sectionName).Bind(opt));

// repositories
services.AddSingleton<IReferenceDataRepository, ReferenceDataRepository>();
services.AddTransient<IGenerationOutputRepository, GenerationOutputRepository>();
services.AddTransient<IGenerationReportRepository, GenerationReportRepository>();

// services
services.AddTransient<IFactorService, FactorService>();
services.AddTransient<IGenerateReportService, GenerateReportService>();
services.AddTransient<ICalculationService, CalculationService>();



IServiceProvider build = services.BuildServiceProvider();

var reportService = build.GetRequiredService<IGenerateReportService>();
int reportSettings = configuration.GetSection(sectionName).Get<ApplicationOptions>()!.Timeout;


while (true)
{
    Console.WriteLine(reportService.CreateReport() ? "Created new report" : "Awaiting file...");
    await Task.Delay(reportSettings);
}