using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace MetadataDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://crmserver2015.crm.data-8.co.uk/SummitEMEA2019/tools/systemcustomization/attributes/manageAttribute.aspx?appSolutionId=%7b6BDEA0EC-7528-E911-80F1-00155D007101%7d&attributeId=%7bFB468DCD-0C2E-E911-80F1-00155D007101%7d&entityId=%7b9ec65306-082e-e911-80f1-00155d007101%7d 

            var crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRMUGDemo"].ConnectionString);
            var org = crmSvc.OrganizationServiceProxy;

            var existingRecordsQry = new QueryExpression("data8_metadata")
            {
                ColumnSet = new ColumnSet("data8_name", "data8_thealphabet"),
                
            };
            existingRecordsQry.Criteria.AddCondition("data8_thealphabet", ConditionOperator.Equal, "abcdefghijklmnopqrstuvwxyz");
            var existingRecordsResp = org.RetrieveMultiple(existingRecordsQry);
            Console.WriteLine($"{existingRecordsResp.TotalRecordCount} records found");

            var firstMatch = existingRecordsResp.Entities.First();
            firstMatch["data8_name"] = "A new name";
            org.Update(firstMatch);
        }
    }
}
