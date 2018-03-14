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
    public class ComentariosApiController : ApiController
    {
        [HttpGet]
        public IEnumerable<ComentariosModels> GetComentariosAll(int id)
        {
            return new ComentariosModels().Read(id);
        }

        [HttpPost]
        public IEnumerable<ResponseModels> PostSave(ComentariosModels comentarios)
        {
            ComentariosModels _comentarios = new ComentariosModels();
            _comentarios.pro_id = Convert.ToInt32(comentarios.pro_id);
            _comentarios.com_usuario = comentarios.com_usuario.ToString();
            _comentarios.com_mensagem = comentarios.com_mensagem.ToString();
            return new ComentariosModels().Save(_comentarios);
        }
    }
}
