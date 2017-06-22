using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SbInventory.Models;

namespace SbInventory.DAL
{
    public class ReceiveGateway
    {
        SbDBEntities db=new SbDBEntities();

        public int Receive(SBReceive sbReceive)
        {
             db.SBReceives.Add(sbReceive);
               return db.SaveChanges();
        }

        public int EditReceives(SBReceive sbReceive)
        {
            db.Entry(sbReceive).State = EntityState.Modified;
           return db.SaveChanges();
        }

        public IQueryable<SBReceive> GetReceiveViewModel()
        {
            var sbreceives = db.SBReceives.Include(s => s.SBDSB);
            return sbreceives;
        }

        public List<SBReceive> GetReceives()
        {
            List<SBReceive> sbreceives = db.SBReceives.ToList();
            return sbreceives;
        } 
    }
}