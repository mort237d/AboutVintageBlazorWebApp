using System;
using System.Collections.Generic;
using System.Text;
using EfDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EfDataAccessLibrary.DataAccess
{
    public class OrderComplaintContext : DbContext
    {
        public OrderComplaintContext(DbContextOptions options) : base(options){}
        public DbSet<OrderComplaint> OrderComplaints { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Variant> Variants { get; set; }

        public void BulkDelete<TModel>(IList<TModel> entities) where TModel : class
        {
            this.BulkDelete(entities);
        }
    }
}
