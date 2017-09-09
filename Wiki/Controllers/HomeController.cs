using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wiki.Models.Biz;
using Wiki.Models.DAL;

namespace Wiki.Controllers
{
    public class HomeController : Controller
    {
        Articles repo = new Articles();


        /*********************************************** INDEX ************************************************/
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Index() //Daily
        {
            ViewBag.Article = null;
            return View();
        }

        [ValidateInput(false)]
        [Route("home/Index/{titre}")]
        public ActionResult Index(string titre) //Daily
        {
            if (titre != "")
            {
                Article a = repo.Find(titre);
                if (a == null)
                    ViewBag.Erreur = "L'article n'existe pas";
                else
                    ViewBag.Article = a;
            }
            return View();
        }

        /***************************************** AjouterArticle *********************************************/
        //Ajouter une Article
        public ActionResult Ajouter(string titre) //Daily
        {
            Article a = new Article();
            a.Titre = titre;
            return View(a);
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ajouter(Article article) //Daily
        {
            if (ModelState.IsValid)
            {
                if (repo.Add(article))
                {
                    ViewBag.Erreur = "L'article a été ajouté";
                    ViewBag.Article = repo.Find(article.Titre);
                }
                else
                {
                    ViewBag.Erreur = "L'article n'a pas été ajouté";
                    return View("Error");
                }
            }

            return View("Index");
        }

        /*********************************************** Details **********************************************/
        //Trouve les details d'un article selectionne dans la table des matieres
        public ActionResult Details(string titre) //Daily
        {
            Article a = repo.Find(titre);
            if (a == null)
            {
                ViewBag.Erreur = "L'article n'existe pas";
                ViewBag.NouvelleTitre = titre;
            }
                
            else
                ViewBag.Article = a;
            return View("Index");
        }

        /********************************************** Modifier ************************************************/
        [HttpGet]
        public ActionResult Modifier(string titre) { return View(repo.Find(titre)); } //Daily

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Modifier(Article article, string operation) //Daily
        {
            if (operation == "Modifier")
            {
                if (ModelState.IsValid)
                {
                    article.IdContributeur = 1;
                    if (repo.Update(article))
                    {
                        ViewBag.Erreur = "L'article a été modifié";
                        ViewBag.Article = article;
                    }
                    else
                    {
                        ViewBag.Erreur = "L'article n'a pas été modifié";
                        return View("Error");
                    }
                }
            }
            else
            {
                return View("Supprimer", article);
            }

            return View("Index");
        }

        /********************************************* Supprimer *********************************************/
        public ActionResult Supprimer(string titre) //Juan Carlos
        {
            if (Request.HttpMethod == "GET")
            {
                Article a = repo.Find(titre);
                if (a == null)
                {
                    ViewBag.Erreur = "L'article n'a pas ete supprime";
                    return View("Error");
                }
                return View(a);
            }
            else
            {
                if (repo.Delete(titre))
                {
                    return RedirectToAction("Index", new { titre = "" });
                }
                ViewBag.Erreur = "L'article n'a pas ete supprime";
                return View("Error");
            }
        }

        /**************************************** Methodes pour s'appuyer ************************************/
        //Donne l'article en question
        public ActionResult Partial_Article(Article article) //Daily
        {
            return PartialView(article);
        }

        //Donne la liste des matieres
        public ActionResult Partial_ListeMatieres() { return PartialView(repo); } //Juan Carlos
	}
}