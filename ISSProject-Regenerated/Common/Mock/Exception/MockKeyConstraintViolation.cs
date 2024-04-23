using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MockKeyConstraintViolation : Exception
{
    public MockKeyConstraintViolation()
    {
    }
    public MockKeyConstraintViolation(string message) : base(message)
    {
    }
    public MockKeyConstraintViolation(object obj) : base($"Key Constraint Violation in Mock Database for key: {obj}")
    {
    }
}
