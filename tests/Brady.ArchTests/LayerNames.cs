using Brady.Application.Interfaces;
using Brady.Domain.Enum;
using Brady.Infrastructure.Interfaces;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Reflection;

namespace Brady.ArchTests
{
    public abstract class LayerNames
    {
            protected static readonly Assembly ApplicationAssembly = typeof(ICalculationService).Assembly;

            protected static readonly Assembly DomainAssembly = typeof(GenerationType).Assembly;

            protected static readonly Assembly InfrastructureAssembly = typeof(IReferenceDataRepository).Assembly;

            protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
        }

  
}