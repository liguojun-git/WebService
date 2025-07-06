using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace MCCMWebServiceApp.Infrastructure.SqlContext;

public class DapperHelper
{
   private readonly IDbConnection _dbConnection;
    public string ConnectionString => Dbcontext.ConnectionString;

    public DapperHelper()
    {
        if (string.IsNullOrWhiteSpace(Dbcontext.ConnectionString))
            throw new InvalidOperationException("データベース接続文字列未初期化です");

        _dbConnection = new SqlConnection(Dbcontext.ConnectionString);
    }

    /// <summary>
    ///  照会方法は単行です
    /// </summary>
    /// <typeparam name="T">マッピングの実体です</typeparam>
    /// <param name="sql">sql</param>
    /// <param name="param">パラメータオブジェクトです</param>
    /// <param name="transaction">事務です</param>
    /// <param name="commandTimeout">commandタイムアウト時間(秒)です。</param>
    /// <param name="commandType">伝送の種類です</param>
    /// <returns></returns>
    public T QueryFirstOrDefault<T>(
        string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        return _dbConnection.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);

    }

    /// <summary>
    ///  検索語句複数行
    /// </summary>
    /// <typeparam name="T">映射的实体</typeparam>
    /// <param name="sql">sql语句</param>
    /// <param name="param">参数对象</param>
    /// <param name="transaction">事务</param>
    ///  <param name="buffered">是否缓存结果</param>
    /// <param name="commandTimeout">command超时时间 (秒)</param>
    /// <param name="commandType">传输的类型,是sql语句还是存储过程还是表</param>
    /// 
    /// 
    /// <returns></returns>
    /// 

    /// <summary>
    ///  検索語句複数行
    /// </summary>
    /// <typeparam name="T">マッピングの実体です</typeparam>
    /// <param name="sql">sql</param>
    /// <param name="param">パラメータオブジェクトです</param>
    /// <param name="transaction">事務です</param>
    /// <param name="buffered">結果をキャッシュするかどうかです</param>
    /// <param name="commandTimeout">commandタイムアウト時間(秒)です。</param>
    /// <param name="commandType">伝送の種類です</param>
    /// <returns></returns>
    public IEnumerable<T> Query<T>(
        string sql,
        object param = null,
        IDbTransaction transaction = null,
        bool buffered = true,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        return _dbConnection.Query<T>(sql,param, transaction, buffered, commandTimeout, commandType);
    }

    /// <summary>
    /// トランザクション操作を実行します
    /// </summary>
    public T ExecuteTransaction<T>(Func<IDbTransaction, T> func)
    {
        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        using (var transaction = _dbConnection.BeginTransaction())
        {
            try
            {
                var result = func(transaction);
                transaction.Commit();
                return result;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }

    /// <summary>
    ///  執行方法(加筆・修正)です
    /// </summary>
    /// <param name="sql">sql</param>
    /// <param name="param">パラメータオブジェクトです</param>
    /// <param name="transaction">事務です</param>
    /// <param name="buffered">結果をキャッシュするかどうかです</param>
    /// <param name="commandTimeout">commandタイムアウト時間(秒)です。</param>
    /// <param name="commandType">伝送の種類です</param>
    /// <returns></returns>
    public int Execute(
        string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        return _dbConnection.Execute(sql, param, transaction, commandTimeout, commandType);
    }
        

}

