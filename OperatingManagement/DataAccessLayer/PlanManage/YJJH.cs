﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OperatingManagement.Framework.Basic;
using OperatingManagement.Framework;
using OperatingManagement.Framework.Core;
using System.Data;
using Oracle.DataAccess.Client;

namespace OperatingManagement.DataAccessLayer.PlanManage
{
    /// <summary>
    /// 应用研究工作计划
    /// </summary>
    public class YJJH : BaseEntity<int, YJJH>
    {
        private static readonly string GET_List_ByDate = "up_YJJH_getlist";
        private static readonly string Insert = "up_YJJH_insert";

        /// <summary>
        /// Create a new instance of <see cref="YJJH"/> class.
        /// </summary>
        public YJJH()
        {
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region -Properties-
        private OracleDatabase _database = null;

        public int ID { get; set; }
        public DateTime CTime { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string TaskID { get; set; }
        public string InfoType { get; set; }
        public int LineCount { get; set; }
        public string Format1 { get; set; }
        public string DataSection { get; set; }
        public string FileIndex { get; set; }
        public string Reserve { get; set; }

        public string SatID { get; set; }
        /// <summary>
        /// 信息分类
        /// </summary>
        public string XXFL { get; set; }
        /// <summary>
        /// 计划序号
        /// </summary>
        public string JXH { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SysName { get; set; }
        /// <summary>
        /// 试验开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 试验结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 系统任务
        /// </summary>
        public string Task { get; set; }
        #endregion

        #region -Methods-
        /// <summary>
        /// 获取应用研究工作计划列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetListByDate(DateTime startDate, DateTime endDate)
        {
            DataSet ds = null;
            try
            {
                ds = new DataSet();
                ds.Tables.Add();
                OracleCommand command = _database.GetStoreProcCommand(GET_List_ByDate);
                if (startDate != DateTime.MinValue)
                {
                    _database.AddInParameter(command, "p_startDate", OracleDbType.Date, DBNull.Value);
                }
                else
                {
                    _database.AddInParameter(command, "p_startDate", OracleDbType.Date, startDate);
                }
                if (endDate != DateTime.MinValue)
                {
                    _database.AddInParameter(command, "p_endDate", OracleDbType.Date, DBNull.Value);
                }
                else
                {
                    _database.AddInParameter(command, "p_endDate", OracleDbType.Date, endDate);
                }
                using (IDataReader reader = _database.ExecuteReader(command))
                {
                    ds.Tables[0].Load(reader);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        /// <summary>
        /// Inserts a new record into database.
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
            _database.SpExecuteNonQuery(Insert, new OracleParameter[]{
                new OracleParameter("p_CTime",this.CTime),
                new OracleParameter("p_Source",this.Source),
                new OracleParameter("p_Destination",this.Destination),
                new OracleParameter("p_TaskID",this.TaskID),
                new OracleParameter("p_InfoType",this.InfoType),
                new OracleParameter("p_LineCount",(int)this.LineCount),
                new OracleParameter("p_Format1",this.Format1),
                new OracleParameter("p_DataSection",this.DataSection),
                new OracleParameter("p_FileIndex",this.FileIndex),
                new OracleParameter("p_Reserve",this.Reserve),
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
            _database.SpExecuteNonQuery("up_yjjh_update", new OracleParameter[]{
                new OracleParameter("p_Id",this.ID),
                //new OracleParameter("p_CTime",this.CTime),
                new OracleParameter("p_Source",this.Source),
                new OracleParameter("p_Destination",this.Destination),
                new OracleParameter("p_TaskID",this.TaskID),
                new OracleParameter("p_InfoType",this.InfoType),
                new OracleParameter("p_LineCount",(int)this.LineCount),
                new OracleParameter("p_Format1",this.Format1),
                new OracleParameter("p_DataSection",this.DataSection),
               // new OracleParameter("p_FileIndex",this.FileIndex),
                new OracleParameter("p_Reserve",this.Reserve),
                p
            });
            return (FieldVerifyResult)Convert.ToInt32(p.Value);
        }

        public YJJH SelectById(int id)
        {
            OracleParameter p = PrepareRefCursor();

            DataSet ds = _database.SpExecuteDataSet("up_yjjh_selectbyid", new OracleParameter[]{
                new OracleParameter("p_Id", id), 
                p
            });

            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    return new YJJH()
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        CTime = Convert.ToDateTime(dr["CTIME"].ToString()),
                        Source = dr["Source"].ToString(),
                        Destination = dr["Destination"].ToString(),
                        TaskID = dr["TaskID"].ToString(),
                        InfoType = dr["InfoType"].ToString(),
                        LineCount = Convert.ToInt32(dr["LineCount"].ToString()),
                        Format1 =dr["Format1"].ToString(),
                        DataSection = dr["DataSection"].ToString(),
                        FileIndex = dr["FileIndex"].ToString(),
                        Reserve =dr["Reserve"].ToString()
                    };
                }
            }
            return null;
        }

        public FieldVerifyResult UpdateFileIndex()
        {
            OracleParameter p = new OracleParameter()
            {
                ParameterName = "v_result",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Double
            };
            _database.SpExecuteNonQuery("up_yjjh_updatefileindex", new OracleParameter[]{
                new OracleParameter("v_Id",this.Id),
                new OracleParameter("p_FileIndex",this.FileIndex),
                p
            });
            return (FieldVerifyResult)Convert.ToInt32(p.Value);
        }
        #endregion

        #region -Private methods-
        private OracleParameter PrepareRefCursor()
        {
            return new OracleParameter()
            {
                ParameterName = "o_cursor",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.RefCursor
            };
        }
        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
            //this.AddValidRules("ID", "序号不能为空。", string.IsNullOrEmpty(ID));
        }
        #endregion
    }
}
