using RusTarihi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RusTarihi.Controllers
{
    public class BaseController : Controller
    {
        protected RusTarihiDBEntities context { get; private set; }

        public BaseController()
        {
            context = new RusTarihiDBEntities();
        }

        protected Models.Members CurrentUser()
        {
            if (Session["LogonUser"] == null)
            {
                return null;
            }

            return (Models.Members)Session["LogonUser"];
        }

        protected int CurrentUserId()
        {
            if (Session["LogonUser"] == null)
            {
                return 0;
            }

            return ((Models.Members)Session["LogonUser"]).ID;
        }
    }
}