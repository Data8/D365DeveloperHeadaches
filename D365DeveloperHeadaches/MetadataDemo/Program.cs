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

            var newRecord = new Entity("data8_metadata")
            {
                ["data8_thealphabet"] = "abcdefghijklmnopqrstuvwxyz"
            };

            newRecord.Id = org.Create(newRecord);
            Console.WriteLine($"Record created ({newRecord.Id})");
        }
    }
}
