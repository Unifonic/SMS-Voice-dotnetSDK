using Moq;
using NUnit.Framework;
using RestSharp;

namespace Otsdc.API.IntegrationTests
{
    public class AccountTests
    {
        private Mock<OtsdcRestClient> _mockClient;

        private const string SenderId = "unitTest";

        [SetUp]
        public void Setup()
        {
            _mockClient = new Mock<OtsdcRestClient>(Credentials.ApplicationSid) {CallBase = true};
        }

        [Test]
        public void ShouldGetBalance()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetBalanceResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new GetBalanceResult());
            var client = _mockClient.Object;

            client.GetBalance();

            _mockClient.Verify(orc => orc.Execute<GetBalanceResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Account/GetBalance", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(0, savedRequest.Parameters.Count);
        }

        [Test]
        public void ShouldAddSender()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<AddSenderResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new AddSenderResult());
            var client = _mockClient.Object;

            client.AddSender(SenderId);

            _mockClient.Verify(orc => orc.Execute<AddSenderResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Account/addSenderID", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.IsNotNull(senderIdParam);
            Assert.AreEqual(SenderId, senderIdParam.Value);
        }

        [Test]
        public void ShouldGetSenderStatus()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetSenderStatusResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new GetSenderStatusResult());
            var client = _mockClient.Object;

            client.GetSenderStatus(SenderId);

            _mockClient.Verify(orc => orc.Execute<GetSenderStatusResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Account/getSenderIDStatus", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.IsNotNull(senderIdParam);
            Assert.AreEqual(SenderId, senderIdParam.Value);
        }

        [Test]
        public void ShouldGetSenders()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetSendersResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new GetSendersResult());

            var client = _mockClient.Object;
            client.GetSenders();

            _mockClient.Verify(orc => orc.Execute<GetSendersResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Account/getSenderIDs",savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(0, savedRequest.Parameters.Count);
        }

        [Test]
        public void ShouldDeleteSender()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new bool());
            var client = _mockClient.Object;

            client.DeleteSender(SenderId);

            _mockClient.Verify(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Account/DeleteSenderID", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.IsNotNull(senderIdParam);
            Assert.AreEqual(SenderId, senderIdParam.Value);
        }

        [Test]
        public void ShouldGetAppDefaultSender()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetAppDefaultSenderResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new GetAppDefaultSenderResult());
            var client = _mockClient.Object;

            client.GetAppDefaultSender();
            _mockClient.Verify(orc => orc.Execute<GetAppDefaultSenderResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Account/GetAppDefaultSenderID", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(0, savedRequest.Parameters.Count);
        }


        [Test]
        public void ShouldChangeAppDefaultSender()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new bool());
            var client = _mockClient.Object;

            client.ChangeAppDefaultSender(SenderId);

            _mockClient.Verify(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Account/changeAppDefaultSenderID", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.IsNotNull(senderIdParam);
            Assert.AreEqual(SenderId, senderIdParam.Value);
        }

    }
}
