using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MockNoEntityViolation : Exception
{
    public MockNoEntityViolation() { }
    public MockNoEntityViolation(string message) : base(message) { }
    public MockNoEntityViolation(object obj) :
        base($"No entity found in Mock Database for key: {obj}") { }
}
