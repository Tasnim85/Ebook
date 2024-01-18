using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Books
{
    public class Categorie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nom { get; set; }
    }
}
