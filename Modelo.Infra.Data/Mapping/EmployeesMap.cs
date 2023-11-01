using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Domain.Entities;

namespace Modelo.Infra.Data.Mapping
{
    public class EmployeesMap : IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Nome)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("varchar(100)");

            builder.Property(prop => prop.Sobrenome)
              .HasConversion(prop => prop.ToString(), prop => prop)
              .IsRequired()
              .HasColumnName("SobreNome")
              .HasColumnType("varchar(100)");

            builder.Property(prop => prop.Email)
               .HasConversion(prop => prop.ToString(), prop => prop)
               .IsRequired()
               .HasColumnName("Email")
               .HasColumnType("varchar(100)");

            builder.Property(prop => prop.Password)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Password")
                .HasColumnType("varchar(100)");

            builder.Property(prop => prop.Role)
               .HasConversion<int>()
               .HasColumnName("Role");
        }
    }
}
