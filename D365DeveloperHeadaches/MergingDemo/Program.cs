using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace MergingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRMUGDemo"].ConnectionString);
            var org = crmSvc.OrganizationServiceProxy;

            var c1 = new Entity("contact")
            {
                ["firstname"] = "Sample",
                ["lastname"] = "Contact"
            };
            c1.Id = org.Create(c1);

            var a1 = new Entity("account")
            {
                ["name"] = "Sample Account",
                ["websiteurl"] = "https://www.data-8.co.uk"
                
            };
            a1.Id = org.Create(a1);

            var a2 = new Entity("account")
            {
                ["name"] = "Sample Account",    
                ["primarycontactid"] = c1.ToEntityReference()
            };
            a2.Id = org.Create(a2);

            Console.WriteLine("Records created");

            Console.WriteLine("(Re)load in account");
            var oldAccount = org.Retrieve(a2.LogicalName, a2.Id, new ColumnSet("name", "primarycontactid"));

            try
            {
                var mergeReq = new MergeRequest()
                {
                    Target = a1.ToEntityReference(),
                    SubordinateId = oldAccount.Id,
                    UpdateContent = new Entity("account")
                    {
                        ["primarycontactid"] = oldAccount["primarycontactid"],
                        ["websiteurl"] = a1["websiteurl"]
                    }
                };
                org.Execute(mergeReq);
            }
            catch (Exception ex)
            {
                org.Delete(a1.LogicalName, a1.Id);
                org.Delete(a2.LogicalName, a2.Id);
                org.Delete(c1.LogicalName, c1.Id);
                throw;
            }
        }
    }
}
