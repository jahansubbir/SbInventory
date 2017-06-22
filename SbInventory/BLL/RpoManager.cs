using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SbInventory.DAL;
using SbInventory.Models;

namespace SbInventory.BLL
{
    public class RpoManager
    {
        RpoGateway rpoGateway =new RpoGateway();

        public string SaveRpo(RPO rpo)
        {
            if (rpoGateway.GetRpos().Find(a=>a.Name==rpo.Name)==null)
            {
                if (rpoGateway.SaveRpo(rpo) > 0)
                {
                    return rpo.Name + " has been saved successfully";
                }
                else return "failed";
            }
            return "Rpo already exists";
        }

        public string EditRpo(RPO rpo)
        {
            if (rpoGateway.EditRpo(rpo)>0)
            {
                return "Edited";
            }
            return "failed";
        }

        public string DeleteRpo(int id)
        {
            if (rpoGateway.DeleteRpo(id)>0)
            {
                return "Deleted";
            }
            return "Failed";
        }
    }
}