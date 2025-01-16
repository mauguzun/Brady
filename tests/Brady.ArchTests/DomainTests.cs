using NetArchTest.Rules;

namespace Brady.ArchTests
{
    public class DomainTests : LayerNames
    {
        [Fact]
        public void DomainLayer_ShouldNotHaveDependencyOn()
        {
            TestResult result = Types.InAssembly(DomainAssembly)
                .Should()
                .NotHaveDependencyOnAny([ApplicationAssembly.GetName().Name!, InfrastructureAssembly.GetName().Name! ,PresentationAssembly.GetName().Name!])
                .GetResult();

            Assert.True(result.IsSuccessful, "Wrong layer dependecy");
        }

        [Fact]
        public void Domain_Should_BePublic()
        {
            TestResult result = Types.InAssembly(DomainAssembly)
                .Should()
                .BePublic()
                .GetResult();

            Assert.True(result.IsSuccessful, "Domain classes should be public");
        }
    }
}
