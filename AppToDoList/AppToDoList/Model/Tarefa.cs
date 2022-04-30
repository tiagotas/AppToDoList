using SQLite;
using System;

namespace AppToDoList.Model
{
    public class Tarefa
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data_Criacao { get; set; }
        public DateTime Data_Conclusao { get; set; }   
    }
}
