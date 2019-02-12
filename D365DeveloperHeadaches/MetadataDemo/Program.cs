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
            var crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRMUGDemo"].ConnectionString);
            var org = crmSvc.OrganizationServiceProxy;

            var existingRecordsQry1 = new QueryExpression("data8_metadata") { ColumnSet = new ColumnSet("data8_name", "data8_thealphabet") };
            existingRecordsQry1.Criteria.AddCondition("data8_thealphabet", ConditionOperator.Equal, "abcdefghijklmnopqrstuvwxyz");
            var existingRecordsResp1 = org.RetrieveMultiple(existingRecordsQry1);
            Console.WriteLine($"{existingRecordsResp1.Entities.Count} records found");

            var alphabetRecord = existingRecordsResp1.Entities.First();
            alphabetRecord["data8_name"] = "A new name";

            var existingRecordsQry2 = new QueryExpression("data8_metadata") { ColumnSet = new ColumnSet("data8_name", "data8_themagicnumber") };
            existingRecordsQry2.Criteria.AddCondition("data8_themagicnumber", ConditionOperator.Equal, 7);
            var existingRecordsResp2 = org.RetrieveMultiple(existingRecordsQry2);
            Console.WriteLine($"{existingRecordsResp2.Entities.Count} records found");

            var numberRecord = existingRecordsResp2.Entities.First();
            numberRecord["data8_name"] = "A new name";

            var existingRecordsQry3 = new QueryExpression("data8_metadata") { ColumnSet = new ColumnSet("data8_name", "data8_therating") };
            existingRecordsQry3.Criteria.AddCondition("data8_therating", ConditionOperator.Equal, 888880002);
            var existingRecordsResp3 = org.RetrieveMultiple(existingRecordsQry3);
            Console.WriteLine($"{existingRecordsResp3.Entities.Count} records found");

            var wrongRatingRecord = existingRecordsResp3.Entities.First();
            wrongRatingRecord["data8_name"] = "A new name";

            try
            {
                org.Update(alphabetRecord);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating alphabet record{Environment.NewLine}{ex}");
            }

            try
            {
                org.Update(numberRecord);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating magic number record{Environment.NewLine}{ex}");
            }

            try
            {
                org.Update(wrongRatingRecord);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating rating record{Environment.NewLine}{ex}");
            }
        }
    }
}
