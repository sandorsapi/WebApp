using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace GyakolroWebApp.Controllers
{
    public class HomeController : Controller
    {
        
        CegContextDataContext ce = new CegContextDataContext();

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult UjKategoriaFelvitel(Munkahely mModel)
        {
            int szId = 0;
            GyakolroWebApp.Models.ListaModel lm = new Models.ListaModel();
            try
            {                             
                if (mModel.munkahelyNev != null)
                {
                    if (!lm.mKeresNev(mModel.munkahelyNev))
                    {
                        lm.mUjFelvitel(szId, mModel.munkahelyNev);
                        foreach (var m in lm.mKeresNevSzerint(mModel.munkahelyNev))
                        {
                            lm.mModositasFokat(m.mhID, mModel.munkahelyNev);
                        }
                        ViewBag.Hiba = "Felvitel végrehajtva!";
                    }
                    else
                    {
                        ViewBag.Hiba = "A munkahely már szerepel az adatbázisban!";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Hiba = "Hiba a felvitel feldolgozásában! " + ex.Message;
            }        
            return View();
        }

        public ActionResult MunkahelyCreate(Munkahely mModel, string mnev)
        {
            int id = 0;
            GyakolroWebApp.Models.ListaModel lm = new Models.ListaModel();            
            try
            {
                var m = lm.mKatID(mnev);
                ViewBag.mhely = mnev;
                foreach (var mid in m)
                {
                    id = mid.mhID;
                }
                if (mModel.munkahelyNev != null)
                {
                    if (!lm.mKeresNev(mModel.munkahelyNev))
                    {
                        lm.mUjFelvitel(id, mModel.munkahelyNev);
                        ViewBag.Hiba = "Felvitel végrehajtva!";
                    }
                    else
                    {
                        ViewBag.Hiba = "A munkahely már szerepel az adatbázisban!";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Hiba = "Hiba a felvitel feldolgozásában! " + ex.Message;
            }        
            return View();
        }

        public ActionResult MunkahelyEdit(int id, string nev)
        {
            ViewBag.nev = nev;
            GyakolroWebApp.Models.ListaModel lm = new Models.ListaModel();
            try
            {
                if (lm.mKeres(id, nev))
                {
                    ViewBag.Hiba = "Még nem módosítottad az adatot!";
                }
                else
                {
                    lm.mModositas(id, nev);
                    ViewBag.Hiba = "Módosítás végrehajtva!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Hiba = "Hiba a módosítás feldolgozásában! " + ex.Message;
            }
            return View();            
        }
 
        public ActionResult MunkahelyDelete(int id)
        {
            try
            {
                GyakolroWebApp.Models.ListaModel lm = new Models.ListaModel();
                lm.mDelete(id);
                ViewBag.Hiba = "Törlés végrehajtva!";
            }
            catch (Exception ex)
            {
                ViewBag.Hiba = "Hiba a törlés feldolgozásában! " + ex.Message;
            }
            return View();
        }

        public ActionResult Munkahely(int id, int szID, string st, string neve)
        {
            String FkNev = null;
            GyakolroWebApp.Models.ListaModel lm = new Models.ListaModel();
            try
            {
                foreach (var f in lm.FoKatNev(szID))
                {
                    FkNev = f.munkahelyNev;
                }
                @ViewBag.Id = id;
                @ViewBag.mhnev = FkNev;
                @ViewBag.neve = neve;
                @ViewBag.st = st;
            }
            catch (Exception ex)
            {
                ViewBag.Hiba = "Hiba a feldolgozásában! " + ex.Message;
            }
            return View();
        }

        public ActionResult Dolgozo(int id, string dNev, string mNev)
        {
            GyakolroWebApp.Models.ListaModel lm = new Models.ListaModel();
            try
            {
                foreach (var dLista in lm.doldozoListaKivalaszt(id, dNev))
                {
                    ViewBag.ID = dLista.dolgozoID;
                    ViewBag.mhNev = dLista.Munkahely.munkahelyNev;
                    ViewBag.dolgozoNev = dLista.dolgozoNev;
                    ViewBag.email = dLista.email;
                    ViewBag.statusz = dLista.dstatusz;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Hiba = "Hiba a feldolgozásában! " + ex.Message;
            }
            return View();
        }

        public ActionResult DolgozoDelete(int id)
        {
            try
            {
                GyakolroWebApp.Models.ListaModel lm = new Models.ListaModel();
                lm.dDelete(id);
                ViewBag.Hiba = "Törlés végrehajtva!";
            }
            catch (Exception ex)
            {
                ViewBag.Hiba = "Hiba a törlés feldolgozásában! " + ex.Message;
            }
            return View();
        }

        public ActionResult DolgozoEdit(string nev, int id)
        {
            ViewBag.nev = nev;
            GyakolroWebApp.Models.ListaModel lm = new Models.ListaModel();
            try
            {
                if (lm.dKeres(id, nev))
                {
                    ViewBag.Hiba = "Még nem módosítottad az adatot!";
                }
                else
                {
                    lm.dModositas(id, nev);
                    ViewBag.Hiba = "Módosítás végrehajtva!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Hiba = "Hiba a módosítás feldolgozásában! " + ex.Message;
            }
            return View();
        }

        public ActionResult DolgozoCreate(Dolgozo dModel, string m_hely)
        {
            int id = 0;
            GyakolroWebApp.Models.ListaModel lm = new Models.ListaModel();
            try
            {
                var m = lm.mKatID(m_hely);
                ViewBag.mhely = m_hely;
                foreach (var mid in m)
                {
                    id = mid.mhID;
                }
                if (dModel.dolgozoNev != null || dModel.email != null)
                {
                    if (!lm.dKeresNev(dModel.dolgozoNev, dModel.email))
                    {
                        lm.dUjFelvitel(id, dModel.dolgozoNev, dModel.email);
                        ViewBag.Hiba = "Felvitel végrehajtva!";
                    }
                    else
                    {
                        ViewBag.Hiba = "A dolgozó valamelyik adata szerepel már az adatbázisban!";
                    }
                }                
            }
            catch (Exception ex)
            {
                ViewBag.Hiba = "Hiba a felvitel feldolgozásában! " + ex.Message;
            }
            return View();
        }      
    }
}
