using JuanJoseVillalbaRuiz.ViewModel;
using SQLite;
using System;

namespace JuanJoseVillalbaRuiz
{
    public partial class Notas : ContentPage
    {
        public Notas()
        {
            InitializeComponent();
        }
        public class Note
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}


