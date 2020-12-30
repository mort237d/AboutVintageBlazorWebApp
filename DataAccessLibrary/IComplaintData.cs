using System.Collections.Generic;
using System.Threading.Tasks;
using AboutVintageWebAppLibrary;

namespace DataAccessLibrary
{
    public interface IComplaintData
    {
        Task<List<ComplaintModel>> GetComplaints();
        Task<List<ComplaintModel>> GetTopTenComplaints();
        Task<List<ComplaintModel>> GetTopTenComplaintsWhere(string where);
        Task<int> AddComplaint(ComplaintModel complaint);
        Task UpdateComplaint(ComplaintModel complaint);
        Task DeleteComplaint(int id);
    }
}