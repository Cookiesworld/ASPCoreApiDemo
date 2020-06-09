using System.Net.Http;
using System.Threading.Tasks;
using Authors;
using Authors.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Test
{
    public class AuthorTest
    {
        private TestServer testServer;
        private HttpClient client;

        public AuthorTest()
        {
            var stephenKing = new Authors.Models.Writer("Stephen King", null);            

            var repoMock = new Mock<ILibraryRepository>();
            repoMock.Setup(mbox => mbox.GetWriter(1)).Returns(stephenKing);

            var builder = new WebHostBuilder().
                UseStartup<Startup>()
                .ConfigureTestServices(s => s.AddTransient<ILibraryRepository>(_ => repoMock.Object));

            this.testServer = new TestServer(builder);

            this.client = testServer.CreateClient();
        }

        [Fact]
        public async Task Get_Writer_Returns_Writer()
        {
            var response = await client.GetAsync("/author/1");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
