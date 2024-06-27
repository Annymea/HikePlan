namespace HikePlanTest
{
    public class Test_FindOptimalMaxPathLenth
    {

        [Test]
        public void CorrectOptimalPaxPathIsFound()
        {
            HikingPlan hikingPlan = new HikingPlan()
            {
                days = 2,
                givenPathNumber = 4,
                givenPaths = [10,20,30,40]
            };

            int optimalMaxPathLength = OptimizeHikePath.FindOptimalMaxPathLength(hikingPlan);

            Assert.AreEqual(60, optimalMaxPathLength);
        }

        [Test]
        public void CorrectOptimalPaxPathIsFoundWithHugeField()
        {
            int[] longPath = new int[1000];
            for (int i = 0; i < longPath.Length; i++) { 
                longPath[i] = i + 1;
            }

            HikingPlan hikingPlan = new HikingPlan()
            {
                days = 500,
                givenPathNumber = longPath.Length,
                givenPaths = longPath
            };

            int optimalMaxPathLength = OptimizeHikePath.FindOptimalMaxPathLength(hikingPlan);

            Assert.AreEqual(1407, optimalMaxPathLength);
        }

        [Test]
        public void ReturnsIfPlanIsNotValid()
        {
            HikingPlan hikingPlan = new HikingPlan()
            {
                days = 0,
                givenPathNumber = 4,
                givenPaths = [10, 20, 30, 40]
            };

            int optimalMaxPathLength = OptimizeHikePath.FindOptimalMaxPathLength(hikingPlan);

            Assert.AreEqual(0, optimalMaxPathLength);
        }
    }
}