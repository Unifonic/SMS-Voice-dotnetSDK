using Moq;
using RestSharp;
using Xunit;

namespace Unifonic.API.NetStandard.IntegrationTests
{
    public class CheckerTests
    {
        private Mock<UnifonicRestClient> _mockClient;

        private const string recipient = "962788888888";

        public CheckerTests()
        {
            _mockClient = new Mock<UnifonicRestClient>(Credentials.ApplicationSid) { CallBase = true };
        }

        [Fact]
        public void ShouldNumberInsight()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<NumberInsightResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new NumberInsightResult());

            var client = _mockClient.Object;

            client.NumberInsight(recipient);

            _mockClient.Verify(orc => orc.Execute<NumberInsightResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Checker/NumberInsight", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Single(savedRequest.Parameters);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.NotNull(recipientParam);
            Assert.Equal(recipient, recipientParam.Value);
        }
    }
}
