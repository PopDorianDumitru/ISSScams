using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.MaliciousSubscriptionsBackend.Domain
{
    internal class PostData : IPostData
    {
        public string SelfJSON { get; set; }
        public PostData()
        {
            SelfJSON = string.Empty;
        }
    }
}
