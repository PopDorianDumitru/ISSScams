using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.ScamBots
{
    internal class ScamMessageRepositoryException : Exception
    {
        public ScamMessageRepositoryException() { }
        public ScamMessageRepositoryException(string message) : base(message) { }
        
    }
}

