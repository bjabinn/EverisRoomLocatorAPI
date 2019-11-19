using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using SalasEveris;

namespace SalasEveris.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            TestClient = appFactory.CreateClient();
        }        
    }
}
