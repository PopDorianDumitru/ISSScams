using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISSfixed.ISSProject.Common.Mikha.Post
{
    internal class PremiumPostRepositoryException : Exception
    {
        public PremiumPostRepositoryException() { }
        public PremiumPostRepositoryException(string message) : base(message) { }

    }
}
