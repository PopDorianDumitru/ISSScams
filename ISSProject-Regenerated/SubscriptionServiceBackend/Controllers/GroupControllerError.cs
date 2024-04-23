using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSfixed.ISSProject.Mikha.Controllers
{
    internal class GroupControllerError : Exception
    {
        public GroupControllerError() { }
        public GroupControllerError(string message) : base(message) { }

    }
}
