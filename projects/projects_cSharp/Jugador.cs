using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Jugador
    {
        private List<Carta> Cartas;
        private string nombre;



        public Jugador(string name)
        {
            this.Cartas = new List<Carta>();
            this.nombre = name;
        }


        public string obtenerNombre() 
        {
            return this.nombre;
        }



        public void robaCarta(Baraja baraja)
        {
            this.Cartas.Add(baraja.robaCarta());
        }


        public List<Carta> verCartas()
        {
            return this.Cartas;
        }
    }
}
