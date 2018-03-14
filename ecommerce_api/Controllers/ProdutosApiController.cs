using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using ecommerce_api.Models;

namespace ecommerce_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProdutosApiController : Controller
    {
        /*[HttpPost]
        public IEnumerable<ResponseModels> PostSave(TagsModels tags)
        {
            TagsModels _tags = new TagsModels();
            _tags.Id = Convert.ToInt32(tags.Id);
            _tags.Nome = tags.Nome;
            _tags.Descricao = tags.Descricao;
            return new TagsModels().Save(_tags);
        }*/

        [HttpGet]
        public ActionResult GetProdutosAll()
        {
            return Json(new ProdutosModels().Read(Convert.ToInt32(0)), JsonRequestBehavior.AllowGet);
        }
    }
}