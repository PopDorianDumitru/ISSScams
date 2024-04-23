using ISSProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.MaliciousSubscriptionsBackend.Domain
{
    internal class CompanyToken : IKeyedEntity<int>
    {
        private readonly int ID;
        private string companyName;
        private string linkToAPI;
        private string token;
        private int serviceSeverity;

        public CompanyToken(int ID, string companyName, string linkToAPI, string token, int serviceSeverity)
        {
            this.ID = ID;
            this.companyName = companyName;
            this.linkToAPI = linkToAPI;
            this.token = token;
            this.serviceSeverity = serviceSeverity;
        }

        /* IKEYEDENTITY Functions */
        public object Clone()
        {
            return MemberwiseClone();
        }

        public int GetId()
        {
            return ID;
        }

        /* GETTERS and SETTERS */
        public string GetCompanyName() 
        {
            return companyName;
        }
        public string GetLinkToAPI() 
        {
            return linkToAPI;
        }
        public string GetToken() 
        {
            return token;
        }
        public int GetServiceSeverity() 
        {
            return serviceSeverity;
        }

    }
}
