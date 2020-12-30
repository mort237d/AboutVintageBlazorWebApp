using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfDataAccessLibrary.DataAccess;
using EfDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace AboutVintage.Data
{
    public class OrderComplaintContextServices
    {
        private readonly OrderComplaintContext _db;

        public OrderComplaintContextServices(OrderComplaintContext db)
        {
            _db = db;
        }

        public List<OrderComplaint> GetOrderComplaints()
        {
            var list = _db.OrderComplaints
                .Include(c => c.Customerer)
                .Include(v => v.Variants)
                .Include(a => a.Customerer.Address)
                .ToList();
            return list;
        }

        public List<OrderComplaint> GetOrderComplaintsByOrderNumber(string orderNumber)
        {
            var list = _db.OrderComplaints
                .Include(c => c.Customerer)
                .Include(v => v.Variants)
                .Include(a => a.Customerer.Address)
                .Where(complaint => complaint.OrderNumber.Contains(orderNumber))
                .Take(5);

            return list.ToList();
        }

        public string CreateOrderComplaint(OrderComplaint orderComplaint)
        {
            _db.OrderComplaints.Add(orderComplaint);
            _db.SaveChanges();
            return "Save Successfully";
        }

        public string CreateMultipleOrderComplaint(List<OrderComplaint> orderComplaints)
        {
            _db.OrderComplaints.AddRange(orderComplaints);
            _db.SaveChanges();
            return "Save Successfully";
        }

        public OrderComplaint GetOrderComplaintById(int id)
        {
            OrderComplaint orderComplaint = _db.OrderComplaints.FirstOrDefault(oc => oc.Id == id);
            return orderComplaint;
        }

        public string UpdateOrderComplaint(OrderComplaint orderComplaint)
        {
            _db.OrderComplaints.Update(orderComplaint);
            _db.SaveChanges();
            return "Update Succesfully";
        }

        public string DeleteOrderComplaint(OrderComplaint orderComplaint)
        {
            _db.Remove(orderComplaint);
            _db.SaveChanges();
            return "Delete Succesfully";
        }
    }
}
