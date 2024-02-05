using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVShelper
{

    public class StoreModelMap : ClassMap<StoreModel>
    {
        public StoreModelMap()
        {
            Map(m => m.SiteId).TypeConverter<NullableIntConverter>();
            Map(m => m.Name);
            Map(m => m.MainLedgerAccountNumber);
            Map(m => m.OrganizationNumber);
            Map(m => m.Email);  
            Map(m => m.ChainName);
            Map(m => m.RegionId).TypeConverter<NullableIntConverter>();
        }
    }

}
