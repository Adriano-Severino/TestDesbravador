﻿using Microsoft.EntityFrameworkCore;
using Modelo.Domain.Entities;
using Modelo.Domain.Enums;
using Modelo.Infra.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Infra.Data.Context
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {

        }

        public DbSet<Employees> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

    
            modelBuilder.Entity<Employees>().HasData(new Employees
            {
                Id = Guid.NewGuid(), // Gere um novo Guid para o Id
                Nome = "Adminsitrador",
                Sobrenome = "adiminsitrador",
                Email = "admin@admin.com",
                Password = "123",
                Role = RoleEnum.Admin,
            });

            modelBuilder.Entity<Employees>(new EmployeesMap().Configure);
            modelBuilder.Entity<Project>(new ProjectMap().Configure);
        }

    }
}
