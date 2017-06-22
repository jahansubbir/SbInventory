using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SbInventory.Models;

namespace SbInventory.DAL
{
    public class DispatchGateway
    {
        SbDBEntities db=new SbDBEntities();

        public int Dispatch(SBDispatch sbdispatch)
        {
            db.SBDispatches.Add(sbdispatch);
           return db.SaveChanges();
        }

        public int EditDispatch(SBDispatch sbDispatch)
        {
            db.Entry(sbDispatch).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public IQueryable<SBDispatch> GetDispatchViewModel()
        {
           IQueryable<SBDispatch> sbdispatches = db.SBDispatches.Include(s => s.SBDSB);
            return sbdispatches;
        }

        public int DeleteDispatch(int id)
        {
            SBDispatch sbdispatch = db.SBDispatches.Find(id);
            db.SBDispatches.Remove(sbdispatch);
            return db.SaveChanges();
        }

        public List<SBDispatch> GetDispatches()
        {
            List<SBDispatch> sbdispatches = db.SBDispatches.ToList();
            return sbdispatches;
        } 
    }
}