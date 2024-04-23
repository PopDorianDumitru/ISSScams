using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSfixed.ISSProject.Mikha.Groups
{
    internal class GroupRepositoryError : Exception
    {
        public GroupRepositoryError() { }
        public GroupRepositoryError(string message) : base(message) { }

    }
}

