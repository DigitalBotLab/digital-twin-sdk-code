using dbl.twins.sdk;
using dbl.twins.consumer;

namespace dbl.twins.test
{
    [TestClass]
    public class ColorTempTests
    {
        [TestMethod]
        public void TestColorTempBehaviourCold()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour("", "Temperature");
            var dict = new KeyValuePair<string, string>("Temperature", "60.0");
            ctb.TelemetryUpdate(dict);

            Assert.IsTrue(ctb.TempColor.R == 0);
            Assert.IsTrue(ctb.TempColor.G == 0);
            Assert.IsTrue(ctb.TempColor.B == 255);
            Assert.IsTrue(ctb.TempColor.A == 1);

        }

        [TestMethod]
        public void TestColorTempBehaviourTooCold()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour("", "Temperature");
            var dict = new KeyValuePair<string, string>("Temperature", "59.0");
            ctb.TelemetryUpdate(dict);

            Assert.IsTrue(ctb.TempColor.R == 0);
            Assert.IsTrue(ctb.TempColor.G == 0);
            Assert.IsTrue(ctb.TempColor.B == 255);
            Assert.IsTrue(ctb.TempColor.A == 1);

        }

        [TestMethod]
        public void TestColorTempBehaviourFreezingCold()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour("", "Temperature");
            var dict = new KeyValuePair<string, string>("Temperature", "32.0");
            ctb.TelemetryUpdate(dict);


            Assert.IsTrue(ctb.TempColor.R == 0);
            Assert.IsTrue(ctb.TempColor.G == 0);
            Assert.IsTrue(ctb.TempColor.B == 255);
            Assert.IsTrue(ctb.TempColor.A == 1);

        }


        [TestMethod]
        public void TestColorTempBehaviourIceAge()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour("", "Temperature");
            var dict = new KeyValuePair<string, string>("Temperature", "-32.0");
            ctb.TelemetryUpdate(dict);

            Assert.IsTrue(ctb.TempColor.R == 0);
            Assert.IsTrue(ctb.TempColor.G == 0);
            Assert.IsTrue(ctb.TempColor.B == 255);
            Assert.IsTrue(ctb.TempColor.A == 1);
        }

        [TestMethod]
        public void TestColorTempBehaviourNormal()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour("", "Temperature");
            var dict = new KeyValuePair<string, string>("Temperature", "75.0");
            ctb.TelemetryUpdate(dict);

            Assert.IsTrue(ctb.TempColor.R == 0);
            Assert.IsTrue(ctb.TempColor.G == 255);
            Assert.IsTrue(ctb.TempColor.B == 0);
            Assert.IsTrue(ctb.TempColor.A == 1);
        }

        [TestMethod]
        public void TestColorTempBehaviourHotter()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour("", "Temperature");
            var dict = new KeyValuePair<string, string>("Temperature", "85.0");
            ctb.TelemetryUpdate(dict);

            Assert.IsTrue(ctb.TempColor.R == 255);
            Assert.IsTrue(ctb.TempColor.G == 170);
            Assert.IsTrue(ctb.TempColor.B == 0);
            Assert.IsTrue(ctb.TempColor.A == 1);
        }

        [TestMethod]
        public void TestColorTempBehaviourAlmostHot()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour("", "Temperature");
            var dict = new KeyValuePair<string, string>("Temperature", "88.0");
            ctb.TelemetryUpdate(dict);

            Assert.IsTrue(ctb.TempColor.R == 255);
            Assert.IsTrue(ctb.TempColor.G == 68);
            Assert.IsTrue(ctb.TempColor.B == 0);
            Assert.IsTrue(ctb.TempColor.A == 1);
        }

        [TestMethod]
        public void TestColorTempBehaviourHot()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour("", "Temperature");
            var dict = new KeyValuePair<string, string>("Temperature", "90.0");

            ctb.TelemetryUpdate(dict);

            Assert.IsTrue(ctb.TempColor.R == 255);
            Assert.IsTrue(ctb.TempColor.G == 0);
            Assert.IsTrue(ctb.TempColor.B == 0);
            Assert.IsTrue(ctb.TempColor.A == 1);
        }

        [TestMethod]
        public void TestColorTempBehaviourBoiling()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour("", "Temperature");
            var dict = new KeyValuePair<string, string>("Temperature", "212.0");

            ctb.TelemetryUpdate(dict);

            Assert.IsTrue(ctb.TempColor.R == 255);
            Assert.IsTrue(ctb.TempColor.G == 0);
            Assert.IsTrue(ctb.TempColor.B == 0);
            Assert.IsTrue(ctb.TempColor.A == 1);
        }

        [TestMethod]
        public void TestColorTempBehaviourTooHot()
        {
            ColorTempBehaviour ctb = new ColorTempBehaviour("", "Temperature");
            var dict = new KeyValuePair<string, string>("Temperature", "1000.0");
            ctb.TelemetryUpdate(dict);

            Assert.IsTrue(ctb.TempColor.R == 255);
            Assert.IsTrue(ctb.TempColor.G == 0);
            Assert.IsTrue(ctb.TempColor.B == 0);
            Assert.IsTrue(ctb.TempColor.A == 1);
        }
    }
}