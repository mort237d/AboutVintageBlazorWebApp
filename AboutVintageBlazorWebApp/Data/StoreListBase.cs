using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace AboutVintageBlazorWebApp.Data
{
    public class StoreListBase : ComponentBase
    {
        [Inject]
        public IStoreService storeService { get; set; }
        public IEnumerable<Store> stores { get; set; } = new List<Store>();

        protected override async Task OnInitializedAsync()
        {
            stores = (await storeService.GetStores()).ToList();
        }
    }
}
