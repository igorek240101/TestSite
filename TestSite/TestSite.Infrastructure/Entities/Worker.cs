using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestSite.Infrastructure.Entities
{
    public class Worker
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DepartamentId { get; set; }

        [ForeignKey("DepartamentId")]
        public Departament Departament { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime StartWorkDate { get; set; }

        public int Wage { get; set; }
    }
}
