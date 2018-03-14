using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecommerce_api.Models
{
    public class ConnectionModels
    {
        public string Connection()
        {
            return "Database=ecommerce; Data Source=localhost; User Id=root; Password=040810; pooling=false".ToString();
        }
    }
}