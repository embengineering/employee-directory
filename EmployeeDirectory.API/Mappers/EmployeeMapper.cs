using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EmployeeDirectory.API.Entities;

namespace EmployeeDirectory.API.Mappers
{
    class EmployeeMapper : EntityTypeConfiguration<Employee>
    {
        public EmployeeMapper()
        {
            ToTable("Employees");

            HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Id).IsRequired();

            Property(c => c.FirstName).IsRequired();
            Property(c => c.FirstName).HasMaxLength(50);

            Property(c => c.MiddleInitial).HasMaxLength(2);

            Property(c => c.LastName).IsRequired();
            Property(c => c.LastName).HasMaxLength(50);

            Property(c => c.SecondLastName).HasMaxLength(50);

            Property(c => c.JobTitle).HasMaxLength(50);

            Property(c => c.Location).HasMaxLength(50);

            Property(c => c.Email).IsRequired();
            Property(c => c.Email).HasMaxLength(50);

            Property(c => c.PhoneNumber).HasMaxLength(15);
        }
    }
}
