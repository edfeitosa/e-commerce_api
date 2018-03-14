using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecommerce_api.Models
{
    public class ResponseModels
    {
        public int HttpStatusCode { get; set; }
        public int SystemCode { get; set; }
        public string Mensagem { get; set; }
        public int Container { get; set; }
    }
}