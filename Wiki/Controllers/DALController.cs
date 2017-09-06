using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wiki.Models.DAL;
using Wiki.Models.Biz;

namespace Wiki.Controllers
{

    public class DALController : Controller
    {
        Articles repo = new Articles();

        [ValidateInput(false)]
        public ActionResult Index(String operation, Article a)
        {
            switch (operation)
            {
                case "Consulter":
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
                    break;
                case "Update":
                    if (ModelState.IsValid)
                    {
                        a.IdContributeur = 1;
                        repo.Update(a);
                    }
                    break;
                case "Add":
                    if (ModelState.IsValid)
                    {
                        a.IdContributeur = 1;
                        repo.Add(a);
                    }
                    break;
                case "Delete":
                    repo.Delete(a.Titre);
                    break;
            }
            return View();
        }
     
    }
}
