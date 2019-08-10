
using MEDAPP.WebAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace MEDAPP.IntegrationTesting
{
    public class TestFixture: ICollectionFixture<WebApplicationFactory<Startup>>
    {
        public readonly HttpClient Client;

        public TestFixture()
        {
            var factory = new WebApplicationFactory<Startup>();
            Client = factory.CreateClient();
        }
    }
   
}
