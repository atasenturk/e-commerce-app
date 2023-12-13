using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApp.Common.Infrastructure.Extensions
{
    public static class UsernameGenerator
    {
        public static string GenerateUsername(string firstName, string lastName)
        {
            var random = new Random();
            StringBuilder sb = new StringBuilder();
            sb.Append(firstName);
            sb.Append(lastName);
            sb.Append(random.Next(1000, 100000));
            return sb.ToString();   
        }
    }
}
