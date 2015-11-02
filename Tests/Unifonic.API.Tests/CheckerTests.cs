using Moq;
using NUnit.Framework;
using RestSharp;

namespace Unifonic.API.IntegrationTests
{
    class CheckerTests
    {
        private Mock<UnifonicRestClient> _mockClient;

        private const string recipient = "962788888888";

        [SetUp]
        public void Setup()
        {
            _mockClient = new Mock<UnifonicRestClient>(Credentials.ApplicationSid) { CallBase = true };
        }

        [Test]
        public void ShouldNumberInsight()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<NumberInsightResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new NumberInsightResult());

            var client = _mockClient.Object;

            client.NumberInsight(recipient);

            _mockClient.Verify(orc => orc.Execute<NumberInsightResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Checker/NumberInsight", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(recipient, recipientParam.Value);
        }
    }
}
