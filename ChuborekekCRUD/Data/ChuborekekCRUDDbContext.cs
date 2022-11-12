using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ChuborekekCRUD.Data
{
    public class ChuborekekCRUDDbContext : DbContext
    {

        public ChuborekekCRUDDbContext(DbContextOptions<ChuborekekCRUDDbContext> options) 
            : base(options)
        {
        }
        public virtual DbSet<Cat> Cats { get; set; }
        public virtual DbSet<Dog> Dogs { get; set; }
    }
}
