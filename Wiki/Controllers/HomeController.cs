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

        [ValidateInput(false)]
        public ActionResult Index(Article a)
        {
            if (Request.HttpMethod == "POST")
            {
                if (a.Titre == null)
                {
                    ViewBag.Erreur = "Erreur : Vous devez saisir un titre !!!";
                    return View();
                }

                string titre = a.Titre;
                a = repo.Find(a.Titre);
                if (a == null)
                {
                    ViewBag.Erreur = "L'article n'existe pas";
                    ViewBag.Titre = titre;
                }
                ViewBag.Article = a;
            }

            return View();
        }
        
        //Donne l'article en question
        public ActionResult Partial_Article(Article article)
        { 
            return PartialView(article);
        }

        //Ajouter une Article
        [HttpGet]
        public ActionResult AjouterArticle(string titre)
        {
            Article a = new Article();
            a.Titre = titre;
            return View(a);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjouterArticle(Article article)
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

        //Donne la liste des matieres
        public ActionResult Partial_ListeMatieres() { return PartialView(repo); }

        //Trouve les details d'un article selectionne dans la table des matieres
        [HttpGet]
        public ActionResult Details(string titre)
        {
            ViewBag.Article = repo.Find(titre);
            return View("Index");
        }

        //Modifier l'article
        [HttpGet]
        public ActionResult Modifier(string titre) { return View(repo.Find(titre)); }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Modifier(Article article, string operation)
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

        //Supprimer
        [HttpPost]
        public ActionResult Supprimer(string titre)
        {
            if (repo.Delete(titre))
            {
                //return View("Index");
                return RedirectToAction("Index");
            }
            ViewBag.Erreur = "L'article n'a pas ete supprime";
            return View("Error");
        }
	}
}