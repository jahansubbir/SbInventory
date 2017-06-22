using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SbInventory.Models;

namespace SbInventory.DAL
{
    public class RpoGateway
    {
        SbDBEntities db=new SbDBEntities();

        public int SaveRpo(RPO rpo)
        {
            db.RPOes.Add(rpo);
            return db.SaveChanges();
        }

        public int EditRpo(RPO rpo)
        {
            db.Entry(rpo).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int DeleteRpo(int id)
        {
            RPO rpo = db.RPOes.Find(id);
            db.RPOes.Remove(rpo);
           return db.SaveChanges();
        }

        public List<RPO> GetRpos()
        {
            return db.RPOes.ToList();
        } 
    }
}