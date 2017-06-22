using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SbInventory.Models;

namespace SbInventory.DAL
{
    public class LoginGateway
    {
        SbDBEntities db=new SbDBEntities();
        public RPOLogin Login(RPOLogin rpoLogin)
        {
            if (db.RPOLogins.ToList().Find(a => a.UserId.Equals(rpoLogin.UserId, StringComparison.InvariantCultureIgnoreCase) && a.Password==rpoLogin.Password)!=null)
            {
                return rpoLogin;
            }
            else
            {
                return null;
            }//if (
            //    db.RPOLogins.ToList().Any(
            //        a => a.RPOId == rpoLogin.RPOId && a.UserId == rpoLogin.UserId && a.Password == rpoLogin.Password))
            //{
            //    RPOLogin user = new RPOLogin()
            //    {
            //        RPOId = rpoLogin.RPOId,
            //        UserId = rpoLogin.UserId
            //    };
            //    return user;
            //}
            //else
            //{
            //    return null;
            //}
        }

        public List<RPOLogin> IsExist(RPOLogin login)
        {
            return db.RPOLogins.ToList();
        }

        public int Register(RPOLogin login)
        {
            db.RPOLogins.Add(login);
            return db.SaveChanges();
        }
    }
}