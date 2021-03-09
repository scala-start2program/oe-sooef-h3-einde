using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Scala.Oefening03.Core
{

    public class Vloot
    {
        private string currentOrder = "ASC";
        public List<Voertuig> Voertuigen { get; private set; }
        public Vloot()
        {
            Voertuigen = new List<Voertuig>();
            Seeding();
        }
        private void Seeding()
        {
            Voertuigen.Add(new Voertuig("BMW", "118i", "wit", "1-ABC-123", 100, new DateTime(2021, 02, 01)));
            Voertuigen.Add(new Voertuig("Peugeot", "3008", "bruin", "1-DEF-456", 110, new DateTime(2020, 05, 06)));
            Voertuigen.Add(new Voertuig("Opel", "Astra", "zilver", "1-GHI-789", 80, new DateTime(2019, 11, 23)));
            Voertuigen.Add(new Voertuig("Vokswagen", "Transporter", "wit", "1-JKL-012", 105, new DateTime(2018, 7, 23)));
            Sort();
        }
        public void AddVoertuig(Voertuig nieuwVoertuig)
        {
            Voertuigen.Add(nieuwVoertuig);
            Sort(currentOrder);
        }
        public void RemoveVoertuig(Voertuig teWissenVoertuig)
        {
            Voertuigen.Remove(teWissenVoertuig);
            Sort(currentOrder);
        }
        public void Sort(string order = "ASC")
        {
            if(order.ToUpper() == "ASC")
            {
                currentOrder = order;
                Voertuigen = Voertuigen.OrderBy(o => o.InDienst).ToList();
            }
            else if (order.ToUpper() == "DESC")
            {
                currentOrder = order;
                Voertuigen = Voertuigen.OrderByDescending(o => o.InDienst).ToList();
            }
            else
            {
                currentOrder = "unknown";
            }
        }


    }
}
