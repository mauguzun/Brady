using NetArchTest.Rules;

namespace Brady.ArchTests
{
    public class InfrastructureTests : LayerNames
    {
        [Fact]
        public void Infrastructure_ShouldHaveDependencyOn_ApplicationAssembly()
        {
            TestResult result = Types.InAssembly(InfrastructureAssembly)
             .Should()
             .NotHaveDependencyOnAny([ApplicationAssembly.GetName().Name!, PresentationAssembly.GetName().Name!])
             .GetResult();

            Assert.True(result.IsSuccessful, "Wrong layer dependecy");
        }

        [Fact]
        public void Infrastructure_BaseRepository_ShouldBePublc()
        {
            TestResult result = Types.InAssembly(InfrastructureAssembly)
             .That()
             .AreGeneric()
             .Should()
             .BePublic()
             .GetResult();


            Assert.True(result.IsSuccessful, "BaseRepository should be public");
        }
    }
}
