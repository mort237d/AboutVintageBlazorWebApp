using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AboutVintageBlazorWebApp.Data;
using AboutVintageWebAppLibrary;

namespace AboutVintageApiService.DBUtils
{
    public class ManageComplaint
    {
        private SqlConnection connection = Utils.GetConnection();

        public List<Complaint> GetAllComplaints()
        {
            string queryString = "SELECT * FROM complaints;";
            List<Complaint> complaints = new List<Complaint>();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Complaint complaint = new Complaint();
                    complaint.Id = reader.GetInt32(0);
                    complaint.Date = reader.GetDateTime(1);
                    complaint.CustomerName = reader.GetString(2);
                    complaint.OrderNumber = reader.GetString(3);
                    complaint.OrderDate = reader.GetDateTime(4);
                    complaint.ProductVariant = reader.GetString(5);
                    complaint.WatchNumber = reader.GetInt32(6);
                    complaint.WatchProducer = reader.GetString(7);
                    complaint.Error = reader.GetString(8);
                    complaint.Solution = reader.GetString(9);
                    complaint.Comments = reader.GetString(10);
                    complaint.ProductType = reader.GetString(11);
                    complaints.Add(complaint);
                }
            }
            finally
            {
                connection.Close();
                reader.Close();
            }

            return complaints;
        }

        public Complaint GetComplaintFromId(int complaintId)
        {
            string queryString = string.Format("SELECT * FROM complaints WHERE Id = {0};", complaintId);
            Complaint complaint = new Complaint();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    complaint.Id = reader.GetInt32(0);
                    complaint.Date = reader.GetDateTime(1);
                    complaint.CustomerName = reader.GetString(2);
                    complaint.OrderNumber = reader.GetString(3);
                    complaint.OrderDate = reader.GetDateTime(4);
                    complaint.ProductVariant = reader.GetString(5);
                    complaint.WatchNumber = reader.GetInt32(6);
                    complaint.WatchProducer = reader.GetString(7);
                    complaint.Error = reader.GetString(8);
                    complaint.Solution = reader.GetString(9);
                    complaint.Comments = reader.GetString(10);
                    complaint.ProductType = reader.GetString(11);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            if (complaint.Id == 0) return null;
            return complaint;
        }

        public bool CreateComplaint(Complaint complaint)
        {
            string queryString = string.Format("INSERT INTO complaints (Date, CustomerName, OrderNumber, OrderDate, ProductVariant, WatchNumber, WatchProducer, Error, Solution, Comments, ProductType) " +
                                               "VALUES (cast('{0}' as date), '{1}', '{2}', cast('{3}' as date), '{4}', {5}, '{6}', '{7}', '{8}', '{9}', '{10}');",
                complaint.Date.ToString("yyyy-MM-dd"), complaint.CustomerName, complaint.OrderNumber, complaint.OrderDate.ToString("yyyy-MM-dd"), complaint.ProductVariant, 
                complaint.WatchNumber, complaint.WatchProducer, complaint.Error, complaint.Solution, complaint.Comments, complaint.ProductType);

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            try
            {
                return command.ExecuteNonQuery() == 0 ? false : true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool UpdateComplaint(Complaint complaint, int complaintId)
        {
            string queryString = string.Format("UPDATE complaints SET Date = cast('{0}' as date), CustomerName = '{1}', OrderNumber = '{2}', " +
                                               "OrderDate = cast('{3}' as date), ProductVariant = '{4}', WatchNumber = {5}, WatchProducer = '{6}', " +
                                               "Error = '{7}', Solution = '{8}', Comments = '{9}', ProductType = '{10}' WHERE Id = {11};",
                complaint.Date.ToString("yyyy-MM-dd"), complaint.CustomerName, complaint.OrderNumber, complaint.OrderDate.ToString("yyyy-MM-dd"), 
                complaint.ProductVariant, complaint.WatchNumber, complaint.WatchProducer, complaint.Error, complaint.Solution, complaint.Comments, complaint.ProductType, complaintId);

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            try
            {
                return command.ExecuteNonQuery() == 0 ? false : true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool DeleteComplaint(int complaintId)
        {
            string queryString = string.Format("DELETE FROM complaints WHERE Id = {0};", complaintId);

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            try
            {
                return command.ExecuteNonQuery() == 0 ? false : true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
