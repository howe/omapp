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
using OperatingManagement.Framework;
using OperatingManagement.DataAccessLayer.PlanManage;
using System.Web.Security;
using System.Data;

namespace OperatingManagement.Web.PlanManage
{
    public partial class NotSpaceTaskList : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnSend.Attributes.Add("onclick", "javascript:return confirm('确定要发送所选数据吗?');");
            }
        }

        public override void OnPageLoaded()
        {
            this.ShortTitle = "查看卫星轨道根数";
            this.SetTitle();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            BindGridView();
        }

        //绑定列表
        void BindGridView()
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            DataSet objDs = new DataSet();
            objDs = (new YDSJ()).GetYDSJListByDate(startDate, endDate,"2");
            gvList.DataSource = objDs;            
            gvList.DataBind();
            if (objDs.Tables[0].Rows.Count > 0)
            {
                btnSend.Visible = true;
            }
            else
            {
                btnSend.Visible = false;
            }
        }

        void BindRadDestination()
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            pnlData.Visible = false;
            pnlDestination.Visible = true;
            BindRadDestination();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlData.Visible = true;
            pnlDestination.Visible = false;
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if ("ShowDetail" == e.CommandName)
            {
                int idx = Int32.Parse(e.CommandArgument.ToString());
                int ydsjID = Convert.ToInt32(gvList.DataKeys[idx][0]);

                Response.Redirect(string.Format("YDSJDetail.aspx?ydsjid={0}", ydsjID));
            }
        }
    }
}