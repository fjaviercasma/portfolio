using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Baraja
    {
        private Random random = new Random();

        private List<Carta> ListaDeCartas;



        public Baraja()
        {
            this.ListaDeCartas = new List<Carta>();


            for (int palo = 0; palo < 4; palo++)
            {
                for (int numero = 0; numero < 14; numero++)
                {
                    this.ListaDeCartas.Add(new Carta(numero, palo));
                }
            }
        }



        public void numeroCartas()
        {
            Console.WriteLine(this.ListaDeCartas.Count);
        }


        public Carta robaCarta()
        {
            Carta carta = this.ListaDeCartas[0];
            this.ListaDeCartas.RemoveAt(0);
            return carta;
        }


        public Carta cogeCarta(int posicion)
        {
            Carta carta = this.ListaDeCartas[posicion];
            this.ListaDeCartas.RemoveAt(posicion);
            return carta;
        }


        public Carta cogeCartaAlAzar()
        {
            int posicion = this.random.Next(0, this.ListaDeCartas.Count);

            Carta carta = this.ListaDeCartas[posicion];
            this.ListaDeCartas.RemoveAt(posicion);
            return carta;
        }


        public void escribeBaraja()
        {
            foreach (Carta c in this.ListaDeCartas)
            {
                c.escribirCarta();
            }
        }


        public void Barajar()
        {
            List<Carta> ShuffleListaDeCartas = new List<Carta>();


            while (this.ListaDeCartas.Count > 0)
            {
                int RandNum = this.random.Next(0, this.ListaDeCartas.Count);
                ShuffleListaDeCartas.Add(this.ListaDeCartas[RandNum]);
                this.ListaDeCartas.RemoveAt(RandNum);
            }

            this.ListaDeCartas = ShuffleListaDeCartas;
        }
    }
}
