using System;
using NUnit.Framework;
using Helper;

namespace ArduinoManagerTests
{
    [TestFixture]
    public class WebServiceTests
    {
        [Test]
        public void WebServiceRequester_GetResponse_Status_ValueIsThesame()
        { 
            WebServiceRequester requester = new WebServiceRequester();
            var response = requester.GetDeviceStatus();

           Assert.AreEqual("1", response.Trim());
        }

        [Test]
        public void WebServiceRequester_GetResponse_Motion_ValueIsThesame()
        {
            WebServiceRequester requester = new WebServiceRequester();
            var response = requester.GetMotionStatus();

            Assert.AreEqual("0", response.Trim());
        }

        [Test]
        public void WebServiceRequester_GetResponse_Light_ValueIsThesame()
        {
            WebServiceRequester requester = new WebServiceRequester();
            requester.TurnOffLight();

            var response = requester.GetLightStatus();

            Assert.AreEqual("0", response.Trim());
        }

        [Test]
        public void WebServiceRequester_GetResponse_Temperature_ValueIsThesame()
        {          
            WebServiceRequester requester = new WebServiceRequester();
            var response = Convert.ToInt16(requester.GetTemperature());

            Assert.IsTrue(response > 0 && response <= 20);
        }

        [Test]
        public void WebServiceRequester_GetResponse_Light_ON_ValueIsThesame()
        {
            WebServiceRequester requester = new WebServiceRequester();
            var response = requester.TurnOnLight();

            Assert.AreEqual("1", response.Trim());
        }

        [Test]
        public void WebServiceRequester_GetResponse_Light_OFF_ValueIsThesame()
        {
            WebServiceRequester requester = new WebServiceRequester();
            var response = requester.TurnOffLight();

            Assert.AreEqual("0", response.Trim());
        }
    }
}
