using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class GraphAnalyserLogRepositoryException : Exception
{
    public GraphAnalyserLogRepositoryException() { }
    public GraphAnalyserLogRepositoryException(string message) : base(message) { }
}
