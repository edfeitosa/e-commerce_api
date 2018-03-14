using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyEncryption;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace ecommerce_api.Models
{
    public class ComentariosModels
    {
        public int com_id { get; set; }
        public int pro_id { get; set; }
        public string com_usuario { get; set; }
        public string com_mensagem { get; set; }
        public string com_data { get; set; }

        MySqlConnection db = new MySqlConnection(new ConnectionModels().Connection());

        public IEnumerable<ResponseModels> Save(ComentariosModels comentarios)
        {
            IList<ResponseModels> list = new List<ResponseModels>();
            string QueryString;
            QueryString = "INSERT INTO comentarios (pro_id, com_usuario, com_mensagem, com_data) " +
                "VALUES (" + comentarios.pro_id + ", '" + comentarios.com_usuario + "', '" + comentarios.com_mensagem + "', NOW())";
            MySqlCommand query = new MySqlCommand(QueryString, db);
            try
            {
                db.Open();
                if (Convert.ToInt32(query.ExecuteNonQuery()) > 0)
                {
                    list.Add(new ResponseModels
                    {
                        HttpStatusCode = 200,
                        SystemCode = 1,
                        Mensagem = "Dados salvos com sucesso",
                        Container = 1
                    });
                }
                else
                {
                    list.Add(new ResponseModels
                    {
                        HttpStatusCode = 400,
                        SystemCode = 2,
                        Mensagem = "Ocorreu um erro no momento de salvar",
                        Container = 1
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                list.Add(new ResponseModels
                {
                    HttpStatusCode = 500,
                    SystemCode = 5,
                    Mensagem = "Ocorreu um erro: " + ex,
                    Container = 1
                });
                return list;
            }
            finally
            {
                db.Close();
            }
        }

        public IEnumerable<ComentariosModels> Read(int pro_id)
        {
            try
            {
                IList<ComentariosModels> list = new List<ComentariosModels>();
                string queryString;
                queryString = "SELECT com_id, com_usuario, com_mensagem, com_data FROM comentarios ORDER BY com_id DESC";
                MySqlCommand query = new MySqlCommand(queryString, db);
                db.Open();
                MySqlDataReader itens = query.ExecuteReader();
                if (itens.HasRows)
                {
                    while (itens.Read())
                    {
                        list.Add(new ComentariosModels
                        {
                            com_id = Convert.ToInt32(itens["com_id"]),
                            com_usuario = itens["com_usuario"].ToString(),
                            com_mensagem = itens["com_mensagem"].ToString(),
                            com_data = DateTime.ParseExact(Convert.ToDateTime(itens["com_data"]).ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString()
                        });
                    }
                }
                else
                {
                    list.Add(new ComentariosModels
                    {
                        com_id = Convert.ToInt32(0),
                        com_usuario = "Usuário não informado".ToString()
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
    }
}