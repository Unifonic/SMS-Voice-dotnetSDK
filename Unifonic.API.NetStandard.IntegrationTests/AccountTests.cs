using Moq;
using RestSharp;
using Xunit;

namespace Unifonic.API.NetStandard.IntegrationTests
{
    public class AccountTests
    {
        private Mock<UnifonicRestClient> _mockClient;

        private const string SenderId = "unitTest";

        public AccountTests()
        {
            _mockClient = new Mock<UnifonicRestClient>(Credentials.ApplicationSid) { CallBase = true };
        }

        [Fact]
        public void ShouldGetBalance()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetBalanceResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new GetBalanceResult());
            var client = _mockClient.Object;

            client.GetBalance();

            _mockClient.Verify(orc => orc.Execute<GetBalanceResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Account/GetBalance", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Empty(savedRequest.Parameters);
        }

        [Fact]
        public void ShouldAddSender()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<AddSenderResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new AddSenderResult());
            var client = _mockClient.Object;

            client.AddSender(SenderId);

            _mockClient.Verify(orc => orc.Execute<AddSenderResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Account/AddSenderID", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Single(savedRequest.Parameters);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.NotNull(senderIdParam);
            Assert.Equal(SenderId, senderIdParam.Value);
        }

        [Fact]
        public void ShouldGetSenderStatus()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetSenderStatusResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new GetSenderStatusResult());
            var client = _mockClient.Object;

            client.GetSenderStatus(SenderId);

            _mockClient.Verify(orc => orc.Execute<GetSenderStatusResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Account/GetSenderIDStatus", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Single(savedRequest.Parameters);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.NotNull(senderIdParam);
            Assert.Equal(SenderId, senderIdParam.Value);
        }

        [Fact]
        public void ShouldGetSenders()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetSendersResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new GetSendersResult());

            var client = _mockClient.Object;
            client.GetSenders();

            _mockClient.Verify(orc => orc.Execute<GetSendersResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Account/GetSenderIDs", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Empty(savedRequest.Parameters);
        }

        [Fact]
        public void ShouldDeleteSender()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new bool());
            var client = _mockClient.Object;

            client.DeleteSender(SenderId);

            _mockClient.Verify(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Account/DeleteSenderID", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Single(savedRequest.Parameters);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.NotNull(senderIdParam);
            Assert.Equal(SenderId, senderIdParam.Value);
        }

        [Fact]
        public void ShouldGetAppDefaultSender()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetAppDefaultSenderResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new GetAppDefaultSenderResult());
            var client = _mockClient.Object;

            client.GetAppDefaultSender();
            _mockClient.Verify(orc => orc.Execute<GetAppDefaultSenderResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Account/GetAppDefaultSenderID", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Empty(savedRequest.Parameters);
        }


        [Fact]
        public void ShouldChangeAppDefaultSender()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new bool());
            var client = _mockClient.Object;

            client.ChangeAppDefaultSender(SenderId);

            _mockClient.Verify(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Account/ChangeAppDefaultSenderID", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Single(savedRequest.Parameters);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.NotNull(senderIdParam);
            Assert.Equal(SenderId, senderIdParam.Value);
        }

    }
}
