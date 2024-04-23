using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.MaliciousSubscriptionsBackend.Domain;
using ISSProject.MaliciousSubscriptionsBackend.Service;
using ISSProject_Regenerated.MaliciousSubscriptionsBackend;

namespace ISSProject.MaliciousSubscriptionsBackend
{
    internal static class Driver
    {
        public static void RUN()
        {
            CompanyToken benigntest1 = new CompanyToken(2, "Benign Newsletters and Tea", "http://fakelink2.com", "MRVSHEP07", 0);
            CompanyToken benigntest2 = new CompanyToken(6, "Benign Overlords A.C.C.E.N.T.", "http://fakelink6.com", "PACFDIM07", 0);
            CompanyToken severetest = new CompanyToken(10, "Netflix", "http://fakelink10.com", "NETFLIX00", 1);
            MaliciousSubcriptionController controller = new MaliciousSubcriptionController(benigntest1);
            controller.RUN();
        }
    }
}
