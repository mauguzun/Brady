﻿using Brady.Application.Interfaces;
using Brady.Domain.Models.Output;
using Brady.Domain.Options;
using Brady.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace Brady.Application.Services
{
    public class GenerateReportService : IGenerateReportService
    {
        private readonly ICalculationService _calculationService;
        private readonly IGenerationReportRepository _generationReportRepository;
        private readonly IGenerationOutputRepository _generationOutputRepository;

        private ApplicationOptions _options;

        public GenerateReportService(
            ICalculationService calculationService, 
            IGenerationReportRepository generationReportRepository,
            IGenerationOutputRepository generationOutputRepository,
            IOptions<ApplicationOptions> options)
        {
            _calculationService = calculationService;
            _generationReportRepository = generationReportRepository;
            _generationOutputRepository = generationOutputRepository;
            _options = options.Value;

        }

        public bool CreateReport()
        {
            string ?filename = Directory.GetFiles(_options.InputFolder).FirstOrDefault();
            if (filename is null)
                return false;

            var reportData = _generationReportRepository.LoadXml(filename);

            if (reportData is null)
                return false;

            var generationOutput = new GenerationOutput
            {
                Totals = _calculationService.Totals(reportData),
                MaxEmissionGenerators = _calculationService.MaxEmissionGenerators(reportData),
                ActualHeatRates = _calculationService.ActualHeatRates(reportData)
            };

            _generationOutputRepository.CreateXml(generationOutput, Path.Combine(_options.OutputFolder, "report.xml"));
            _generationReportRepository.DeleteXml(filename);

            return true;
        }
    }
}