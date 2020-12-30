using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace AboutVintageBlazorWebApp.Data
{
    public class StoreService : IStoreService
    {
        private readonly HttpClient _httpClient;

        public StoreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Store>> GetStores()
        {
            return await _httpClient.GetJsonAsync<Store[]>("/api/stores");
        }
    }
}
