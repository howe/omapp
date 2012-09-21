﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.WebKernel.Basic;
using OperatingManagement.Framework.Core;
using System.Data;
using System.Drawing;

namespace OperatingManagement.Web.Views.UserAndRole
{
    public partial class RoleAdd : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindModules();
            }
        }
        void BindModules()
        {
            DataAccessLayer.System.Permission p = new DataAccessLayer.System.Permission();
            try
            {
                permissions = p.SelectAll();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("新增角色页面初始化BindModules出现异常，异常原因", ex));
            }
            finally { }
            var permissionModules = (from q in permissions
                                     select q.Module).ToList().DistinctBy(o => o.Id);
            rpModules.DataSource = permissionModules;
            rpModules.DataBind();
        }

        private List<DataAccessLayer.System.Permission> permissions = null;
        protected void rpModules_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (permissions == null)
                {
                    DataAccessLayer.System.Permission p = new DataAccessLayer.System.Permission();
                    try
                    {
                        permissions = p.SelectAll();
                    }
                    catch (Exception ex)
                    {
                        throw (new AspNetException("新增角色页面初始化ItemDataBound出现异常，异常原因", ex));
                    }
                    finally { }
                }
                var rpTasks = e.Item.FindControl("rpTasks") as Repeater;

                DataAccessLayer.System.Module m = e.Item.DataItem as DataAccessLayer.System.Module;

                if (rpTasks != null && m != null)
                {
                    rpTasks.DataSource = permissions.Where(o => o.Module.Id == m.Id);
                    rpTasks.DataBind();
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataAccessLayer.System.Role r = new DataAccessLayer.System.Role()
            {
                Note = txtNote.Text.Trim(),
                RoleName = txtName.Text.Trim()
            };
            string[] permissions = hfModuleTasks.Value.Split(new string[] { "][" }, StringSplitOptions.RemoveEmptyEntries);
            string strPerms = string.Empty;
            string msg = string.Empty;
            if (permissions.Length > 0)
            {
                foreach (var s in permissions)
                {
                    strPerms += s.TrimStart('[').TrimEnd(']') + ",";
                }
                if (strPerms != string.Empty)
                {
                    strPerms = strPerms.Substring(0, strPerms.Length - 1);
                }

                Framework.FieldVerifyResult result = Framework.FieldVerifyResult.Error;
                try
                {
                    result = r.Add(strPerms);
                }
                catch (Exception ex)
                {
                    throw (new AspNetException("新增角色页面保存角色数据出现异常，异常原因", ex));
                }
                finally { }

                switch (result)
                {
                    case Framework.FieldVerifyResult.NameDuplicated:
                        msg = "已存在相同角色名，请输入其他“名称”。";
                        break;
                    case Framework.FieldVerifyResult.Error:
                        msg = "发生了数据错误，无法完成请求的操作。";
                        break;
                    case Framework.FieldVerifyResult.Success:
                        msg = "新增角色已成功。";
                        txtName.Text = txtNote.Text = hfModuleTasks.Value = string.Empty;
                        break;
                }
            }
            else
                msg = "没有为角色指定任何权限";
            ltMessage.Text = msg;
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "OMRoleManage.Add";
            this.ShortTitle = "新增角色";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/usernrole/roleadd.aspx.js");
        }

        protected void btnEmpty_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Request.CurrentExecutionFilePath);
        }

    }
}