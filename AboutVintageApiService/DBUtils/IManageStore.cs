using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AboutVintageBlazorWebApp.Data;

namespace AboutVintageApiService.DBUtils
{
    interface IManageStore
    {
        List<Store> GetAllStore();

        Store GetStoreFromId(int storeId);

        bool CreateStore(Store store);

        bool UpdateStore(Store store, int storeId);

        bool DeleteStore(int storeId);
    }
}
