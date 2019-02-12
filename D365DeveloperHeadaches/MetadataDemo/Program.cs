using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace MetadataDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRMUGDemo"].ConnectionString);
            var org = crmSvc.OrganizationServiceProxy;

            var newRecord1 = new Entity("data8_metadata")
            {
                ["data8_name"] = "Alphabet Record",
                ["data8_thealphabet"] = "abcdefghijklmnopqrstuvwxyz"
            };

            newRecord1.Id = org.Create(newRecord1);
            Console.WriteLine($"Record created ({newRecord1.Id})");

            var newRecord2 = new Entity("data8_metadata")
            {
                ["data8_name"] = "Magic Number Record",
                ["data8_themagicnumber"] = 7
            };

            newRecord2.Id = org.Create(newRecord2);
            Console.WriteLine($"Record created ({newRecord2.Id})");

            var newRecord3 = new Entity("data8_metadata")
            {
                ["data8_name"] = "The Wrong Record",
                ["data8_therating"] = new OptionSetValue(888880002) // Bad
            };

            newRecord3.Id = org.Create(newRecord3);
            Console.WriteLine($"Record created ({newRecord3.Id})");
        }
    }
}
