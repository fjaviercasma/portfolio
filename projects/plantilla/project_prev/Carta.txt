using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Carta
    {
        private int numero;
        private string[] numeros = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        private int palo;
        private string[] palos = { "Picas", "Corazones", "Treboles", "Rombos" };



        public Carta(int numero, int palo)
        {
            this.numero = numero;
            this.palo = palo;
        }


        public int obtenerNumero() 
        {
            return this.numero;
        }


        public int obtenerPalo()
        {
            return this.palo;
        }



        public void escribirCarta()
        {
            Console.WriteLine(this.numeros[this.numero] + " de " + this.palos[this.palo]);
        }
    }
}
