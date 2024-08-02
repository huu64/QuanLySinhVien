using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLySinhVien
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faculty>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Faculty)
                .HasForeignKey(e => e.FacultyID);

            modelBuilder.Entity<Faculty>()
                .HasMany(e => e.Students1)
                .WithOptional(e => e.Faculty1)
                .HasForeignKey(e => e.FacultyID);
        }
    }
}
