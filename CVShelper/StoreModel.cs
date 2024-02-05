using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVShelper
{
    public class StoreModel
    {

        public int? SiteId { get; set; } 

        public String Name { get; set; }

        public decimal? MainLedgerAccountNumber { get; set; }

        public decimal? OrganizationNumber { get; set; }

        public String Email { get; set; }

        public String ChainName { get; set; }

        public int? RegionId { get; set; }

    }
}
