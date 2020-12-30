using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AboutVintageBlazorWebApp.Data;

namespace DataAccessLibrary
{
    public interface IStoreData
    {
        Task<List<StoreModel>> GetStores();
        Task<List<StoreModel>> GetStoresByDateAndStore(string store, DateTime minDate, DateTime maxDate);
        Task<List<StoreModel>> GetTopTenStores();
        Task AddStore(StoreModel store);
        Task UpdateStore(StoreModel store);
        Task DeleteStore(int id);
    }
}