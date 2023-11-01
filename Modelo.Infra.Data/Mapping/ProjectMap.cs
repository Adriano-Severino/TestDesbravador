using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Domain.Entities;

namespace Modelo.Infra.Data.Mapping
{
    public class ProjectMap : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.ProjectName)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("NomeProjeto")
                .HasColumnType("varchar(100)");

            builder.Property(prop => prop.ProjectDescription)
              .HasConversion(prop => prop.ToString(), prop => prop)
              .IsRequired()
              .HasColumnName("DescricaoProjeto")
              .HasColumnType("varchar(1024)");

            builder.Property(prop => prop.StartDate)
               .HasConversion(prop => prop, prop => prop.Date)
               .IsRequired()
               .HasColumnName("DataInicio")
               .HasColumnType("date");

            builder.Property(prop => prop.EndDate)
                .HasColumnName("DataFinal")
                .HasColumnType("date");

            builder.Property(prop => prop.ProjectRiskEnum)
               .HasConversion<int>()
               .HasColumnName("RiscoProjeto");

            builder.Property(prop => prop.StatusProjectEnum)
               .HasConversion<int>()
               .HasColumnName("StatusProjeto");
        }
    }
}