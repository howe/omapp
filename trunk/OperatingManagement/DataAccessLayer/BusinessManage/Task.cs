﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;

using OperatingManagement.Framework.Basic;
using OperatingManagement.Framework;
using OperatingManagement.Framework.Core;
using Oracle.DataAccess.Client;

namespace OperatingManagement.DataAccessLayer.BusinessManage
{
    [Serializable]
    public class Task : BaseEntity<int, Task>
    {
        public Task()
        {
            _dataBase = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region Properties
        private OracleDatabase _dataBase = null;
        private const string s_up_task_selectall = "up_task_selectall";
        private const string s_up_task_selectbyid = "up_task_selectbyid";
        private const string s_up_task_insert = "up_task_insert";
        private const string s_up_task_update = "up_task_update";

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        ///// <summary>
        ///// 任务代号
        ///// </summary>
        public string TaskNo { get; set; }
        /// <summary>
        /// 对象标识
        /// </summary>
        public string ObjectFlag { get; set; }
        /// <summary>
        /// 卫星编号，可能多个，逗号,分隔
        /// </summary>
        public string SatID { get; set; }
        /// <summary>
        /// 是否当前任务，1是，0否，表中只有一个为1的
        /// </summary>
        public string IsCurTask { get; set; }
        /// <summary>
        /// 任务开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 任务结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime CTime { get; set; }

        public static List<Task> _taskCache = null;
        public List<Task> Cache
        {
            get
            {
                if (_taskCache == null)
                {
                    _taskCache = SelectAll();
                }
                return _taskCache;
            }
            set { }
        }
        #endregion

        #region -Private Methods-
        private OracleParameter PrepareRefCursor()
        {
            return new OracleParameter()
            {
                ParameterName = "o_Cursor",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.RefCursor
            };
        }

        private OracleParameter PrepareOutputResult()
        {
            return new OracleParameter()
            {
                ParameterName = "v_Result",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Int32,
            };
        }
        #endregion

        #region -Public Methods


        /// <summary>
        /// 获得所有任务信息实体列表
        /// </summary>
        /// <returns>任务信息实体列表</returns>
        public List<Task> SelectAll()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet(s_up_task_selectall, new OracleParameter[] { o_Cursor });

            List<Task> infoList = new List<Task>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Task info = new Task()
                    {
                        Id = Convert.ToInt32(dr["id"].ToString()),
                        TaskName = dr["TaskName"].ToString(),
                        TaskNo = dr["TaskNo"].ToString(),
                        ObjectFlag = dr["ObjectFlag"].ToString(),
                        SatID = dr["SatID"].ToString(),
                        IsCurTask = dr["IsCurTask"].ToString(),
                        BeginTime = dr["BeginTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["BeginTime"]),
                        EndTime = dr["EndTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["EndTime"]),
                        CTime = dr["CTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["CTime"])
                    };

                    infoList.Add(info);
                }
            }
            return infoList;
        }

        /// <summary>
        /// Select Task by id.
        /// </summary>
        /// <returns></returns>
        public Task SelectById()
        {
            OracleParameter p = PrepareRefCursor();

            DataSet ds = _dataBase.SpExecuteDataSet(s_up_task_selectbyid, new OracleParameter[]{
                new OracleParameter("p_Id", this.Id), 
                p
            });

            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    return new Task()
                    {
                        Id = Convert.ToInt32(dr["id"].ToString()),
                        TaskName = dr["TaskName"].ToString(),
                        TaskNo = dr["TaskNo"].ToString(),
                        ObjectFlag = dr["ObjectFlag"].ToString(),
                        SatID = dr["SatID"].ToString(),
                        IsCurTask = dr["IsCurTask"].ToString(),
                        BeginTime = dr["BeginTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["BeginTime"]),
                        EndTime = dr["EndTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["EndTime"]),
                        CTime = dr["CTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["CTime"])
                    };
                }
            }
            return null;
        }

        /// <summary>
        /// Insert a Task.
        /// </summary>
        /// <returns></returns>
        public FieldVerifyResult Add()
        {
            OracleParameter p = new OracleParameter()
            {
                ParameterName = "v_result",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Double
            };
            OracleParameter opId = new OracleParameter()
            {
                ParameterName = "v_Id",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Double
            };
            _dataBase.SpExecuteNonQuery(s_up_task_insert, new OracleParameter[]{
                new OracleParameter("p_TaskName",this.TaskName),
                new OracleParameter("p_TaskNo",this.TaskNo),
                new OracleParameter("p_ObjectFlag",this.ObjectFlag),
                new OracleParameter("p_SatID",this.SatID),
                new OracleParameter("p_IsCurTask",this.IsCurTask),
                new OracleParameter("p_BeginTime",this.BeginTime),
                new OracleParameter("p_EndTime",this.EndTime),
                new OracleParameter("p_CTime",DateTime.Now),
                opId,
                p
            });
            if (opId.Value != null && opId.Value != DBNull.Value)
                this.Id = Convert.ToInt32(opId.Value);
            return (FieldVerifyResult)Convert.ToInt32(p.Value);
        }

        public FieldVerifyResult Update()
        {
            OracleParameter p = new OracleParameter()
            {
                ParameterName = "v_result",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Double
            };
            _dataBase.SpExecuteNonQuery(s_up_task_update, new OracleParameter[]{
                new OracleParameter("p_Id", this.Id),
                new OracleParameter("p_TaskName", this.TaskName),
                new OracleParameter("p_TaskNo", this.TaskNo),
                new OracleParameter("p_ObjectFlag", this.ObjectFlag),
                new OracleParameter("p_SatID", this.SatID),
                new OracleParameter("p_IsCurTask", Convert.ToInt32(this.IsCurTask)),
                new OracleParameter("p_BeginTime", this.BeginTime),
                new OracleParameter("p_EndTime", this.EndTime),
                p
            });
            return (FieldVerifyResult)Convert.ToInt32(p.Value);
        }

        /// <summary>
        /// 通过任务代号获取对象标识
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public string GetObjectFlagByTaskNo(string taskNo)
        {
            string strResult = string.Empty;
            if (Cache != null)
            {
                var query = Cache.Where(a => a.TaskNo == taskNo);
                if (query != null && query.Count() > 0)
                    strResult = query.FirstOrDefault().ObjectFlag;
            }
            return strResult;
        }

        /// <summary>
        /// 通过对象标识获取任务代号
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public string GetTaskNoByObjectFlag(string objectFlag)
        {
            string strResult = string.Empty;
            if (Cache != null)
            {
                var query = Cache.Where(a => a.ObjectFlag == objectFlag);
                if (query != null && query.Count() > 0)
                    strResult = query.FirstOrDefault().TaskNo;
            }
            return strResult;
        }

        /// <summary>
        /// 通过任务代号获取卫星编号
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public string GetSatIDByTaskNo(string taskNo)
        {
            string strResult = string.Empty;
            if (Cache != null)
            {
                var query = Cache.Where(a => a.TaskNo == taskNo);
                if (query != null && query.Count() > 0)
                    strResult = query.FirstOrDefault().SatID;
            }
            return strResult;
        }
        /// <summary>
        /// 通过任务代号获取任务名称
        /// </summary>
        /// <param name="taskNO"></param>
        /// <returns></returns>
        public string GetTaskName(string taskNO)
        {
            string taskName = string.Empty;
            if (Cache != null)
            {
                var query = Cache.Where(a => a.TaskNo == taskNO);
                if (query != null && query.Count() > 0)
                    taskName = query.FirstOrDefault().TaskName;
            }
            return taskName;
        }
        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
        }
        #endregion
    }
}