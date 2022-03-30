using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSite.Infrastructure.Interfaces;


namespace TestSite.Api.Entites
{
    public struct GetWorkerParams
    {
        public Filter Filter { get; set; }

        public IWorkerRepository.Sort Sort { get; set; }
    }
}
