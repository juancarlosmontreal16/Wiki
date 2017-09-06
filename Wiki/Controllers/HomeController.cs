﻿using System;
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

        //AjouterArticle
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
                    ViewBag.Erreur = "L'article a ete ajoute";
                    ViewBag.Article = repo.Find(article.Titre);
                }
                else
                {
                    ViewBag.Erreur = "L'article n'a pas ete ajoute";
                    return View("Error");
                }
            }

            return View("Index");
        }
	}
}