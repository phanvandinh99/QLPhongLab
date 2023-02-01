using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QL_HDPHONGLAB.Model
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<CT_HOACHAT> CT_HOACHAT { get; set; }
        public virtual DbSet<PHIEUMUON> PHIEUMUON { get; set; }
        public virtual DbSet<PHONGLAB> PHONGLAB { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CT_HOACHAT>()
                .Property(e => e.MAHC)
                .IsUnicode(false);

            modelBuilder.Entity<PHIEUMUON>()
                .Property(e => e.MAHC)
                .IsUnicode(false);

            modelBuilder.Entity<PHIEUMUON>()
                .Property(e => e.MAPHLAB)
                .IsUnicode(false);

            modelBuilder.Entity<PHONGLAB>()
                .Property(e => e.MAPHLAB)
                .IsUnicode(false);
        }
    }
}
