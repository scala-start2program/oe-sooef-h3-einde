using System;
using System.Collections.Generic;
using System.Text;

namespace Scala.Oefening03.Core
{
    public class Voertuig
    {
        public string Merk { get; set; }
        public string Serie { get; set; }
        public string Kleur { get; set; }
        public string Nummerplaat { get; set; }
        public int KW { get; set; }
        public DateTime InDienst { get; set; }

        public Voertuig()
        { }
        public Voertuig(string merk, string serie, string kleur, string nummerplaat, int kw, DateTime inDienst)
        {
            Merk = merk;
            Serie = serie;
            Kleur = kleur;
            Nummerplaat = nummerplaat;
            KW = kw;
            InDienst = inDienst;
        }
        public override string ToString()
        {
            return $"{Merk} - {Serie}";
        }
    }
}
