using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.Configuration;

/// <summary>
///DBHelper：数据库访问操作类
/// </summary>
public class DBHelper
{
    /// <summary>
    /// 更新操作：增，删，改 共用
    /// </summary>
    /// <param name="sql"></param>
    /// <returns>bool</returns>
    public static bool UpdateOpera(string sql)
    {
        OleDbCommand cmd = new OleDbCommand(sql, Connection);
        return cmd.ExecuteNonQuery() > 0;
    }

    /// <summary>
    /// 多行查询操作：返回OleDbDataReader
    /// </summary>
    /// <param name="sql">查询SQL语句</param>
    /// <returns>OleDbDataReader</returns>
    public static OleDbDataReader GetReader(string sql)
    {
        OleDbCommand cmd = new OleDbCommand(sql, Connection);
        return cmd.ExecuteReader(CommandBehavior.CloseConnection);
    }

    /// <summary>
    /// 多行查询操作：返回DataTable
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static DataTable GetDataTable(string sql)
    {
        DataTable dt = new DataTable();
        OleDbDataAdapter dad = new OleDbDataAdapter(sql, Connection);
        dad.Fill(dt);
        return dt;
    }

    /// <summary>
    /// 查询操作：返回首行首列数据
    /// </summary>
    /// <param name="sql">查询SQL语句</param>
    /// <returns>object</returns>
    public static object GetScalar(string sql)
    {
        OleDbCommand cmd = new OleDbCommand(sql, Connection);
        return cmd.ExecuteScalar();
    }

    private static OleDbConnection _connection;
    /// <summary>
    /// Connection对象
    /// </summary>
    public static OleDbConnection Connection
    {
        get
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=data.accdb;Persist Security Info=False";
            if (_connection == null)
            {
                _connection = new OleDbConnection(connectionString);
                _connection.Open();
            }
            else if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            else if (_connection.State == ConnectionState.Broken || _connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection.Open();
            }
            return _connection;
        }
    }
}