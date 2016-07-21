using System;
using NUnit.Framework;

namespace EnergyTrading.Logging.WebApiRestClient.Tests
{
    [TestFixture]
    public class LoggerGatewayFixture
    {
        private class TestLoggerGateway : LoggerGateway
        {
            public TestLoggerGateway(string baseUri) : base(baseUri)
            {
            }

            public string GetServiceUri()
            {
                return ServiceUri;
            }
        }

        [Test]
        public void Construction()
        {
            Assert.That(() => new LoggerGateway(null), Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(() => new LoggerGateway("anything"), Throws.TypeOf<UriFormatException>());
            var candidate = new LoggerGateway("http://baseUri");
            Assert.That(candidate, Is.Not.Null);
        }

        [Test]
        public void CorrectServiceUri()
        {
            var candidate = new TestLoggerGateway("http://baseuri/path").GetServiceUri();
            Assert.That(candidate, Is.EqualTo("http://baseuri/path/api/logs"));
            candidate = new TestLoggerGateway("http://baseuri/path/").GetServiceUri();
            Assert.That(candidate, Is.EqualTo("http://baseuri/path/api/logs"));
        }
    }
}