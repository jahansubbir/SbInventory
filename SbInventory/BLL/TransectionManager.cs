using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SbInventory.DAL;
using SbInventory.Models;

namespace SbInventory.BLL
{
    public class TransectionManager
    {
        DispatchGateway dispatchGateway=new DispatchGateway();
        ReceiveGateway receiveGateway=new ReceiveGateway();

        public string Dispatch(SBDispatch sbDispatch)
        {
            if (IsReceived(sbDispatch.EID)==null)
            {
                if (IsDispatched(sbDispatch.EID)!=null)
                {
                    return "Already dispatched";
                }
                else
                {
                    sbDispatch.Remarks = "Dispatch";
                    sbDispatch.Status = "New Dispatch";
                    if (dispatchGateway.Dispatch(sbDispatch)>0)
                    {
                        return "Enrollment Id " + sbDispatch.EID + " has been dispatched ";
                    }
                    return "Failed";
                }
            }
            else
            {
                if (GetReceives().Last(a=>a.EID==sbDispatch.EID).Status=="Positive")
                {
                    return "This File has already been dealt";
                }
                else
                {
                    if (GetDispatches().Count(a => a.EID==sbDispatch.EID)==GetReceives().Count(a => a.EID==sbDispatch.EID))
                    {
                        sbDispatch.Status = "Re-Dispatched";
                        if (dispatchGateway.Dispatch(sbDispatch)>0)
                        {
                            return "Enrollment Id " + sbDispatch.EID + "has been re-dispatched";
                        }
                        return "failed";
                    }
                    return "Wait for the file to be received";
                }
            }
        }

        //public string EditDispatch(SBDispatch sbDispatch)
        //{
            
        //}

        public List<SBDispatch> GetDispatches()
        {
            return dispatchGateway.GetDispatches();
        }

        public IQueryable<SBDispatch> GetDispatchViewModel()
        {
            return dispatchGateway.GetDispatchViewModel();
        }

        public string Receive(SBReceive sbReceive)
        {
            if (IsDispatched(sbReceive.EID)==null)
            {
                return "File with Enrollment Id" + sbReceive.EID + "has not been dispatched yet";
            }
            else
            {
                if (IsReceived(sbReceive.EID)==null)
                {
                    if (receiveGateway.Receive(sbReceive)>0)
                    {
                        return "File with Enrollment Id " + sbReceive.EID + "has been received " + sbReceive.Status +
                               "ly";
                    }
                    return "Failed";
                }
                else
                {
                    if (GetReceives().Last(a => a.EID == sbReceive.EID).Status=="Positive")
                    {
                        return "File with Enrollment Id " + sbReceive.EID + "has already been dealt";
                    }
                    else
                    {
                        if (GetDispatches().Count(a => a.EID == sbReceive.EID) ==GetReceives().Count(a => a.EID == sbReceive.EID)+1)
                        {
                            if (receiveGateway.Receive(sbReceive) > 0)
                            {
                                return "File with Enrollment Id " + sbReceive.EID + "has been received " + sbReceive.Status +
                                       "ly";
                            }
                            return "Failed";
                        }
                        else
                        {
                            return "Wait for file to be dispatched";
                        }
                    }
                    
                }
            }
        }

        public List<SBReceive> GetReceives()
        {
            return receiveGateway.GetReceives();
        }

        public IQueryable<SBReceive> GetReceiveViewModels()
        {
            return receiveGateway.GetReceiveViewModel();
        }

        public SBDispatch IsDispatched(string eId)
        {
            SBDispatch dispatch = GetDispatches().Find(a => a.EID == eId);
            return dispatch;
        }

        public SBReceive IsReceived(string eId)
        {
            SBReceive receive = GetReceives().Find(a => a.EID == eId);
            return receive;
        }
    }
}