using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CustomActionDemo
{
    public class CountContactsAction : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var orgFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var org = orgFactory.CreateOrganizationService(context.UserId);
            var systemOrg = orgFactory.CreateOrganizationService(null);

            var target = (EntityReference)context.InputParameters["Target"];
            var contactQry = new QueryExpression("contact")
            {
                ColumnSet = new ColumnSet("firstname", "lastname"),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("parentcustomerid", ConditionOperator.Equal, target.Id)
                    }
                }
            };

            var contactResults = org.RetrieveMultiple(contactQry);
            context.OutputParameters["TotalCount"] = contactResults.Entities.Count;
        }
    }
}
