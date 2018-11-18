using System.Data;

namespace AutoHome.Training.Dapper
{
    public interface IRepositoryContextBase
    {
        bool Committed { get; set; }
        IDbConnection Conn { get; }
        IDbTransaction Tran { get; }

        void BeginTran();
        void Commit();
        void InitConnection();
    }
}