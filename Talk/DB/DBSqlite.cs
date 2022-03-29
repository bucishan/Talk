using Talk.Commons.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Talk.DB
{
    public static class DBSqlite
    {
        private static DBSqliteHelper DB = new DBSqliteHelper();
        /// <summary>
        /// 创建Chat通信记录表
        /// </summary>
        /// <param name="table_name">表名</param>
        public static void CreateChatTable(string table_name)
        {
            string[] sql =
            {
                "CREATE TABLE IF NOT EXISTS '{0}'(",
                "id INTEGER PRIMARY KEY AUTOINCREMENT,",        //自增编号
                "msg_senduser TEXT NOT NULL,",                  //消息发送用户
                "msg TEXT NOT NULL,",                           //消息文本
                "msg_datetime TEXT NOT NULL,",                  //消息时间
                "msg_type INTEGER NOT NULL,",                   //消息类型:[0:本人,1:他人]
                "msg_status INTEGER NOT NULL,",                 //消息状态:[0:未读,1:已读]
                "chat_type INTEGER NOT NULL,",                  //数据类型:[0:mesg,1:game]
                "chat_status INTEGER NOT NULL",                 //数据状态:[0:有效,1:无效]
                ");"
            };
            //string sqlCreateUnique = "CREATE UNIQUE INDEX IF NOT EXISTS 'ik_subjextsn_studentsn' ON 'local_filter' ('subject_sn' ASC, 'student_sn' ASC);";
            DB.ExecuteSql(string.Format(string.Join(" ", sql), table_name));
        }

        /// <summary>
        /// 插入通信数据
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <param name="DecryptMsg">未加密的消息</param>
        /// <param name="msg_status">消息状态是否已读</param>
        /// <param name="chat_type">通讯类型</param>
        /// <param name="chat_status">通讯状态，默认失效</param>
        /// <returns></returns>
        public static int InsertChatTable(string table_name, MsgType msg_type, string msg_senduser, string DecryptMsg, MsgStatus msg_status, ChatType chat_type, ChatStatus chat_status = ChatStatus.Invalid)
        {
            string EncryptMsg = RSATool.Encryption(DecryptMsg);
            string msg_datetime = Tool.GetLongDateTime();
            string sql = string.Format(@"insert into '{0}' (id,msg_senduser,msg,msg_datetime,msg_type,msg_status,chat_type,chat_status) 
                values(NULL,'{1}','{2}','{3}',{4},{5},{6},{7}) ", table_name, msg_senduser, EncryptMsg, msg_datetime, (int)msg_type, (int)msg_status, (int)chat_type, (int)chat_status);
            return DB.ExecuteSql(sql);
        }
        /// <summary>
        /// 删除通信数据表
        /// </summary>
        /// <param name="table_name"></param>
        public static void DeleteChatTable(string table_name)
        {
            string sqlDeleteTable = string.Format("DELETE FROM '{0}';", table_name);
            string sqlUpdateTable = string.Format("update sqlite_sequence SET seq = 0 where name = '{0}';", table_name);

            DB.ExecuteSql(sqlDeleteTable + sqlUpdateTable);
        }

        /// <summary>
        /// 判断数据表是否存在
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <returns></returns>
        public static bool CheckTableExist(string table_name)
        {
            string sql = "select count(1) table_count from sqlite_master where type = 'table' and name = '{0}';";
            object count = DB.GetSingle(string.Format(sql, table_name));
            return Convert.ToInt32(count) > 0;
        }

        /// <summary>
        /// 查询全部消息
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <returns></returns>
        public static DataTable SelectChatInfo(string table_name)
        {
            string sql = "select * from '{0}' order by msg_datetime ASC";
            return DB.GetDataTable(string.Format(sql, table_name));
        }

        /// <summary>
        /// 查询当日内消息
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <returns></returns>
        public static DataTable SelectChatInfoByDay(string table_name)
        {
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            string sql = "select * from '{0}' where msg_datetime BETWEEN '{1}' AND '{2}' order by msg_datetime ASC";
            return DB.GetDataTable(string.Format(sql, table_name, beginDate, endDate));
        }

        /// <summary>
        /// 查询最后100条消息
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <returns></returns>
        public static DataTable SelectChatInfoByHundred(string table_name)
        {
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            string sql = "select * from '{0}' order by msg_datetime DESC limit 0,100";
            DataTable dt = DB.GetDataTable(string.Format(sql, table_name));
            if (dt != null)
            {
                dt.DefaultView.Sort = "id";
                dt = dt.DefaultView.ToTable();
            }
            return dt;
        }

        /// <summary>
        /// 查询最新消息
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <returns></returns>
        public static Dictionary<string, object> SelectChatCountByLatestMsg(string table_name)
        {
            // where msg_type={1}  , (int)MsgType.Other
            Dictionary<string, object> dic = new Dictionary<string, object>();
            string sql = "select * from '{0}' order by msg_datetime DESC limit 0,1";
            DataTable dt = DB.GetDataTable(string.Format(sql, table_name));
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    dic.Add(column.ColumnName, dt.Rows[0][column.ColumnName]);
                }
            }
            return dic;
        }

        /// <summary>
        /// 查询未读消息条数
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <returns></returns>
        public static int SelectChatCountByUnRead(string table_name)
        {
            string sql = "select count(1) chat_count from '{0}' where msg_status={1}";
            object count = DB.GetSingle(string.Format(sql, table_name, (int)MsgStatus.UnRead));
            return Convert.ToInt32(count);
        }

        /// <summary>
        /// 查询未读消息
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <returns></returns>
        public static DataTable SelectChatInfoByUnRead(string table_name)
        {
            return SelectChatInfoByMsgStatus(table_name, MsgStatus.UnRead);
        }

        /// <summary>
        /// 通过消息状态查询数据
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <param name="msg_status">消息状态</param>
        /// <returns></returns>
        public static DataTable SelectChatInfoByMsgStatus(string table_name, MsgStatus msg_status)
        {
            string sql = "select * from '{0}' where msg_status={1} order by msg_datetime ASC";
            return DB.GetDataTable(string.Format(sql, table_name, (int)msg_status));
        }

        /// <summary>
        /// 标记通信表未读消息为已读
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <returns></returns>
        public static int UpdateChatToHaveRead(string table_name)
        {
            string sql = "update '{0}' set msg_status = 1 where msg_status = 0;";
            object count = DB.ExecuteNonQuery(string.Format(sql, table_name, (int)MsgStatus.UnRead));
            return Convert.ToInt32(count);
        }

        /// <summary>
        /// 标记通信表指定通讯类型消息为指定状态
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <param name="chat_type">通讯类型</param>
        /// <param name="chat_status">通讯状态</param>
        /// <returns></returns>
        public static int UpdateChatTypeToSpecify(string table_name, ChatType chat_type, ChatStatus chat_status)
        {
            string sql = "update '{0}' set chat_status = {1} where chat_type = {2};";
            object count = DB.ExecuteNonQuery(string.Format(sql, table_name, (int)chat_status, (int)chat_type));
            return Convert.ToInt32(count);
        }
    }
}
