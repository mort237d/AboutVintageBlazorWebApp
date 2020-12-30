using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AboutVintageWebAppLibrary;

namespace DataAccessLibrary
{
    public class ComplaintData : IComplaintData
    {
        private readonly ISqlDataAccess _db;

        public ComplaintData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<ComplaintModel>> GetComplaints()
        {
            string sql = "SELECT * FROM complaints ORDER BY Date DESC";

            return _db.LoadData<ComplaintModel, dynamic>(sql, new { });
        }

        public Task<List<ComplaintModel>> GetTopTenComplaints()
        {
            string sql = "SELECT TOP 10 * FROM complaints ORDER BY Date DESC";

            return _db.LoadData<ComplaintModel, dynamic>(sql, new { });
        }

        public Task<List<ComplaintModel>> GetTopTenComplaintsWhere(string where)
        {
            string sql = $"SELECT TOP 10 * FROM complaints WHERE {where} ORDER BY Date DESC";

            return _db.LoadData<ComplaintModel, dynamic>(sql, new { });
        }

        public Task<int> AddComplaint(ComplaintModel complaint)
        {
            string sql = "INSERT INTO complaints (Date, CustomerName, OrderNumber, OrderDate, ProductVariant, WatchNumber, WatchProducer, Error, Solution, " +
                         "Comments, ProductType)" +
                         "VALUES (@Date, @CustomerName, @OrderNumber, @OrderDate, @ProductVariant, @WatchNumber, @WatchProducer, @Error, @Solution, " +
                         "@Comments, @ProductType);" +
                         "SELECT CAST(SCOPE_IDENTITY() as int)";

            return _db.SaveData(sql, complaint);
        }

        public Task UpdateComplaint(ComplaintModel complaint)
        {
            string sql = "UPDATE complaints SET Date = @Date, CustomerName = @CustomerName, OrderNumber = @OrderNumber, " +
                         "OrderDate = @OrderDate, ProductVariant = @ProductVariant, WatchNumber = @WatchNumber, WatchProducer = @WatchProducer, " +
                         "Error = @Error, Solution = @Solution, Comments = @Comments, ProductType = @ProductType WHERE Id = @Id;";

            return _db.SaveData(sql, complaint);
        }

        public Task DeleteComplaint(int id)
        {
            string sql = "DELETE FROM complaints WHERE Id = @Id;";

            return _db.DeleteData(sql, new { Id = id });
        }
    }
}
