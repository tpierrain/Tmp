using Diverse;

namespace TrainTrain.Tests
{
    [SetUpFixture]
    public class AllTestFixtures
    {
        [OneTimeSetUp]
        public void Init()
        {
            Fuzzer.Log = TestContext.WriteLine;
        }
    }
}
