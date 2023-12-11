using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApp.Common.Infrastructure.Exceptions;

public class DatabaseValidationException : Exception
{
    public DatabaseValidationException()
    {
        
    }
    public DatabaseValidationException(string? message) : base(message)
    {
        
    }
}