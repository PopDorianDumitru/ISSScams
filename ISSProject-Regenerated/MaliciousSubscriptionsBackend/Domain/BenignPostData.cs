using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.MaliciousSubscriptionsBackend.Domain
{
    internal class BenignPostData
    {
        public string Email { get; set; }

        public BenignPostData(string email)
        {
            this.Email = email;
        }
    }
}
