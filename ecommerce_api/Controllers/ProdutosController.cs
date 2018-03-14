using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ecommerce_api.Models;

namespace ecommerce_api.Controllers
{
    public class ProdutosController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Save()
        {
            if (Request.QueryString["Id"] == null)
            {
                ViewBag.title = "Adicionando novo produto";
            }
            else
            {
                ViewBag.title = "Editando produto existente";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Save(ProdutosModels produtos, HttpPostedFileBase arquivo)
        {
            ProdutosModels _produtos = new ProdutosModels();
            _produtos.pro_id = Convert.ToInt32(produtos.pro_id);
            _produtos.pro_nome = produtos.pro_nome;
            _produtos.pro_descricao = produtos.pro_descricao;
            var dados = new ProdutosModels().Save(_produtos, arquivo);
            string retorno = "";
            foreach(var item in dados)
            {
                string classe = "";
                switch(item.HttpStatusCode)
                {
                    case 200: classe = "sucesso"; break;
                    case 400: classe = "erro"; break;
                    case 500: classe = "erro"; break;
                    default: classe = "alerta"; break;
                }
                retorno = "<div class='" + classe + "'>" + item.Mensagem + "</div>";
            }
            ViewBag.mensagem = retorno;
            return View();
        }
    }
}