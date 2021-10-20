using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DemoWebApi.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext():base("DefaultConnection")
        {

        }
        public DbSet<User> Users { get;set; }
        public DbSet<UserModelTemp> TempUsers { get; set; }
        public DbSet<VarificationModel> VarificationModel { get; set; }


    }
}