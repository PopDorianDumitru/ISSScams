using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MessageRepositoryException : Exception
{
    public MessageRepositoryException() { }
    public MessageRepositoryException(string message) : base(message) { }
}
