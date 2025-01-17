using Brady.Application.Interfaces;
using Brady.Application.Services;
using Brady.Domain.Enum;
using Brady.Domain.Models.Generators;
using Brady.Domain.Models.Generators.Types.Coal;
using Brady.Domain.Models.Generators.Types.Gas;
using Brady.Domain.Models.Generators.Types.Wind;
using Moq;

namespace Brady.Services
{
    public class CalculationServiceTests
    {
        private readonly DateTime _date = DateTime.UtcNow.Date;
        private const string _firstWindGeneration = "FirstWndGeneration";
        private const string _secondWindGeneration = "SecondWindGeneraton";
        private const string _coalGeneration = "CoalGeneration";
        private const string _gasGenerator = "GasGeneration";

        [Fact]
        public void Totals_ShouldBeValid()
        {
            // arrange
            Mock<IFactorService> factoryMoq = FactoryMoqService();
            var targetService = new CalculationService(factoryMoq.Object);
            // act
            var generationReports = GenerationReport();
            var testResults = targetService.Totals(generationReports);

            //arrange
            Assert.Equal(4, testResults.Count);
            Assert.Equal(8, testResults.Where(x => x.Name == _firstWindGeneration).First().Total);
            Assert.Equal(16, testResults.Where(x => x.Name == _secondWindGeneration).First().Total);
            Assert.Equal(40, testResults.Where(x => x.Name == _gasGenerator).First().Total);
            Assert.Equal(66, testResults.Where(x => x.Name == _coalGeneration).First().Total);
        }

        [Fact]
        public void ActualHeatRates_ShouldBeValid()
        {
            // arrange
            Mock<IFactorService> factoryMoq = FactoryMoqService();
            var targetService = new CalculationService(factoryMoq.Object);
            // act
            var generationReports = GenerationReport();
            var testResults = targetService.ActualHeatRates(generationReports).First();

            //arrange
            Assert.Equal(2, testResults.HeatRate);
            Assert.Equal(_coalGeneration, testResults.Name);
        }

        [Fact]
        public void MaxEmissionGenerators_ShouldBeValid()
        {
            // arrange
            Mock<IFactorService> factoryMoq = FactoryMoqService();
            var targetService = new CalculationService(factoryMoq.Object);
            // act
            var generationReports = GenerationReport();
            var testResults = targetService.MaxEmissionGenerators(generationReports);

            //arrange
            Assert.Equal(_coalGeneration, testResults[0].Name);
            Assert.Equal(32, testResults[0].Emission);
            Assert.Equal(_date.AddDays(1), testResults[0].Date); 
            
            Assert.Equal(_gasGenerator, testResults[1].Name);
            Assert.Equal(16, testResults[1].Emission);
            Assert.Equal(_date.AddDays(2), testResults[1].Date);    
            
            Assert.Equal(_gasGenerator, testResults[2].Name);
            Assert.Equal(8, testResults[2].Emission);
            Assert.Equal(_date, testResults[2].Date);
         
        }


        private static Mock<IFactorService> FactoryMoqService()
        {
            var mockMovieService = new Mock<IFactorService>();
            mockMovieService.Setup(s => s.ValueFactor(GenerationType.Wind, GenerationLocation.Offshore)).Returns(1);
            mockMovieService.Setup(s => s.ValueFactor(GenerationType.Wind, GenerationLocation.Onshore)).Returns(2);  
            mockMovieService.Setup(s => s.ValueFactor(GenerationType.Coal, default)).Returns(1);
            mockMovieService.Setup(s => s.ValueFactor(GenerationType.Gas, default)).Returns(2);
            mockMovieService.Setup(s => s.EmissionFactor(GenerationType.Coal)).Returns(2);
            mockMovieService.Setup(s => s.EmissionFactor(GenerationType.Gas)).Returns(2);
            return mockMovieService;
        }

        private GenerationReport GenerationReport()
        {
            return new GenerationReport
            {
                Wind = new List<WindGenerator>
               {
                   new WindGenerator
                   {
                        Location = GenerationLocation.Offshore.ToString(),
                        Name  = _firstWindGeneration,
                        Generation = new List<Day>
                        {
                            Day(1,2,_date),Day(2,3,_date.AddDays(1)),
                        }
                   },
                   new WindGenerator
                   {
                        Location = GenerationLocation.Onshore.ToString(),
                        Name  = _secondWindGeneration,
                        Generation = new List<Day>
                        {
                            Day(1,2,_date), Day(2,3,_date.AddDays(2)),
                        }
                   }
               },
                Coal = new List<CoalGenerator>()
               {
                   new CoalGenerator()
                   {
                       ActualNetGeneration =2,
                       EmissionsRating = 2,
                       TotalHeatInput = 4,
                       Name  = _coalGeneration,
                       Generation = new List<Day>()
                       {
                            Day(1,2,_date), Day(8,8,_date.AddDays(1))
                       }

                   }
               },
                Gas = new List<GasGenerator>
                {
                      new GasGenerator
                      {
                          Name = _gasGenerator,
                          EmissionsRating = 2,
                          Generation = new List<Day>()
                          {
                            Day(1,2,_date), Day(2,3,_date.AddDays(1)),Day(3,4,_date.AddDays(2))
                          }
                      }
                }
            };
        }

        private Day Day(decimal price, decimal energy, DateTime date) =>
                            new Day
                            {
                                Price = price,
                                Energy = energy,
                                Date = date
                            };

    }
}