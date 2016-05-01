using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GyakolroWebApp.Models
{
    
    public class ListaModel
    {
        CegContextDataContext ce = new CegContextDataContext();
        public List<Munkahely> froot = new List<Munkahely>();
        public List<Munkahely> root = new List<Munkahely>();
        public List<Munkahely> gy = new List<Munkahely>();
        public List<Munkahely> gyRoot = new List<Munkahely>();
       

        public List<Munkahely> FoRootCsp()
        {
            List<Munkahely> mLista = new List<Munkahely>();
            var adatList = ce.Munkahelies.Where(m => m.mhstatusz == "aktiv").Select(m => m);
            foreach (var al in adatList)
            {
                mLista.Add(al);
            }
            return mLista;
        }

        public List<Munkahely> FoKatNev(int szID)
        {
            List<Munkahely> mLista = new List<Munkahely>();
            var adatList = ce.Munkahelies.Where(m => m.mhstatusz == "aktiv" && m.mhID == szID).Select(m => m);
            foreach (var al in adatList)
            {              
                    mLista.Add(al);                              
            }
            return mLista;
        }
        
        public List<Munkahely> mKatID(string m_nev)
        {
            List<Munkahely> mLista = new List<Munkahely>();
            var adatList = ce.Munkahelies.Where(m => m.mhstatusz == "aktiv" && m.munkahelyNev == m_nev).Select(m => m);
            foreach (var al in adatList)
            {
                mLista.Add(al);
            }
            return mLista;
        }

        public int rootCount()
        {
            int db = 0;
            var adatList = ce.Munkahelies.Where(m => m.mhstatusz == "aktiv" && m.mhID == m.szuloID).Select(m => m);
            db = adatList.Count();            
            return db;
        }

        public List<Munkahely> rootCsp(int id)
        {
            List<Munkahely> mLista = new List<Munkahely>();
            var adatList = ce.Munkahelies.Where(m => m.mhstatusz == "aktiv" && m.mhID == m.szuloID && m.mhID == id).Select(m => m);
            foreach (var al in adatList)
            {
                mLista.Add(al);
            }
            return mLista;
        }

        public List<Munkahely> gyerekCsp(int id)
        {
            List<Munkahely> mLista = new List<Munkahely>();
            var adatList = ce.Munkahelies.Where(m => m.mhstatusz == "aktiv" && m.mhID != m.szuloID && m.szuloID == id).Select(m => m);
            foreach (var al in adatList)
            {
                mLista.Add(al);
            }
            return mLista;
        }
     

        public List<Dolgozo> dolgozoLista()
        {
            List<Dolgozo> dLista = new List<Dolgozo>();
            var dolg = ce.Dolgozos.Where(d => d.dstatusz == "aktiv").Select(d => d);
            foreach (var dl in dolg)
            {
                dLista.Add(dl);
            }
            return dLista;
        }

        public List<Dolgozo> doldozoListaKivalaszt(int id, string nev)
        {
            List<Dolgozo> dLista = new List<Dolgozo>();
            var dolg = ce.Dolgozos.Where(d => d.dstatusz == "aktiv" && d.mh_id == id && d.dolgozoNev == nev).Select(d => d);
            foreach (var dl in dolg)
            {
                dLista.Add(dl);
            }
            return dLista;
        }

         public void mUjFelvitel(int szulo_id, string munkahely_neve)
        {
            Munkahely mh = new Munkahely { szuloID = szulo_id, munkahelyNev = munkahely_neve, mhstatusz = "aktiv"};
            ce.Munkahelies.InsertOnSubmit(mh);
            ce.SubmitChanges();
        }

        public void mModositas(int id, string neve)
        {
            var kivadat = ce.Munkahelies.Where(m => m.mhID == id).Select(m => m);            
            if (kivadat != null)
            {
                foreach (var m in kivadat)
                {
                    m.munkahelyNev = neve;                               
                }
                ce.SubmitChanges();  
            }
        }

        public void mModositasFokat(int id, string neve)
        {
            var kivadat = ce.Munkahelies.Where(m => m.munkahelyNev == neve).Select(m => m);
            if (kivadat != null)
            {
                foreach (var m in kivadat)
                {
                    m.munkahelyNev = neve;
                    m.szuloID = id;
                }
                ce.SubmitChanges();
            }
        }

        public void dModositas(int id, string neve)
        {
            var kivadat = ce.Dolgozos.Where(m => m.dolgozoID == id).Select(m => m);
            if (kivadat != null)
            {
                foreach (var m in kivadat)
                {                   
                        m.dolgozoNev = neve;                                                            
                }
                ce.SubmitChanges();
            }
        }


        public Boolean dKeresNev(string neve, string e_mail)
        {
            bool eredmeny = false;
            var kivadat = ce.Dolgozos.Where(m => m.dolgozoNev == neve || m.email == e_mail).Select(m => m);
            if (kivadat != null)
            {
                foreach (var m in kivadat)
                {
                    if (m.dolgozoNev == neve)
                    {
                        eredmeny = true;
                    }
                    else
                    {
                        eredmeny = false;
                    }
                }
            }
            return eredmeny;
        }

        public Boolean dKeres(int id, string neve)
        {
            bool eredmeny = false;
            var kivadat = ce.Dolgozos.Where(m => m.dolgozoID == id).Select(m => m);
            if (kivadat != null)
            {
                foreach (var m in kivadat)
                {
                    if (m.dolgozoNev == neve)
                    {
                        eredmeny = true;
                    }
                    else
                    {
                        eredmeny = false;
                    }
                }
            }
            return eredmeny;
        }

        public List<Munkahely> mKeresNevSzerint(string neve)
        {
            List<Munkahely> mList = new List<Munkahely>();
            var kivadat = ce.Munkahelies.Where(m => m.munkahelyNev == neve).Select(m => m);
            if (kivadat != null)
            {
                foreach (var m in kivadat)
                {
                    mList.Add(m);
                }
            }
            return mList;
        }


        public Boolean mKeresNev(string neve)
        {
            bool eredmeny = false;
            var kivadat = ce.Munkahelies.Where(m => m.munkahelyNev == neve).Select(m => m);
            if (kivadat != null)
            {
                foreach (var m in kivadat)
                {
                    if (m.munkahelyNev == neve)
                    {
                        eredmeny = true;
                    }
                    else
                    {
                        eredmeny = false;
                    }
                }
            }    
           
            return eredmeny;
        }

        public Boolean mKeres(int id, string neve)
        {
            bool eredmeny = false;
            var kivadat = ce.Munkahelies.Where(m => m.mhID == id).Select(m => m);
            if (kivadat != null)
            {
                foreach (var m in kivadat)
                {
                    if (m.munkahelyNev == neve)
                    {
                        eredmeny = true;
                    }
                    else
                    {
                        eredmeny = false;
                    }
                }
            }
            return eredmeny;
        }

        public void mDelete(int id)
        {            
                var kivadat = ce.Munkahelies.Where(m => m.mhID == id).Select(m => m).Single();
                if (kivadat != null)
                {
                    kivadat.mhstatusz = "törölve";
                    ce.SubmitChanges();
                }
        }

        public void dDelete(int id)
        {
            var kivadat = ce.Dolgozos.Where(d => d.dolgozoID == id).Select(m => m).Single();
            if (kivadat != null)
            {
                kivadat.dstatusz = "törölve";
                ce.SubmitChanges();
            }
        }

        public void dUjFelvitel(int id, string nev, string e_mail)
        {
            GyakolroWebApp.Models.DolgozoModel dm = new Models.DolgozoModel();     
            Dolgozo d = new Dolgozo { mh_id = id, dolgozoNev = nev, dstatusz = "aktiv", email = e_mail};
            ce.Dolgozos.InsertOnSubmit(d);
            ce.SubmitChanges();
        }
    }
}