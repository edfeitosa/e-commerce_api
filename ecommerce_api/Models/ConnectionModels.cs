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
            return "Database=odinproj_ecommerce; Data Source=localhost; User Id=api-comm-test; Password=91hJf^l5; pooling=false".ToString();
        }
    }
}