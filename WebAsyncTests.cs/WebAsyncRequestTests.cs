using System;
using Assets._1_Solution.Scripts.ArduinoManager.Helper;
using NUnit.Framework;
using System.Threading;
using Helper;
using System.Runtime.Remoting.Messaging;

namespace WebAsyncTests
{
    [TestFixture]
    public class WebAsyncRequestTests
    {     
        [Test]
        public void AsyncController_GetAsyncResponse_Power_DontWaitForResponse_ValueIsTheSame()
        {
            AsyncController ac = new AsyncController();
            ac.StartAsyncRequestForPowerSensor();

            Assert.AreEqual(null, ac.SensorValue);
        }

        [Test]
        public void AsyncController_GetAsyncResponse_Power_WaitForResponse_ValueIsTheSame()
        {
            AsyncController ac = new AsyncController();
            ac.StartAsyncRequestForPowerSensor();

            //wait async method while retrive value
            Thread.Sleep(20000);

            Assert.AreEqual("1", ac.SensorValue);
        }
    }
}
