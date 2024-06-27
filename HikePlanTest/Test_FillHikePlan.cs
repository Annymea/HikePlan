namespace HikePlanTest
{
    internal class Test_FillHikePlan
    {
        [Test]
        public void HikePlanIsFilledCorrectly() 
        {
            HikingPlan hikingPlan = new HikingPlan()
            {
                days = 2,
                givenPathNumber = 4,
                givenPaths = [10, 20, 30, 40]
            };

            hikingPlan = OptimizeHikePath.FillHikePlan(60, hikingPlan);

            int[] expectedResult = [60,40];

            Assert.AreEqual(expectedResult, hikingPlan.plannedPaths);
        }

        [Test]
        public void HikePlanIsNotFilledIfOptimalPathIsInvalid()
        {
            HikingPlan hikingPlan = new HikingPlan()
            {
                days = 2,
                givenPathNumber = 4,
                givenPaths = [10, 20, 30, 40]
            };

            hikingPlan = OptimizeHikePath.FillHikePlan(0, hikingPlan);

            int[] expectedResult = [];

            Assert.AreEqual(expectedResult, hikingPlan.plannedPaths);
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

            hikingPlan = OptimizeHikePath.FillHikePlan(0, hikingPlan);

            int[] expectedResult = [];

            Assert.AreEqual(expectedResult, hikingPlan.plannedPaths);
        }
    }
}
