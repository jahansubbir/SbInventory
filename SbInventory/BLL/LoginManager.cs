using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SbInventory.DAL;
using SbInventory.Models;

namespace SbInventory.BLL
{
    public class LoginManager
    {
        LoginGateway loginGateway=new LoginGateway();
        public string Login(RPOLogin rpoLogin)
        {
            if (loginGateway.Login(rpoLogin)!=null)
            {
                return "Logged In";
            }
            return "Invalid User or Password";
        }

        public string Register(RPOLogin login)
        {
            if (loginGateway.IsExist(login).Count==0)
            {
                if (loginGateway.Register(login)>0)
                {
                    return "Registerd successfully";
                }
                else
                {
                    return "Registration Failed";
                }
            }
            return "Sorry! You cannot Register more than once";
        }
    }
}