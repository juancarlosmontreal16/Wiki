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
                case "Find":
                    if (a.Titre == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    a = repo.Find(a.Titre);
                    if (a == null)
                    {
                        return HttpNotFound();
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

            //return View(repo.GetArticles());
            Article b = new Article();
            b.Contenu = "asdasdasdad";
            b.DateModification = DateTime.Now;
            b.Revision = 0;
            b.Titre = "Nuevo";
            b.IdContributeur = 1;
            List<Article> art = new List<Article>();
            art.Add(b);
            return View(art);
        }
     
    }
}
