using dbl.twins.consumer;
using dbl.twins.sdk;

namespace dbl.twins.test
{
    [TestClass]
    public class ColorTempTests
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
        public void TestColorTempBehaviourTooCold()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour();
            ctb.TelemetryUpdate(59.0);

            Assert.IsTrue(ctb.TempColor.R == 0);
            Assert.IsTrue(ctb.TempColor.G == 0);
            Assert.IsTrue(ctb.TempColor.B == 255);
            Assert.IsTrue(ctb.TempColor.A == 1);

        }

        [TestMethod]
        public void TestColorTempBehaviourFreezingCold()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour();
            ctb.TelemetryUpdate(32.0);
            
            Assert.IsTrue(ctb.TempColor.R == 0);
            Assert.IsTrue(ctb.TempColor.G == 0);
            Assert.IsTrue(ctb.TempColor.B == 255);
            Assert.IsTrue(ctb.TempColor.A == 1);

        }


        [TestMethod]
        public void TestColorTempBehaviourIceAge()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour();
            ctb.TelemetryUpdate(-32.0);

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
        public void TestColorTempBehaviourHotter()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour();
            ctb.TelemetryUpdate(85.0);

            Assert.IsTrue(ctb.TempColor.R == 255);
            Assert.IsTrue(ctb.TempColor.G == 170);
            Assert.IsTrue(ctb.TempColor.B == 0);
            Assert.IsTrue(ctb.TempColor.A == 1);
        }

        [TestMethod]
        public void TestColorTempBehaviourAlmostHot()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour();
            ctb.TelemetryUpdate(88.0);

            Assert.IsTrue(ctb.TempColor.R == 255);
            Assert.IsTrue(ctb.TempColor.G == 68);
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

        [TestMethod]
        public void TestColorTempBehaviourBoiling()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour();
            ctb.TelemetryUpdate(212.0);

            Assert.IsTrue(ctb.TempColor.R == 255);
            Assert.IsTrue(ctb.TempColor.G == 0);
            Assert.IsTrue(ctb.TempColor.B == 0);
            Assert.IsTrue(ctb.TempColor.A == 1);
        }

        [TestMethod]
        public void TestColorTempBehaviourTooHot()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour();
            ctb.TelemetryUpdate(1000.0);

            Assert.IsTrue(ctb.TempColor.R == 255);
            Assert.IsTrue(ctb.TempColor.G == 0);
            Assert.IsTrue(ctb.TempColor.B == 0);
            Assert.IsTrue(ctb.TempColor.A == 1);
        }
    }
}