﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OperatingManagement.WebKernel.Basic;
using OperatingManagement.WebKernel.Route;
using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer;
using OperatingManagement.DataAccessLayer.PlanManage;
using OperatingManagement.Framework;
using System.Web.Security;
using System.Xml;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class YJJHEdit : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    string sID = Request.QueryString["id"];
                    HfID.Value = sID;
                    BindJhTable(sID);
                    BindXML();
                }
            }
        }
        private void BindJhTable(string sID)
        {
            List<JH> jh = (new JH()).SelectByIDS(sID);
            txtPlanStartTime.Text = jh[0].StartTime.ToShortTimeString();
            txtPlanEndTime.Text = jh[0].EndTime.ToShortTimeString();
            HfFileIndex.Value = jh[0].FileIndex;
        }
        private void BindXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HfFileIndex.Value);
            XmlNode root = xmlDoc.SelectSingleNode("应用研究工作计划/XXFL");
            txtXXFL.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("应用研究工作计划/JXH");
            txtJXH.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("应用研究工作计划/SysName");
            txtSysName.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("应用研究工作计划/StartTime");
            txtStartTime.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("应用研究工作计划/EndTime");
            txtEndTime.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("应用研究工作计划/Task");
            txtTask.Text = root.InnerText;

        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.Edit";
            this.ShortTitle = "编辑计划";
            base.OnPageLoaded();
            this.AddJavaScriptInclude("scripts/pages/YJJHEdit.aspx.js");
        }

    }
}