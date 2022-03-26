using TestSite.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestSite.Infrastructure.Context
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Departament>().HasData(
                    new Departament
                    {
                        Id = 1,
                        Name = "IT"
                    },
                    new Departament
                    {
                        Id = 2,
                        Name = "Бухгалтерия"
                    },
                    new Departament
                    {
                        Id = 2,
                        Name = "Отдел продаж"
                    }
                );
            modelBuilder.Entity<Worker>().HasData(
                new Worker()
                {
                    Id = 1,
                    Name = "Алексеев Алексей Алексеевич",
                    StartWorkDate = new DateTime(2010, 1, 1),
                    BirthDate = new DateTime(1990, 1, 1),
                    Wage = 10000,
                    DepartamentId = 1
                },
                new Worker()
                {
                    Id = 2,
                    Name = "Борисов Борис Борисович",
                    StartWorkDate = new DateTime(2015, 5, 15),
                    BirthDate = new DateTime(1985, 1, 5),
                    Wage = 30000,
                    DepartamentId = 1
                },
                new Worker()
                {
                    Id = 3,
                    Name = "Васильев Василий Васильевич",
                    StartWorkDate = new DateTime(2017, 7, 17),
                    BirthDate = new DateTime(1957, 1, 7),
                    Wage = 15000,
                    DepartamentId = 2
                },
                new Worker()
                {
                    Id = 4,
                    Name = "Григорьев Григорий Григорьевич",
                    StartWorkDate = new DateTime(2022, 1, 12),
                    BirthDate = new DateTime(1987, 2, 7),
                    Wage = 5000,
                    DepartamentId = 3
                }
                );
            */
        }
    }
}
