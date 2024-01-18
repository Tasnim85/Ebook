using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Books
{
    public class ArticlePanier
    {
        public int IdProduit { get; set; }
        public string UrlImage { get; set; }
        public string NomProduit { get; set; }
        public decimal PrixUnitaire { get; set; }
        public int Quantite { get; set; }
        public decimal PrixTotale => PrixUnitaire * Quantite;
        
    }
}
