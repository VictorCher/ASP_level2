using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient
    {
        protected readonly HttpClient _Client;

        protected readonly string _ServiceAddress;

        protected BaseClient(IConfiguration configuration, string ServiceAddress)
        {
            _ServiceAddress = ServiceAddress;

            _Client = new HttpClient
            {
                BaseAddress = new Uri(configuration["ClientAddress"])
            };
            var headers = _Client.DefaultRequestHeaders.Accept;

            headers.Clear();

            headers.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
