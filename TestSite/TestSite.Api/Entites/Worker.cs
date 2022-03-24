using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSite.Api.Entites
{
    public class Worker
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Departament { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? StartWorkDate { get; set; }

        public int? Wage { get; set; }
    }
}
