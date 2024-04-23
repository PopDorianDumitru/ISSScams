using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject_Regenerated.MaliciousSubscriptionsFrontEnd.CompanyForm.Controller
{
    internal interface IProcessedCompanyInformation
    {
        bool ValidateCompanyToken();

        bool CommitTokenToDatabase();
    }
}
