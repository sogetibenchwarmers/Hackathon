using HackathonApi.Models.Options;
using Xunit;
using Moq;
using HackathonApi.Mediator;
using MediatR;
using System.Threading;

namespace HackathonTest
{
    public class GetLocationsHandlerTest
    {
        private readonly string _host = "http://moq.this.gov";
        private readonly string _user = "HelloWorld";
        private readonly string _secret = "HelloSecret";
        private ServiceNowOptions MockServiceNowSetup() {
            var options = new Mock<ServiceNowOptions>();
            options.Setup(m => m.ServiceNowHost).Returns(_host);
            options.Setup(m => m.ServiceNowUser).Returns(_user);
            options.Setup(m => m.ServiceNowSecret).Returns(_secret);
            return options.Object;
        }

        [Fact]
        public void GetLocationsHandler()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(s => s.Send(It.IsAny<GetLocationsRequest>(), default(CancellationToken))).Verifiable("Notification was not sent.");
            Assert.NotNull(mockMediator);
            Assert.NotNull(mockMediator.Object);
        }
    }
}