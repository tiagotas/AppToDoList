using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using AppToDoList.Model;
using System.Threading.Tasks;

namespace AppToDoList.Helper
{
    public class SQLiteDataBaseHelper
    {
        readonly SQLiteAsyncConnection _connection;

        public SQLiteDataBaseHelper(string path)
        {
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Tarefa>().Wait();
        }

        public Task<int> Save(Tarefa t)
        {
            return _connection.InsertAsync(t);
        }

        public Task<List<Tarefa>> GetAllRows()
        {
            return _connection.Table<Tarefa>().ToListAsync();
        }

        public Task<int> Delete(int id)
        {
            return _connection.Table<Tarefa>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Tarefa>> Update (Tarefa t)
        {
            string sql = "UPDATE tarefa SET " +
                         "Descricao=?, Data_criacao=?, Data_conclusao=? " +
                         "WHERE Id=?";

            return _connection.QueryAsync<Tarefa>(sql,
                t.Descricao, t.Data_Criacao, t.Data_Conclusao, t.Id);
        }

        public Task<List<Tarefa>> Search(string q)
        {
            string sql = "SELECT * FROM tarefa WHERE Descricao LIKE '%" + q + "%'";

            return _connection.QueryAsync<Tarefa>(sql);
        }
    }
}
