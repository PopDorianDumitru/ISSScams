using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.ScamBots
{
    internal class FakeUserRepositoryException : Exception
    {
        public FakeUserRepositoryException()
        {
        }
        public FakeUserRepositoryException(string message) : base(message)
        {
        }
    }
}

