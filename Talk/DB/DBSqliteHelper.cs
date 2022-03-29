using Talk.Commons.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace Talk.DB
{
    /// <summary>
    /// Sqlite数据库辅助类
    /// </summary>
    public class DBSqliteHelper
    {
        private static string connectionString = string.Empty;

        public DBSqliteHelper()
        {
            SetConnectionString();
        }
        /// <summary>
        /// 根据数据源、密码、版本号设置连接字符串。
        /// </summary>
        public static void SetConnectionString()
        {
            string appDir = System.Windows.Forms.Application.StartupPath;
            string dbFile = Path.Combine(System.Windows.Forms.Application.StartupPath, "chat.db");
            if (!File.Exists(dbFile))
            {
                CreateDB(dbFile);
                //return;
            }
            connectionString = string.Format("Data Source={0};Version={1};", dbFile, 3);
            //connectionString = string.Format("Data Source={0};Version={1};password={2};",dbFile, 3, password);
        }

        /// <summary>
        /// 创建一个数据库文件。如果存在同名数据库文件，则会覆盖。
        /// </summary>
        /// <param name="dbName">数据库文件名。为null或空串时不创建。</param>
        /// <param name="password">（可选）数据库密码，默认为空。</param>
        /// <exception cref="Exception"></exception>
        public static void CreateDB(string dbName)
        {
            if (!string.IsNullOrEmpty(dbName))
            {
                try
                {
                    SQLiteConnection.CreateFile(dbName);
                }
                catch (System.Data.SQLite.SQLiteException e)
                {
                    Tool.Message(e.Message);
                }
            }
        }

        /// <summary> 
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。 
        /// </summary> 
        /// <param name="sql">要执行的增删改的SQL语句。</param> 
        /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准。</param> 
        /// <returns></returns> 
        /// <exception cref="Exception"></exception>
        public int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    try
                    {
                        connection.Open();
                        command.CommandText = sql;
                        if (parameters.Length != 0)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        return command.ExecuteNonQuery();
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        Tool.Message(e.Message);
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// 批量处理数据操作语句
        /// 使用事务执行
        /// </summary>
        /// <param name="list">SQL语句集合。</param>
        /// <exception cref="Exception"></exception>
        public void ExecuteNonQueryBatch(List<KeyValuePair<string, SQLiteParameter[]>> list)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (System.Data.SQLite.SQLiteException e)
                {
                    Tool.Message(e.Message);
                }
                using (SQLiteTransaction tranction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        try
                        {
                            foreach (var item in list)
                            {
                                command.CommandText = item.Key;
                                if (item.Value != null)
                                {
                                    command.Parameters.AddRange(item.Value);
                                }
                                command.ExecuteNonQuery();
                            }
                            tranction.Commit();
                        }
                        catch (System.Data.SQLite.SQLiteException e)
                        {
                            tranction.Rollback();
                            Tool.Message(e.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string sql, string content)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    try
                    {
                        SQLiteParameter myParameter = new SQLiteParameter("@content", DbType.String);
                        myParameter.Value = content;
                        command.Parameters.Add(myParameter);
                        connection.Open();
                        return command.ExecuteNonQuery();
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        Tool.Message(e.Message);
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        if (parameters.Length != 0)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        return command.ExecuteNonQuery();
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        Tool.Message(e.Message);
                        return 0;
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，并返回第一个结果。
        /// </summary>
        /// <param name="sql">查询语句。</param>
        /// <returns>查询结果。</returns>
        /// <exception cref="Exception"></exception>
        public object GetSingle(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        if (parameters.Length != 0)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        return command.ExecuteScalar();
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        Tool.Message(e.Message);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters.Length != 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    try
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        return ds;
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        Tool.Message(e.Message);
                        return null;
                    }
                }
            }
        }

        /// <summary> 
        /// 执行一个查询语句，返回一个包含查询结果的DataTable。 
        /// </summary> 
        /// <param name="sql">要执行的查询语句。</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准。</param> 
        /// <returns></returns> 
        /// <exception cref="Exception"></exception>
        public DataTable GetDataTable(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters.Length != 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    try
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        Tool.Message(e.Message);
                        return null;
                    }
                }
            }
        }

        /// <summary> 
        /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例。 
        /// </summary> 
        /// <param name="sql">要执行的查询语句。</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准。</param> 
        /// <returns></returns> 
        /// <exception cref="Exception"></exception>
        public SQLiteDataReader ExecuteReader(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    try
                    {
                        if (parameters.Length != 0)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        connection.Open();
                        return command.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        Tool.Message(e.Message);
                        return null;
                    }
                }
            }
        }

        /// <summary> 
        /// 查询数据库中的所有数据类型信息。
        /// </summary> 
        /// <returns></returns> 
        /// <exception cref="Exception"></exception>
        public DataTable GetSchema()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return connection.GetSchema("TABLES");
                }
                catch (System.Data.SQLite.SQLiteException e)
                {
                    Tool.Message(e.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// 批量插入DataTable数据到Sqlite
        /// 使用事务
        /// </summary>
        /// <param name="tableName">导入的标名</param>
        /// <param name="dt"></param>
        public int ImportToSqliteBatch(string tableName, DataTable dt)
        {
            List<string> myFields = new List<string>();
            List<string> valueVars = new List<string>();// insert command text
            int colCount = dt.Columns.Count;

            for (int i = 0; i < colCount; i++)
            {
                string colName = dt.Columns[i].ColumnName;
                myFields.Add(colName);
                valueVars.Add("@" + colName);
            }
            string insertHead = string.Format("insert into {0} ({1})", tableName, string.Join(",", myFields.ToArray()));
            string insertCmdText = string.Format("{0} values ({1})", insertHead, string.Join(",", valueVars.ToArray()));
            string[] fields = myFields.ToArray();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (System.Data.SQLite.SQLiteException e)
                {
                    Tool.Message(e.Message);
                }
                using (SQLiteTransaction tranction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        int successNums = 0;
                        try
                        {
                            command.CommandText = insertCmdText;
                            foreach (DataRow dr in dt.Rows)
                            {
                                for (int i = 0; i < colCount; i++)
                                {
                                    object o = null;
                                    string paraName = "@" + fields[i];
                                    if (DBNull.Value != dr[fields[i]])
                                    {
                                        o = dr[fields[i]];
                                    }
                                    command.Parameters.AddWithValue(paraName, o);
                                }
                                successNums += command.ExecuteNonQuery();
                            }
                            tranction.Commit();
                            return successNums;
                        }
                        catch (System.Data.SQLite.SQLiteException e)
                        {
                            tranction.Rollback();
                            Tool.Message(e.Message);
                            return 0; 
                        }
                    }
                }
            }
        }

    }
}
