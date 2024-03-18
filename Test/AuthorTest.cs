using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Authors;
using Authors.Models;
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
        private readonly TestServer testServer;
        private readonly HttpClient client;

        public AuthorTest()
        {
            var stephenKing = new Writer("Stephen King", null, Gender.Male);
            var agatha = new Writer("Agatha Christie", DateTime.Parse("1890-09-15"), Gender.Female);
            var repoMock = new Mock<ILibraryRepository>();
            repoMock.Setup(mbox => mbox.GetWriter(1)).Returns(stephenKing);
            repoMock.Setup(mbox => mbox.GetWriter(2)).Returns((Writer)null);
            repoMock.Setup(x => x.GetWriters()).Returns([stephenKing, agatha]);

            var builder = new WebHostBuilder().
                UseStartup<Startup>()
                .ConfigureTestServices(s =>
                {
                    s.AddTransient<ILibraryRepository>(_ => repoMock.Object);
                    s.AddSingleton<DataContext>();
                });

            testServer = new TestServer(builder);

            client = testServer.CreateClient();
        }

        [Fact]
        public async Task Get_Writer_Returns_Writer()
        {
            var response = await client.GetAsync("/author/1");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_Writer_Returns_Not_Found()
        {
            var response = await client.GetAsync("/author/2");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_Writer_Returns_All_Writers()
        {
            var response = await client.GetAsync("/authors");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
