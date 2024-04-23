using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISSfixed.ISSProject.Common.Mikha.Post
{
    internal class MockPostError : Exception
    {
        public MockPostError()
        {
        }
        public MockPostError(string message) : base(message)
        {
        }
    }
}
