using BackEnd;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task ShouldReturnSuccessResponse()
        {
            using var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                    builder.UseSetting("https_port", "5001").UseEnvironment("Testing")
                )

                ;
            var client = factory.CreateClient();
            var response = await client.GetAsync("api/Titles/");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //response.EnsureSuccessStatusCode();
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            //var json = await response.Content.ReadAsStringAsync();
            //Assert.AreEqual("Administrator", json);
        }
    }
}
