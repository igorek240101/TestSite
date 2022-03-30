using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSite.Api.Entites
{
    public struct Filter
    {
        public int? MinWage { get; set; }

        public int? MaxWage { get; set; }

        public DateTime? MinBirth { get; set; }

        public DateTime? MaxBirth { get; set; }

        public DateTime? MinStartWork { get; set; }

        public DateTime? MaxStartWork { get; set; }

        public string[] Departament { get; set; }
    }
}
