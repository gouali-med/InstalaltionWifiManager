using FBCOMSystemManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FBCOMSystemManagement.DATA
{
    public class DBContextFBCOM: DbContext
    {
        public DBContextFBCOM(DbContextOptions<DBContextFBCOM> options) : base(options)
        {
        }

        public DbSet<CPE> CPES { get; set; }
        public DbSet<Installation> Installations { get; set; }
        public DbSet<Telephone> Telephones { get; set; }
    }
}
