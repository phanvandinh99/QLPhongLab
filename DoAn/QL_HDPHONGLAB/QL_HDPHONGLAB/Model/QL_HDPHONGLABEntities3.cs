using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QL_HDPHONGLAB.Model
{
    public partial class QL_HDPHONGLABEntities3 : DbContext
    {
        public QL_HDPHONGLABEntities3()
            : base("name=QL_HDPHONGLABEntities3")
        {
        }

        public virtual DbSet<HOACHAT> HOACHAT { get; set; }
        public virtual DbSet<PHIEUMUON> PHIEUMUON { get; set; }
        public virtual DbSet<PHONGLAB> PHONGLAB { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HOACHAT>()
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
