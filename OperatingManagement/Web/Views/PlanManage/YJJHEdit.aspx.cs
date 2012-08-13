﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

using OperatingManagement.WebKernel.Basic;
using OperatingManagement.WebKernel.Route;
using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer;
using OperatingManagement.DataAccessLayer.PlanManage;
using OperatingManagement.Framework;
using System.Web.Security;
using System.Xml;
using ServicesKernel.File;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class YJJHEdit : AspNetPage, IRouteContext
    {
        bool isTempJH = false;  //是否为临时计划,默认为正式计划

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartTime.Attributes.Add("readonly", "true");
                txtEndTime.Attributes.Add("readonly", "true");
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    string sID = Request.QueryString["id"];
                    
                    if (!string.IsNullOrEmpty(Request.QueryString["istemp"]) && Request.QueryString["istemp"] == "true")
                    {
                        isTempJH = true;
                        ViewState["isTempJH"] = true;
                    }

                    HfID.Value = sID;
                    hfStatus.Value = "edit";    //编辑
                    BindJhTable(sID);
                    BindXML();
                    hfURL.Value = "?type=YJJH&startDate=" + Request.QueryString["startDate"] + "&endDate=" + Request.QueryString["endDate"];
                    if ("detail" == Request.QueryString["op"])
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "hide", "<script type='text/javascript'>hideAllButton();</script>");
                    }
                }
                else
                {
                    btnReturn.Visible = false;
                    hfStatus.Value = "new"; //新建
                    btnSaveTo.Visible = false;
                    //txtJXH.Text = (new Sequence()).GetYJJHSequnce().ToString("0000");   //新建时先给出计划序号
                }
            }
            
        }

        private void BindJhTable(string sID)
        {
            try
            {
                isTempJH = GetIsTempJHValue();

                List<JH> jh = (new JH(isTempJH)).SelectByIDS(sID);
                HfFileIndex.Value = jh[0].FileIndex;
                hfTaskID.Value = jh[0].TaskID.ToString();
                txtJXH.Text = jh[0].PlanID.ToString("0000");
                ucTask1.SelectedValue = jh[0].TaskID.ToString();
                string[] strTemp = jh[0].FileIndex.Split('_');
                if (strTemp.Length >= 2)
                {
                    hfSatID.Value = strTemp[strTemp.Length - 2];
                    ucSatellite1.SelectedValue = strTemp[strTemp.Length - 2];
                }
                txtNote.Text = jh[0].Reserve.ToString();
                if (DateTime.Now > jh[0].StartTime)
                {
                    btnSubmit.Visible = false;
                    //hfOverDate.Value = "true";
                }
            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定计划基本信息出现异常，异常原因", ex));
            }
            finally { }
        }
        private void BindXML()
        {
            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(HfFileIndex.Value);
                XmlNode root = xmlDoc.SelectSingleNode("应用研究工作计划/XXFL");
                //txtXXFL.Text = root.InnerText;
                radBtnXXFL.SelectedValue = root.InnerText;
                root = xmlDoc.SelectSingleNode("应用研究工作计划/JXH");
                txtJXH.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("应用研究工作计划/SysName");
                //txtSysName.Text = root.InnerText;
                ddlSysName.SelectedValue = root.InnerText;
                root = xmlDoc.SelectSingleNode("应用研究工作计划/StartTime");
                txtStartTime.Text = root.InnerText;
                // ucStartTimer.Timer = root.InnerText.Substring(8);
                root = xmlDoc.SelectSingleNode("应用研究工作计划/EndTime");
                txtEndTime.Text = root.InnerText;
                //ucEndTimer.Timer = root.InnerText.Substring(8);
                root = xmlDoc.SelectSingleNode("应用研究工作计划/Task");
                txtTask.Text = root.InnerText;
            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定应用研究工作计划出现异常，异常原因", ex));
            }
            finally { }
        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.Edit";
            this.ShortTitle = "编辑计划";
            base.OnPageLoaded();
           //this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/PlanManage/YJJHEdit.aspx.js");

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                isTempJH = GetIsTempJHValue();

                YJJH obj = new YJJH();
                obj.XXFL = radBtnXXFL.SelectedValue;
                obj.JXH = txtJXH.Text;
                obj.SysName = ddlSysName.SelectedValue;
                obj.StartTime = txtStartTime.Text;
                obj.EndTime = txtEndTime.Text;
                obj.Task = txtTask.Text;
                obj.TaskID = ucTask1.SelectedItem.Value;
                obj.SatID = ucSatellite1.SelectedItem.Value;
                CultureInfo provider = CultureInfo.InvariantCulture;

                PlanFileCreator creater = new PlanFileCreator(isTempJH);

                if (hfStatus.Value == "new")
                {
                    //保存时才生成计划序号
                    obj.JXH = (new Sequence()).GetYJJHSequnce().ToString("0000");
                    txtJXH.Text = obj.JXH;
                    string filepath = creater.CreateYJJHFile(obj, 0);
                    DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH(isTempJH)
                    {
                        TaskID = obj.TaskID,
                        PlanType = "YJJH",
                        PlanID = Convert.ToInt32(obj.JXH),
                        StartTime = DateTime.ParseExact(obj.StartTime, "yyyyMMddHHmmss", provider),
                        EndTime = DateTime.ParseExact(obj.EndTime, "yyyyMMddHHmmss", provider),
                        SRCType = 0,
                        FileIndex = filepath,
                        SatID = obj.SatID,
                        Reserve = txtNote.Text
                    };
                    var result = jh.Add();
                }
                else
                {
                    //当任务和卫星更新时，需要更新文件名称
                    if (hfSatID.Value != ucSatellite1.SelectedValue || hfTaskID.Value != ucTask1.SelectedValue)
                    {
                        string filepath = creater.CreateYJJHFile(obj, 0);
                        DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH(isTempJH)
                        {
                            Id = Convert.ToInt32(HfID.Value),
                            TaskID = obj.TaskID,
                            StartTime = DateTime.ParseExact(obj.StartTime, "yyyyMMddHHmmss", provider),
                            EndTime = DateTime.ParseExact(obj.EndTime, "yyyyMMddHHmmss", provider),
                            FileIndex = filepath,
                            SatID = obj.SatID,
                            Reserve = txtNote.Text
                        };
                        var result = jh.Update();
                        //更新隐藏域的任务ID和卫星ID
                        hfTaskID.Value = jh.TaskID;
                        hfSatID.Value = jh.SatID;
                    }
                    else
                    {
                        creater.FilePath = HfFileIndex.Value;
                        creater.CreateYJJHFile(obj, 1);
                    }
                }

                trMessage.Visible = true;
                ltMessage.Text = "计划保存成功";
                //ClientScript.RegisterStartupScript(this.GetType(), "OK", "<script type='text/javascript'>showMsg('计划保存成功');</script>");
            }
            catch (Exception ex)
            {
                throw (new AspNetException("保存计划信息出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void btnSaveTo_Click(object sender, EventArgs e)
        {
            try
            {
                isTempJH = GetIsTempJHValue();

                YJJH obj = new YJJH();
                obj.XXFL = radBtnXXFL.SelectedValue;
                //obj.JXH = txtJXH.Text;
                obj.SysName = ddlSysName.SelectedValue;
                obj.StartTime = txtStartTime.Text;
                obj.EndTime = txtEndTime.Text;
                obj.Task = txtTask.Text;
                CultureInfo provider = CultureInfo.InvariantCulture;

                PlanFileCreator creater = new PlanFileCreator(isTempJH);

                obj.TaskID = ucTask1.SelectedItem.Value;
                obj.SatID = ucSatellite1.SelectedItem.Value;
                obj.JXH = (new Sequence()).GetYJJHSequnce().ToString("0000");

                if (creater.TestYJJHFileName(obj))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "File", "<script type='text/javascript'>showMsg('存在同名文件，请一分钟后重试');</script>");
                    return;
                }

                string filepath = creater.CreateYJJHFile(obj, 0);

                DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH(isTempJH)
                {
                    TaskID = obj.TaskID,
                    PlanType = "YJJH",
                    PlanID = Convert.ToInt32(obj.JXH),
                    StartTime = DateTime.ParseExact(obj.StartTime, "yyyyMMddHHmmss", provider),
                    EndTime = DateTime.ParseExact(obj.EndTime, "yyyyMMddHHmmss", provider),
                    SRCType = 0,
                    FileIndex = filepath,
                    SatID = obj.SatID,
                    Reserve = txtNote.Text
                };
                var result = jh.Add();

                txtJXH.Text = obj.JXH;  //另存后显示新的序号
                trMessage.Visible = true;
                ltMessage.Text = "计划保存成功";
               // ClientScript.RegisterStartupScript(this.GetType(), "OK", "<script type='text/javascript'>showMsg('计划保存成功');</script>");
            }
            catch (Exception ex)
            {
                throw (new AspNetException("另存计划信息出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(HfID.Value))
                {
                    Page.Response.Redirect(Request.CurrentExecutionFilePath,false);
                }
                else
                {
                    string sID = HfID.Value;
                    HfID.Value = sID;
                    hfStatus.Value = "edit";    //编辑
                    BindJhTable(sID);
                    BindXML();
                }
            }
            catch (Exception ex)
            {
                throw (new AspNetException("重置页面出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            isTempJH = GetIsTempJHValue();

            if (isTempJH)
            {
                Response.Redirect("PlanTempList.aspx" + hfURL.Value);
            }
            else
            {
                Response.Redirect("PlanList.aspx" + hfURL.Value);
            }
        }

        protected bool GetIsTempJHValue()
        {
            bool returnvalue = false;
            if (null != ViewState["isTempJH"])
            {
                returnvalue = Convert.ToBoolean(ViewState["isTempJH"]);
            }
            return returnvalue;
        }

    }
}