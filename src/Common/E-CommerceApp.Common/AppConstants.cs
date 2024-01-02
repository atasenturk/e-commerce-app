using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApp.Common
{
    public class AppConstants
    {
        public const string RabbitMQHost = "localhost";
        public const string DefaultExhangeType = "direct";
        public const int RabbitMQPort = 5672;

        public const string UserExchangeName = "UserExchange";
        public const string UserCreatedQueueName = "UserCreatedQueue";
    }
}
