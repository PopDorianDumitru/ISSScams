using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.ScamBots
{
    internal class FakeUserControllerException : Exception
    {
        public FakeUserControllerException()
        {
        }
        public FakeUserControllerException(string message) : base(message)
        {
        }
    }
}

