using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GyakolroWebApp.Controllers
{
    public class ListaModelController : Controller
    {
        
        //
        // GET: /ListaModel/

        public ActionResult Index()
        {       
            
            GyakolroWebApp.Models.ListaModel lm = new Models.ListaModel();
            try
            {
                foreach (var fr in lm.FoRootCsp())
                {               
                lm.froot.Add(fr);
                foreach (var rcs in lm.rootCsp(fr.mhID))
                {
                    lm.root.Add(rcs);
                    foreach (var gycs in lm.gyerekCsp(rcs.mhID))
                    {
                        lm.gy.Add(gycs);
                        foreach (var gyr in lm.gyerekCsp(gycs.mhID))
                        {
                            lm.gyRoot.Add(gyr);
                        }
                    }
                }
                }
                ViewBag.dlista = lm.dolgozoLista();
            }
            catch (Exception ex)
            {
                ViewBag.Hiba = "Hiba a feldolgozásában! " + ex.Message;
            }
            
            ViewBag.lista = lm.root;
            ViewBag.allista = lm.gy;
            ViewBag.GyAllista = lm.gyRoot;
            return View();
        }
    }
}