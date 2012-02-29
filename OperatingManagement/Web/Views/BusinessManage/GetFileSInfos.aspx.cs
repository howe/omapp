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
using OperatingManagement.DataAccessLayer.BusinessManage;
using System.Web.Security;
using System.Data;
using ServicesKernel.File;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class GetFileSInfos : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitialPageData();
        }

        protected void btnHidRSendFile_Click(object sender, EventArgs e)
        {
            string strRID = txtRID.Text;
            string strStatus = txtStatus.Text;
            //status=0已提交发送；status=1发送中，这两种情况不能重发
            if (strStatus == "0" || strStatus == "1")
                return;
            if (strRID.Equals(string.Empty))
                return;
            ReSendFile(Convert.ToInt32(strRID));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime dtFrom = DateTime.MinValue;
            DateTime dtTo = DateTime.MinValue;
            if (txtFrom.Text != "")
                dtFrom = DateTime.Parse(txtFrom.Text);
            if (txtTo.Text != "")
                dtTo = DateTime.Parse(txtTo.Text);

            FileSendInfo oSend = new FileSendInfo();
            oSend.InfoTypeID = Convert.ToInt32(ddlXXType.SelectedItem.Value);
            List<FileSendInfo> listDatas = oSend.Search(dtFrom, dtTo);
            BindDataSource(listDatas);
        }

        private void InitialPageData()
        {
            FileSendInfo oSend = new FileSendInfo();
            List<FileSendInfo> listDatas = oSend.SelectAll();
            BindDataSource(listDatas);
        }

        private void BindDataSource(List<FileSendInfo> listDatas)
        {
            cpPager.DataSource = listDatas;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpDatas;
            rpDatas.DataSource = cpPager.DataSourcePaged;
            rpDatas.DataBind();
        }

        private bool ReSendFile(int sendInfoID)
        {
            return true;
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_FSendInfo.View";
            this.ShortTitle = "查看文件发送记录";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/BusinessManage/GetFileSInfos.aspx.js");
        }
    }
}