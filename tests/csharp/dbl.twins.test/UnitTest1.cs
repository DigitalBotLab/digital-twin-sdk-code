using dbl.twins.consumer;

namespace dbl.twins.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestColorTempBehaviourCold()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour();
            ctb.TelemetryUpdate(60.0);

            Assert.IsTrue(ctb.TempColor.R == 0);
            Assert.IsTrue(ctb.TempColor.G == 0);
            Assert.IsTrue(ctb.TempColor.B == 255);
            Assert.IsTrue(ctb.TempColor.A == 1);

        }

        [TestMethod]
        public void TestColorTempBehaviourNormal()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour();
            ctb.TelemetryUpdate(75.0);

            Assert.IsTrue(ctb.TempColor.R == 0);
            Assert.IsTrue(ctb.TempColor.G == 255);
            Assert.IsTrue(ctb.TempColor.B == 0);
            Assert.IsTrue(ctb.TempColor.A == 1);
        }

        [TestMethod]
        public void TestColorTempBehaviourHot()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour();
            ctb.TelemetryUpdate(90.0);

            Assert.IsTrue(ctb.TempColor.R == 255);
            Assert.IsTrue(ctb.TempColor.G == 0);
            Assert.IsTrue(ctb.TempColor.B == 0);
            Assert.IsTrue(ctb.TempColor.A == 1);
        }

    }
}