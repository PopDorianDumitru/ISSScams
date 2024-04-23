using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Wrapper;

namespace ISSProject_Regenerated.GraphAnalyser.Controller
{
    internal interface IUserDiscordController
    {
        bool Verbose { get; set; }

        void MessageUserAfterMinMax(UserWrapper user);

        void TraverseAllUsers();
    }
}
