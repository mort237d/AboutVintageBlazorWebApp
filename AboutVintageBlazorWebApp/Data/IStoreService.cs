using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AboutVintageBlazorWebApp.Data
{
    public interface IStoreService
    {
        Task<IEnumerable<Store>> GetStores();
    }
}
