using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Tooling.Connector;

namespace CustomAttributeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRMUGDemo"].ConnectionString);
            var org = crmSvc.OrganizationServiceProxy;
            Entity account = null;
                        
            Console.WriteLine("Connected...");

            var createAttrReq = new CreateAttributeRequest()
            {
                EntityName = "account",
                Attribute = new StringAttributeMetadata()
                {
                    SchemaName = "address1_brandnewfield",
                    DisplayName = new Label("Address 1: Custom Field 1", 1033),
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                    Description = new Label("String Attribute", 1033),
                    MaxLength = 100
                }
            };

            try
            {
                Console.WriteLine("Ready to create attr");
                org.Execute(createAttrReq);
                Console.WriteLine("Attr created");

                Console.WriteLine("Creating Account");
                account = new Entity("account") { ["name"] = "Demo Account" };
                account.Id = org.Create(account);
                Console.WriteLine($"Account created (ID: {account.Id}");

                Console.WriteLine("Updating address1_line1");
                account["address1_line1"] = "RAI Amsterdam";
                org.Update(account);
                Console.WriteLine("Updated address1_line1");

                Console.WriteLine("Updating address1_brandnewfield");
                account["address1_brandnewfield"] = "A useful piece of data";
                org.Update(account);
                Console.WriteLine("Updated address1_brandnewfield");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                Console.WriteLine($"Tidying up...");

                Console.WriteLine($"Deleting attr");
                var deleteAttrReq = new DeleteAttributeRequest()
                {
                    EntityLogicalName = "account",
                    LogicalName = "address1_brandnewfield"
                };

                org.Execute(deleteAttrReq);
                Console.WriteLine("Attr deleted");

                Console.WriteLine($"Deleting account");
                org.Delete(account.LogicalName, account.Id);
                Console.WriteLine($"Deleted account");
            }

            Console.ReadLine();
        }
    }
}
