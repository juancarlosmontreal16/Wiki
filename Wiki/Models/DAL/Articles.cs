using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Collections.Generic;
using Wiki.Models.DAL;
using Wiki.Models.Biz;

namespace Wiki.Models.DAL
{
    public class Articles
    {
        // Auteurs:
        public bool Add(Article a)
        {
            bool TEST = true;
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                var cmd = new SqlCommand("AddArticle", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Titre", SqlDbType.NVarChar).Value = a.Titre;
                cmd.Parameters.Add("@Contenu", SqlDbType.NVarChar).Value = a.Contenu;
                cmd.Parameters.Add("@IdContributeur", SqlDbType.Int).Value = 1;

                try
                {
                    cmd.ExecuteNonQuery();
                    return TEST;
                }
                catch (Exception e)
                {
                    string Msg = e.Message;
                    TEST = false;
                }
                finally
                {
                    conn.Close();
                }
                return TEST;
            }
        }

        // Auteurs:
        public Article Find(string titre)
        {
            string cStr = ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                string requete = "FindArticle";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Titre", SqlDbType.NVarChar).Value = titre;

                try
                {
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    var article = new Article();
                    if (dataReader.Read())
                    {
                        article.Titre = (string)dataReader["Titre"];
                        article.Contenu = (string)dataReader["Contenu"];
                        article.Revision = (int)dataReader["Revision"];
                        article.IdContributeur = (int)dataReader["IdContributeur"];
                        article.DateModification = (DateTime)dataReader["DateModification"];
                    }
                    else
                        return null;

                    dataReader.Close();
                    return article;
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                    return null;
                }
                finally
                {
                    cnx.Close();
                }
            }
        }


        // Auteurs: Vincent Simard, Phan Ngoc Long Denis, Floyd Ducharme, Pierre-Olivier Morin
        public IList<string> GetTitres()
        {
            string cStr = ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                string requete = "GetTitresArticles";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    IList<string> ListeTitre = new List<string>();
                    while (dataReader.Read())
                    {
                        string t = (string)dataReader["Titre"];
                        ListeTitre.Add(t);
                    }
                    dataReader.Close();

                    return ListeTitre;
                }
                finally
                {
                    cnx.Close();
                }
            }
        }

        // Auteurs: Alexandre, Vincent, William et Nicolas
        public IList<Article> GetArticles()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                var cmd = new SqlCommand("GetArticles", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    var dataReader = cmd.ExecuteReader();
                    var articles = new List<Article>();

                    while (dataReader.Read())
                    {
                        var article = new Article();

                        article.Titre = (string)dataReader["Titre"];
                        article.Contenu = (string)dataReader["Contenu"];
                        article.Revision = (int)dataReader["Revision"];
                        article.IdContributeur = (int)dataReader["IdContributeur"];
                        article.DateModification = (DateTime)dataReader["DateModification"];

                        articles.Add(article);
                    }

                    return articles;
                }
                catch
                {
                    return null;
                }
            }
        }


        // Auteurs:
        public bool Update(Article a)
        {
            bool TEST = true;
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                var cmd = new SqlCommand("UpdateArticle", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Titre", SqlDbType.NVarChar).Value = a.Titre;
                cmd.Parameters.Add("@Contenu", SqlDbType.NVarChar).Value = a.Contenu;
                cmd.Parameters.Add("@IdContributeur", SqlDbType.Int).Value = 1;

                try
                {
                    cmd.ExecuteNonQuery();
                    return TEST;
                }
                catch (Exception e)
                {
                    string Msg = e.Message;
                    TEST = false;
                }
                finally
                {
                    conn.Close();
                }
                return TEST;
            }
        }



        // Auteurs:
        public bool Delete(string titre)
        {
            bool TEST = true;
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                var cmd = new SqlCommand("DeleteArticle", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Titre", SqlDbType.NVarChar).Value = titre;

                try
                {
                    cmd.ExecuteNonQuery();
                    return TEST;
                }
                catch (Exception e)
                {
                    string Msg = e.Message;
                    TEST = false;
                }
                finally
                {
                    conn.Close();
                }
                return TEST;
            }
        }



        private string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString; }
        }

    }
}