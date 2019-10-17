using HackathonApi.Models.Options;
using Microsoft.Extensions.Options;
using Xunit;
using Moq;

namespace HackathonTest
{
    public class ServiceNowTest
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
        public void ServiceNowHostNotNull()
        {
            var options = MockServiceNowSetup();
            Assert.NotNull(options.ServiceNowHost);
            Assert.Equal(options.ServiceNowHost, _host);
        }

        [Fact]
        public void ServiceNowUserNotNull()
        {
            var options = MockServiceNowSetup();
            Assert.NotNull(options.ServiceNowUser);
            Assert.Equal(options.ServiceNowUser, _user);
        }

        [Fact]
        public void ServiceNowSecertNotNull()
        {
            var options = MockServiceNowSetup();
            Assert.NotNull(options.ServiceNowSecret);
            Assert.Equal(options.ServiceNowSecret, _secret);
        }
    }
}
