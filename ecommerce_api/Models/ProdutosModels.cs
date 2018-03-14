using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using EasyEncryption;
using MySql.Data.MySqlClient;

namespace ecommerce_api.Models
{
    public class ProdutosModels
    {
        public int pro_id { get; set; }
        public string pro_nome { get; set; }
        public string pro_descricao { get; set; }
        public string pro_arquivo { get; set; }
        public IEnumerable<HttpPostedFile> arquivo { get; set; }

        MySqlConnection db = new MySqlConnection(new ConnectionModels().Connection());

        public IEnumerable<ResponseModels> Save(ProdutosModels produtos, HttpPostedFileBase arquivo)
        {
            var fileName = "";
            if (arquivo != null && arquivo.ContentLength > 0)
            {
                // upload
                var ext = Path.GetExtension(arquivo.FileName);
                fileName = EasyEncryption.MD5.ComputeMD5Hash(Path.GetFileName(arquivo.FileName)) + ext;
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Upload"), fileName);
                arquivo.SaveAs(path);
            }

            IList<ResponseModels> list = new List<ResponseModels>();
            string QueryString;
            if (produtos.pro_id == 0)
            {
                QueryString = "INSERT INTO produtos (pro_nome, pro_descricao, pro_arquivo) VALUES ('" + produtos.pro_nome + "', '" + produtos.pro_descricao + "', 'http://localhost/ecommerce_api/Upload/" + fileName + "')";
            }
            else
            {
                QueryString = "UPDATE produtos SET pro_nome = '" + produtos.pro_nome + "', pro_descricao = '" + produtos.pro_descricao + "' WHERE pro_id = " + produtos.pro_id;
            }
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

        public IEnumerable<ProdutosModels> Read(int pro_id)
        {
            try
            {
                IList<ProdutosModels> list = new List<ProdutosModels>();
                string queryString;
                if (pro_id == 0)
                {
                    queryString = "SELECT pro_id, pro_nome, pro_descricao, pro_arquivo FROM produtos ORDER BY pro_id DESC";
                }
                else
                {
                    queryString = "SELECT pro_id, pro_nome, pro_descricao, pro_arquivo FROM tag WHERE pro_id = " + pro_id;
                }
                MySqlCommand query = new MySqlCommand(queryString, db);
                db.Open();
                MySqlDataReader itens = query.ExecuteReader();
                if (itens.HasRows)
                {
                    while (itens.Read())
                    {
                        list.Add(new ProdutosModels
                        {
                            pro_id = Convert.ToInt32(itens["pro_id"]),
                            pro_nome = itens["pro_nome"].ToString(),
                            pro_descricao = itens["pro_descricao"].ToString(),
                            pro_arquivo = itens["pro_arquivo"].ToString()
                        });
                    }
                }
                else
                {
                    list.Add(new ProdutosModels
                    {
                        pro_id = Convert.ToInt32(0),
                        pro_nome = "Nada foi encontrado".ToString(),
                        pro_descricao = "Não existe nenhum item cadastrado".ToString()
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

        public IEnumerable<ResponseModels> Delete(int pro_id)
        {
            IList<ResponseModels> list = new List<ResponseModels>();
            try
            {
                string queryString = "DELETE FROM produtos WHERE pro_id = " + pro_id;
                MySqlCommand query = new MySqlCommand(queryString, db);
                db.Open();
                if (Convert.ToInt32(query.ExecuteNonQuery()) > 0)
                {
                    list.Add(new ResponseModels
                    {
                        HttpStatusCode = 200,
                        SystemCode = 1,
                        Mensagem = "Item excluído com sucesso",
                        Container = 1
                    });
                }
                else
                {
                    list.Add(new ResponseModels
                    {
                        HttpStatusCode = 400,
                        SystemCode = 2,
                        Mensagem = "Não foi possível excluir item",
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
    }
}