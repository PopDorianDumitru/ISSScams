using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISSfixed.ISSProject.Common.Mikha.Premium_Messages
{
    internal class PremiumMessageRepositoryError : Exception
    {
        public PremiumMessageRepositoryError()
        {
        }
        public PremiumMessageRepositoryError(string message) : base(message)
        {
        }
    }
}
