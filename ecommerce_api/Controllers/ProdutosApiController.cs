using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net;
using System.Net.Http;
using ecommerce_api.Models;

namespace ecommerce_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProdutosApiController : ApiController
    {
        [HttpGet]
        public IEnumerable<ProdutosModels> GetProdutosAll(int id)
        {
            return new ProdutosModels().Read(id);
        }
    }
}