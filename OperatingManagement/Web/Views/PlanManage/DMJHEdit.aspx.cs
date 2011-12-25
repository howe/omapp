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
using System.Collections;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class DMJHEdit : AspNetPage, IRouteContext
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
            List<DMJH_Task> listTask = new List<DMJH_Task>();
            DMJH_Task task;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HfFileIndex.Value);
            XmlNode root = xmlDoc.SelectSingleNode("地面站工作计划/编号");
            txtSequence.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("地面站工作计划/时间");
            txtDatetime.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("地面站工作计划/工作单位");
            txtStationName.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("地面站工作计划/设备代号");
            txtEquipmentID.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("地面站工作计划/任务个数");
            txtTaskCount.Text = root.InnerText;

            root = xmlDoc.SelectSingleNode("地面站工作计划/任务");
            foreach (XmlNode n in root.ChildNodes)
            {
                task = new DMJH_Task();
                task.TaskFlag = n["任务标志"].InnerText;
                task.WorkWay = n["工作方式"].InnerText;
                task.PlanPropertiy = n["计划性质"].InnerText;
                task.WorkMode = n["工作模式"].InnerText;
                task.PreStartTime = n["任务准备开始时间"].InnerText;
                task.StartTime = n["任务开始时间"].InnerText;
                task.TrackStartTime = n["跟踪开始时间"].InnerText;
                task.WaveOnStartTime = n["开上行载波时间"].InnerText;
                task.WaveOffStartTime = n["关上行载波时间"].InnerText;
                task.TrackEndTime = n["跟踪结束时间"].InnerText;
                task.EndTime = n["任务结束时间"].InnerText;
                listTask.Add(task);
            }

            Repeater1.DataSource = listTask;
            Repeater1.DataBind();
        }


        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["arrR"] != null &&ViewState["arrA"] != null && ViewState["op"] != null)
                {
                    ArrayList arrR = (ArrayList)ViewState["arrR"];
                    ArrayList arrA = (ArrayList)ViewState["arrA"];
                    Repeater rpr = e.Item.FindControl("rpReakTimeTransfor") as Repeater;
                    Repeater rpa = e.Item.FindControl("rpAfterFeedBack") as Repeater;

                    List<DMJH_Task_ReakTimeTransfor> listr = new List<DMJH_Task_ReakTimeTransfor>();
                    List<DMJH_Task_AfterFeedBack> lista = new List<DMJH_Task_AfterFeedBack>();
                    DMJH_Task_ReakTimeTransfor rtt;
                    DMJH_Task_AfterFeedBack afb;
                    string op = (string)ViewState["op"];
                    if (op == "Add")
                    {
                        #region ReakTimeTransfor
                        if (e.Item.ItemIndex <= arrR.Count - 1)
                        {
                            listr = (List<DMJH_Task_ReakTimeTransfor>)arrR[e.Item.ItemIndex];
                            rpr.DataSource = listr;
                            rpr.DataBind();
                        }
                        else
                        {
                            rtt = new DMJH_Task_ReakTimeTransfor();
                            rtt.FormatFlag = "";
                            rtt.InfoFlowFlag = "";
                            rtt.TransStartTime = "";
                            rtt.TransEndTime = "";
                            rtt.TransSpeedRate = "";
                            listr.Add(rtt);
                            rpr.DataSource = listr;
                            rpr.DataBind();
                        }
                        #endregion
                        #region AfterFeedBack
                        if (e.Item.ItemIndex <= arrA.Count - 1)
                        {
                            lista = (List<DMJH_Task_AfterFeedBack>)arrA[e.Item.ItemIndex];
                            rpa.DataSource = lista;
                            rpa.DataBind();
                        }
                        else
                        {
                            afb = new DMJH_Task_AfterFeedBack();
                            afb.FormatFlag = "";
                            afb.InfoFlowFlag = "";
                            afb.DataStartTime = "";
                            afb.DataEndTime = "";
                            afb.TransStartTime = "";
                            afb.TransSpeedRate = "";
                            lista.Add(afb);
                            rpa.DataSource = lista;
                            rpa.DataBind();
                        }
                        #endregion
                    }
                    if (op == "Del")
                    {
                        #region ReakTimeTransfor
                        if (e.Item.ItemIndex <= arrR.Count - 1)
                        {
                            listr = (List<DMJH_Task_ReakTimeTransfor>)arrR[e.Item.ItemIndex];
                            rpr.DataSource = listr;
                            rpr.DataBind();
                        }
                        #endregion
                        #region AfterFeedBack
                        if (e.Item.ItemIndex <= arrA.Count - 1)
                        {
                            lista = (List<DMJH_Task_AfterFeedBack>)arrA[e.Item.ItemIndex];
                            rpa.DataSource = lista;
                            rpa.DataBind();
                        }
                        #endregion
                    }
                }
                else
                {
                    #region 正常
                    Repeater rpR = e.Item.FindControl("rpReakTimeTransfor") as Repeater;
                    Repeater rpA = e.Item.FindControl("rpAfterFeedBack") as Repeater;
                    int row = e.Item.ItemIndex;
                    List<DMJH_Task_ReakTimeTransfor> list1 = new List<DMJH_Task_ReakTimeTransfor>();
                    List<DMJH_Task_AfterFeedBack> list2 = new List<DMJH_Task_AfterFeedBack>();
                    DMJH_Task_ReakTimeTransfor rt;
                    DMJH_Task_AfterFeedBack afb;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(HfFileIndex.Value);
                    XmlNode root = xmlDoc.SelectSingleNode("DMJH/任务");
                    int i = 0;
                    foreach (XmlNode n in root.ChildNodes)
                    {
                        foreach (XmlNode nd in n.ChildNodes)
                        {
                            if (row == i)
                            {
                                if (nd.Name == "实时传输")
                                {
                                    rt = new DMJH_Task_ReakTimeTransfor();
                                    rt.FormatFlag = nd["格式标志"].InnerText;
                                    rt.InfoFlowFlag = nd["信息流标志"].InnerText;
                                    rt.TransStartTime = nd["数据传输开始时间"].InnerText;
                                    rt.TransEndTime = nd["数据传输结束时间"].InnerText;
                                    rt.TransSpeedRate = nd["数据传输速率"].InnerText;
                                    list1.Add(rt);
                                }
                                if (nd.Name == "事后回放")
                                {
                                    afb = new DMJH_Task_AfterFeedBack();
                                    afb.FormatFlag = nd["格式标志"].InnerText;
                                    afb.InfoFlowFlag = nd["信息流标志"].InnerText;
                                    afb.DataStartTime = nd["数据起始时间"].InnerText;
                                    afb.DataEndTime = nd["数据结束时间"].InnerText;
                                    afb.TransStartTime = nd["数据传输开始时间"].InnerText;
                                    afb.TransSpeedRate = nd["数据传输速率"].InnerText;
                                    list2.Add(afb);
                                }
                            }
                        }

                        i = i + 1;
                    }
                    rpR.DataSource = list1;
                    rpR.DataBind();
                    rpA.DataSource = list2;
                    rpA.DataBind();

                    #endregion
                }
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            List<DMJH_Task> list2 = new List<DMJH_Task>();
            DMJH_Task rt;
            List<DMJH_Task_ReakTimeTransfor> listr = new List<DMJH_Task_ReakTimeTransfor>();
            List<DMJH_Task_AfterFeedBack> lista = new List<DMJH_Task_AfterFeedBack>();
            DMJH_Task_ReakTimeTransfor rtt;
            DMJH_Task_AfterFeedBack afb;
            Repeater rp = (Repeater)source;
            ArrayList arrR = new ArrayList();
            ArrayList arrA = new ArrayList();
            if (e.CommandName == "Add")
            {
                ViewState["op"] = "Add";
                foreach (RepeaterItem it in rp.Items)
                {
                    #region ReakTimeTransfor
                    Repeater rps = it.FindControl("rpReakTimeTransfor") as Repeater;
                    foreach (RepeaterItem its in rps.Items)
                    {
                        
                        rtt = new DMJH_Task_ReakTimeTransfor();
                        TextBox txtFormatFlag = (TextBox)it.FindControl("txtFormatFlag");
                        TextBox txtInfoFlowFlag = (TextBox)it.FindControl("txtInfoFlowFlag");
                        TextBox txtTransStartTime = (TextBox)it.FindControl("txtTransStartTime");
                        TextBox txtTransEndTime = (TextBox)it.FindControl("txtTransEndTime");
                        TextBox txtTransSpeedRate = (TextBox)it.FindControl("txtTransSpeedRate");

                        rtt.FormatFlag = txtFormatFlag.Text;
                        rtt.InfoFlowFlag = txtInfoFlowFlag.Text;
                        rtt.TransStartTime = txtTransStartTime.Text;
                        rtt.TransEndTime = txtTransEndTime.Text;
                        rtt.TransSpeedRate = txtTransSpeedRate.Text;
                        listr.Add(rtt);
                        
                    }
                    arrR.Add(listr);
                    listr = new List<DMJH_Task_ReakTimeTransfor>();
                    #endregion
                    #region AfterFeedBack
                    Repeater rpa = it.FindControl("rpAfterFeedBack") as Repeater;
                    foreach (RepeaterItem its in rpa.Items)
                    {

                        afb = new DMJH_Task_AfterFeedBack();
                        TextBox txtFormatFlag = (TextBox)it.FindControl("FormatFlag");
                        TextBox txtInfoFlowFlag = (TextBox)it.FindControl("InfoFlowFlag");
                        TextBox txtDataStartTime = (TextBox)it.FindControl("DataStartTime");
                        TextBox txtDataEndTime = (TextBox)it.FindControl("DataEndTime");
                        TextBox txtTransStartTime = (TextBox)it.FindControl("TransStartTime");
                        TextBox txtTransSpeedRate = (TextBox)it.FindControl("TransSpeedRate");

                        afb.FormatFlag = txtFormatFlag.Text;
                        afb.InfoFlowFlag = txtInfoFlowFlag.Text;
                        afb.DataStartTime = txtDataStartTime.Text;
                        afb.DataEndTime = txtDataEndTime.Text;
                        afb.TransStartTime = txtTransStartTime.Text;
                        afb.TransSpeedRate = txtTransSpeedRate.Text;
                        lista.Add(afb);

                    }
                    arrA.Add(lista);
                    lista = new List<DMJH_Task_AfterFeedBack>();
                    #endregion
                    #region Task
                    rt = new DMJH_Task();
                    TextBox txtTaskFlag = (TextBox)it.FindControl("txtTaskFlag");
                    TextBox txtWorkWay = (TextBox)it.FindControl("txtWorkWay");
                    TextBox txtPlanPropertiy = (TextBox)it.FindControl("txtPlanPropertiy");
                    TextBox txtWorkMode = (TextBox)it.FindControl("txtWorkMode");
                    TextBox txtPreStartTime = (TextBox)it.FindControl("txtPreStartTime");
                    TextBox txtStartTime = (TextBox)it.FindControl("txtStartTime");
                    TextBox txtTrackStartTime = (TextBox)it.FindControl("txtTrackStartTime");
                    TextBox txtWaveOnStartTime = (TextBox)it.FindControl("txtWaveOnStartTime");
                    TextBox txtWaveOffStartTime = (TextBox)it.FindControl("txtWaveOffStartTime");
                    TextBox txtTrackEndTime = (TextBox)it.FindControl("txtTrackEndTime");
                    TextBox txtEndTime = (TextBox)it.FindControl("txtEndTime");

                    rt.TaskFlag = txtTaskFlag.Text;
                    rt.WorkWay = txtWorkWay.Text;
                    rt.PlanPropertiy = txtPlanPropertiy.Text;
                    rt.WorkMode = txtWorkMode.Text;
                    rt.PreStartTime = txtPreStartTime.Text;
                    rt.StartTime = txtStartTime.Text;
                    rt.TrackStartTime = txtTrackStartTime.Text;
                    rt.WaveOnStartTime = txtWaveOnStartTime.Text;
                    rt.WaveOffStartTime = txtWaveOffStartTime.Text;
                    rt.TrackEndTime = txtTrackEndTime.Text;
                    rt.EndTime = txtEndTime.Text;
                    list2.Add(rt);

                    #endregion
                }
                rt = new DMJH_Task();
                rt.TaskFlag = "";
                rt.WorkWay = "";
                rt.PlanPropertiy = "";
                rt.WorkMode = "";
                rt.PreStartTime = "";
                rt.StartTime = "";
                rt.TrackStartTime = "";
                rt.WaveOnStartTime = "";
                rt.WaveOffStartTime = "";
                rt.TrackEndTime = "";
                rt.EndTime = "";
                list2.Add(rt);
                ViewState["arrR"] = arrR;
                ViewState["arrA"] = arrA;
                rp.DataSource = list2;
                rp.DataBind();
            }
            if (e.CommandName == "Del")
            {
                ViewState["op"] = "Del";
                foreach (RepeaterItem it in rp.Items)
                {
                    if (e.Item.ItemIndex != it.ItemIndex)
                    {
                        #region ReakTimeTransfor
                        Repeater rps = it.FindControl("rpReakTimeTransfor") as Repeater;
                        foreach (RepeaterItem its in rps.Items)
                        {

                            rtt = new DMJH_Task_ReakTimeTransfor();
                            TextBox txtFormatFlag = (TextBox)it.FindControl("txtFormatFlag");
                            TextBox txtInfoFlowFlag = (TextBox)it.FindControl("txtInfoFlowFlag");
                            TextBox txtTransStartTime = (TextBox)it.FindControl("txtTransStartTime");
                            TextBox txtTransEndTime = (TextBox)it.FindControl("txtTransEndTime");
                            TextBox txtTransSpeedRate = (TextBox)it.FindControl("txtTransSpeedRate");

                            rtt.FormatFlag = txtFormatFlag.Text;
                            rtt.InfoFlowFlag = txtInfoFlowFlag.Text;
                            rtt.TransStartTime = txtTransStartTime.Text;
                            rtt.TransEndTime = txtTransEndTime.Text;
                            rtt.TransSpeedRate = txtTransSpeedRate.Text;
                            listr.Add(rtt);

                        }
                        arrR.Add(listr);
                        listr = new List<DMJH_Task_ReakTimeTransfor>();
                        #endregion
                        #region AfterFeedBack
                        Repeater rpa = it.FindControl("rpAfterFeedBack") as Repeater;
                        foreach (RepeaterItem its in rpa.Items)
                        {

                            afb = new DMJH_Task_AfterFeedBack();
                            TextBox txtFormatFlag = (TextBox)it.FindControl("FormatFlag");
                            TextBox txtInfoFlowFlag = (TextBox)it.FindControl("InfoFlowFlag");
                            TextBox txtDataStartTime = (TextBox)it.FindControl("DataStartTime");
                            TextBox txtDataEndTime = (TextBox)it.FindControl("DataEndTime");
                            TextBox txtTransStartTime = (TextBox)it.FindControl("TransStartTime");
                            TextBox txtTransSpeedRate = (TextBox)it.FindControl("TransSpeedRate");

                            afb.FormatFlag = txtFormatFlag.Text;
                            afb.InfoFlowFlag = txtInfoFlowFlag.Text;
                            afb.DataStartTime = txtDataStartTime.Text;
                            afb.DataEndTime = txtDataEndTime.Text;
                            afb.TransStartTime = txtTransStartTime.Text;
                            afb.TransSpeedRate = txtTransSpeedRate.Text;
                            lista.Add(afb);

                        }
                        arrA.Add(lista);
                        lista = new List<DMJH_Task_AfterFeedBack>();
                        #endregion
                        #region Task
                        rt = new DMJH_Task();
                        TextBox txtTaskFlag = (TextBox)it.FindControl("txtTaskFlag");
                        TextBox txtWorkWay = (TextBox)it.FindControl("txtWorkWay");
                        TextBox txtPlanPropertiy = (TextBox)it.FindControl("txtPlanPropertiy");
                        TextBox txtWorkMode = (TextBox)it.FindControl("txtWorkMode");
                        TextBox txtPreStartTime = (TextBox)it.FindControl("txtPreStartTime");
                        TextBox txtStartTime = (TextBox)it.FindControl("txtStartTime");
                        TextBox txtTrackStartTime = (TextBox)it.FindControl("txtTrackStartTime");
                        TextBox txtWaveOnStartTime = (TextBox)it.FindControl("txtWaveOnStartTime");
                        TextBox txtWaveOffStartTime = (TextBox)it.FindControl("txtWaveOffStartTime");
                        TextBox txtTrackEndTime = (TextBox)it.FindControl("txtTrackEndTime");
                        TextBox txtEndTime = (TextBox)it.FindControl("txtEndTime");

                        rt.TaskFlag = txtTaskFlag.Text;
                        rt.WorkWay = txtWorkWay.Text;
                        rt.PlanPropertiy = txtPlanPropertiy.Text;
                        rt.WorkMode = txtWorkMode.Text;
                        rt.PreStartTime = txtPreStartTime.Text;
                        rt.StartTime = txtStartTime.Text;
                        rt.TrackStartTime = txtTrackStartTime.Text;
                        rt.WaveOnStartTime = txtWaveOnStartTime.Text;
                        rt.WaveOffStartTime = txtWaveOffStartTime.Text;
                        rt.TrackEndTime = txtTrackEndTime.Text;
                        rt.EndTime = txtEndTime.Text;
                        list2.Add(rt);
                        #endregion
                    }
                }
                ViewState["arrR"] = arrR;
                ViewState["arrA"] = arrA;
                rp.DataSource = list2;
                rp.DataBind();
            }
        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<DMJH_Task_ReakTimeTransfor> list2 = new List<DMJH_Task_ReakTimeTransfor>();
                DMJH_Task_ReakTimeTransfor rt;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    rt = new DMJH_Task_ReakTimeTransfor();
                    TextBox txtFormatFlag = (TextBox)it.FindControl("txtFormatFlag");
                    TextBox txtInfoFlowFlag = (TextBox)it.FindControl("txtInfoFlowFlag");
                    TextBox txtTransStartTime = (TextBox)it.FindControl("txtTransStartTime");
                    TextBox txtTransEndTime = (TextBox)it.FindControl("txtTransEndTime");
                    TextBox txtTransSpeedRate = (TextBox)it.FindControl("txtTransSpeedRate");

                    rt.FormatFlag = txtFormatFlag.Text;
                    rt.InfoFlowFlag = txtInfoFlowFlag.Text;
                    rt.TransStartTime = txtTransStartTime.Text;
                    rt.TransEndTime = txtTransEndTime.Text;
                    rt.TransSpeedRate = txtTransSpeedRate.Text;
                    list2.Add(rt);
                }
                rt = new DMJH_Task_ReakTimeTransfor();
                rt.FormatFlag = "";
                rt.InfoFlowFlag = "";
                rt.TransStartTime = "";
                rt.TransEndTime = "";
                rt.TransSpeedRate = "";
                list2.Add(rt);
                rp.DataSource = list2;
                rp.DataBind();
            }
            if (e.CommandName == "Del")
            {
                List<DMJH_Task_ReakTimeTransfor> list2 = new List<DMJH_Task_ReakTimeTransfor>();
                DMJH_Task_ReakTimeTransfor rt;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    if (e.Item.ItemIndex != it.ItemIndex)
                    {
                        rt = new DMJH_Task_ReakTimeTransfor();
                        TextBox txtFormatFlag = (TextBox)it.FindControl("txtFormatFlag");
                        TextBox txtInfoFlowFlag = (TextBox)it.FindControl("txtInfoFlowFlag");
                        TextBox txtTransStartTime = (TextBox)it.FindControl("txtTransStartTime");
                        TextBox txtTransEndTime = (TextBox)it.FindControl("txtTransEndTime");
                        TextBox txtTransSpeedRate = (TextBox)it.FindControl("txtTransSpeedRate");

                        rt.FormatFlag = txtFormatFlag.Text;
                        rt.InfoFlowFlag = txtInfoFlowFlag.Text;
                        rt.TransStartTime = txtTransStartTime.Text;
                        rt.TransEndTime = txtTransEndTime.Text;
                        rt.TransSpeedRate = txtTransSpeedRate.Text;
                        list2.Add(rt);
                    }
                }
                rp.DataSource = list2;
                rp.DataBind();
            }
        }

        protected void Repeater3_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<DMJH_Task_AfterFeedBack> list2 = new List<DMJH_Task_AfterFeedBack>();
                DMJH_Task_AfterFeedBack rt;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    rt = new DMJH_Task_AfterFeedBack();
                    TextBox txtFormatFlag = (TextBox)it.FindControl("FormatFlag");
                    TextBox txtInfoFlowFlag = (TextBox)it.FindControl("InfoFlowFlag");
                    TextBox txtDataStartTime = (TextBox)it.FindControl("DataStartTime");
                    TextBox txtDataEndTime = (TextBox)it.FindControl("DataEndTime");
                    TextBox txtTransStartTime = (TextBox)it.FindControl("TransStartTime");
                    TextBox txtTransSpeedRate = (TextBox)it.FindControl("TransSpeedRate");

                    rt.FormatFlag = txtFormatFlag.Text;
                    rt.InfoFlowFlag = txtInfoFlowFlag.Text;
                    rt.DataStartTime = txtDataStartTime.Text;
                    rt.DataEndTime = txtDataEndTime.Text;
                    rt.TransStartTime = txtTransStartTime.Text;
                    rt.TransSpeedRate = txtTransSpeedRate.Text;
                    list2.Add(rt);
                }
                rt = new DMJH_Task_AfterFeedBack();
                rt.FormatFlag = "";
                rt.InfoFlowFlag = "";
                rt.TransStartTime = "";
                rt.DataEndTime = "";
                rt.DataStartTime = "";
                rt.TransSpeedRate = "";
                list2.Add(rt);
                rp.DataSource = list2;
                rp.DataBind();
            }
            if (e.CommandName == "Del")
            {
                List<DMJH_Task_AfterFeedBack> list2 = new List<DMJH_Task_AfterFeedBack>();
                DMJH_Task_AfterFeedBack rt;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    if (e.Item.ItemIndex != it.ItemIndex)
                    {
                        rt = new DMJH_Task_AfterFeedBack();
                        TextBox txtFormatFlag = (TextBox)it.FindControl("FormatFlag");
                        TextBox txtInfoFlowFlag = (TextBox)it.FindControl("InfoFlowFlag");
                        TextBox txtDataStartTime = (TextBox)it.FindControl("DataStartTime");
                        TextBox txtDataEndTime = (TextBox)it.FindControl("DataEndTime");
                        TextBox txtTransStartTime = (TextBox)it.FindControl("TransStartTime");
                        TextBox txtTransSpeedRate = (TextBox)it.FindControl("TransSpeedRate");

                        rt.FormatFlag = txtFormatFlag.Text;
                        rt.InfoFlowFlag = txtInfoFlowFlag.Text;
                        rt.DataStartTime = txtDataStartTime.Text;
                        rt.DataEndTime = txtDataEndTime.Text;
                        rt.TransStartTime = txtTransStartTime.Text;
                        rt.TransSpeedRate = txtTransSpeedRate.Text;
                        list2.Add(rt);
                    }
                }
                rp.DataSource = list2;
                rp.DataBind();
            }
        }


        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.Edit";
            this.ShortTitle = "编辑计划";
            base.OnPageLoaded();
            this.AddJavaScriptInclude("scripts/pages/ZXJHEdit.aspx.js");
        }
        //
    }
}