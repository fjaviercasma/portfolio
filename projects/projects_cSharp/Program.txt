using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] jugadores = { "Pepe", "Juan", "Manolo", "Eugenio" };


            Jugar(jugadores.Length, jugadores);

            Console.ReadLine();
        }




        static private void Jugar(int NumJug, string[] nombres) 
        {
            List<Jugador> jugadores = new List<Jugador>();


            // Creamos los jugadores
            for (int cont = 0; cont < NumJug; cont++)
            {
                jugadores.Add(new Jugador(nombres[cont]));
            }

            // Creamos un bucle para el juego
            JugarRonda(jugadores);
        }



        static private void JugarRonda(List<Jugador> jugadores)
        {
            Baraja baraja = new Baraja();
            List<Carta> CartasMesa = new List<Carta>();
            List<CondicionDeVictoria> puntuaciones = new List<CondicionDeVictoria>();
            int[] ganador = { 0, 0, 0 };


            baraja.Barajar();

            // Repartimos las cartas a cada jugador
            for (int cont = 0; cont < 2; cont++)
            {
                foreach (Jugador j in jugadores)
                {
                    j.robaCarta(baraja);
                }
            }

            // Repartimos las cartas de la mesa
            for (int cont = 0; cont < 5; cont++)
            {
                CartasMesa.Add(baraja.robaCarta());
            }

            // Turno 1


            // Turno 2


            // Creamos las puntuaciones de cada jugador
            foreach (Jugador j in jugadores) 
            {
                puntuaciones.Add(new CondicionDeVictoria(j, CartasMesa));
            }

            // Que gane el que tenga mayor puntuacion
            for (int cont = 0; cont < puntuaciones.Count; cont++) 
            {
                int[] puntaje = puntuaciones[cont].obtenerPuntaje();

                if (ganador[0] < puntaje[0])
                {
                    ganador[0] = puntaje[0];
                    ganador[1] = puntaje[1];
                    ganador[2] = cont;
                }
                else if (ganador[0] == puntaje[0]) 
                {
                    if (ganador[1] < puntaje[1])
                    {
                        ganador[1] = puntaje[1];
                        ganador[2] = cont;
                    }
                }
            }

            // Anunciamos el ganador de la ronda
            Console.WriteLine("El jugador "+ jugadores[ganador[2]].obtenerNombre() + " ha ganado esta ronda");

            //Comprobamos que todo funcione correctamente
            int cont23 = 0;
            foreach (CondicionDeVictoria cw in puntuaciones) 
            {
                Console.WriteLine(jugadores[cont23].obtenerNombre());
                foreach (Carta c in cw.obtenerCartas()) 
                {
                    c.escribirCarta();
                }
                cont23++;
            }
        }
    }
}
