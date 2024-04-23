using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Wrapper;

namespace ISSProject_Regenerated.GraphAnalyser.Domain
{
    internal interface IUserDiscordGraph
    {
        bool Verbose { get; set; }
        List<UserWrapper> Users { get; set; }

        int ComputeRelationScore(UserWrapper userA, UserWrapper userB);

        void GenerateGraph();

        UserWrapper FindMinMaxImbalance(UserWrapper userA);
    }
}
