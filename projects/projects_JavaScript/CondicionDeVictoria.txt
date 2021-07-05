using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class CondicionDeVictoria
    {
        private List<Carta> cartas;
        private List<int> NumerosDiferentes;
        private List<int> NumerosCoinciden;
        private List<List<int>> PalosNumerosDiferentes;
        private int[] PalosIguales = { 0, 0, 0, 0 };
        private int[] puntuacion = { 0, 0 };



        public CondicionDeVictoria(Jugador jugador, List<Carta> mesa)
        {
            this.cartas = comprobarCartas(jugador, mesa);
            List<int> numeros = new List<int>();
            List<int> palos = new List<int>();

            foreach (Carta c in this.cartas)
            {
                numeros.Add(c.obtenerNumero());
                palos.Add(c.obtenerPalo());
            }

            this.NumerosDiferentes = new List<int>();
            this.NumerosCoinciden = new List<int>();
            this.PalosNumerosDiferentes = new List<List<int>>();
            asignarNumerosDiferentes(numeros, palos);

            asignarPalosIguales(palos);

            puntuacionMano();
            puntuacionCartaAlta();
        }


        public int[] obtenerPuntaje()
        {
            return this.puntuacion;
        }


        public List<Carta> obtenerCartas()
        {
            return this.cartas;
        }



        private List<Carta> comprobarCartas(Jugador jugador, List<Carta> mesa)
        {
            List<Carta> TotalCartas = new List<Carta>();
            List<Carta> CartasJugador = jugador.verCartas();


            for (int cont = 0; cont < CartasJugador.Count; cont++)
            {
                TotalCartas.Add(CartasJugador[cont]);
            }

            for (int cont = 0; cont < mesa.Count; cont++)
            {
                TotalCartas.Add(mesa[cont]);
            }

            return TotalCartas;
        }



        private void asignarPalosIguales(List<int> palos)
        {
            foreach (int p in palos)
            {
                switch (p)
                {
                    case 0:
                        this.PalosIguales[0] += 1;
                        break;
                    case 1:
                        this.PalosIguales[1] += 1;
                        break;
                    case 2:
                        this.PalosIguales[2] += 1;
                        break;
                    case 3:
                        this.PalosIguales[3] += 1;
                        break;
                    default:
                        break;
                }
            }
        }



        private void asignarNumerosDiferentes(List<int> numeros, List<int> palos)
        {
            List<int> NumerosDel = new List<int>();

            for (int IndicadorNumero = 0; IndicadorNumero < numeros.Count; IndicadorNumero++)
            {
                Boolean bypass = true;

                foreach (int b in NumerosDel) 
                {
                    if (IndicadorNumero == b) 
                    {
                        bypass = false;
                    }
                }

                if (bypass == true)
                {
                    List<int> PalosNumero = new List<int>();
                    this.NumerosDiferentes.Add(numeros[IndicadorNumero]);
                    PalosNumero.Add(palos[IndicadorNumero]);

                    int ContIguales = 1;

                    for (int IndicadorComparador = 0; IndicadorComparador < numeros.Count; IndicadorComparador++)
                    {
                        if (IndicadorNumero != IndicadorComparador)
                        {
                            if (numeros[IndicadorNumero] == numeros[IndicadorComparador])
                            {
                                ContIguales++;
                                NumerosDel.Add(IndicadorComparador);
                                PalosNumero.Add(palos[IndicadorComparador]);
                            }
                        }
                    }

                    this.NumerosCoinciden.Add(ContIguales);
                    this.PalosNumerosDiferentes.Add(PalosNumero);
                }
            }
        }



        private void puntuacionMano()
        {
            int trio = 0;
            int par = 0;


            foreach (int p in this.PalosIguales)
            {
                if (p >= 5)
                {
                    if (this.NumerosDiferentes.Count >= 5)
                    {
                        if (tieneEscalera() == true)
                        {
                            actualizarPuntuacionMano(8);
                        }
                    }
                    else
                    {
                        actualizarPuntuacionMano(5);
                    }
                }
            }

            foreach (int n in this.NumerosCoinciden)
            {
                switch (n)
                {
                    case 4:
                        actualizarPuntuacionMano(7);
                        break;
                    case 3:
                        trio++;
                        break;
                    case 2:
                        par++;
                        break;
                    default:
                        break;
                }
            }

            if (trio >= 1)
            {
                if (par >= 1)
                {
                    actualizarPuntuacionMano(6);
                }
                else
                {
                    actualizarPuntuacionMano(3);
                }
            }

            if (par >= 1)
            {
                if (par > 1)
                {
                    actualizarPuntuacionMano(2);
                }
                else
                {
                    actualizarPuntuacionMano(1);
                }
            }

            if (this.NumerosDiferentes.Count >= 5)
            {
                if (tieneEscalera() == true)
                {
                    actualizarPuntuacionMano(4);
                }
            }
        }



        private void actualizarPuntuacionMano(int p)
        {
            if (this.puntuacion[0] < p)
            {
                this.puntuacion[0] = p;
            }
        }



        private void puntuacionCartaAlta()
        {
            for (int cont = 0; cont < 2; cont++)
            {
                int CartaAlta = 0;

                foreach (int comparar in this.NumerosDiferentes)
                {
                    if (CartaAlta < comparar && cont >= 1 && comparar < this.puntuacion[1])
                    {
                        CartaAlta = comparar;
                    }
                    else if (CartaAlta < comparar)
                    {
                        CartaAlta = comparar;
                    }
                }

                this.puntuacion[1] = this.puntuacion[1] + CartaAlta;
            }
        }



        private Boolean tieneEscalera()
        {
            int[] NumerosOrdenados = new int[this.NumerosDiferentes.Count];
            List<int>[] PalosOrdenados = new List<int>[this.PalosNumerosDiferentes.Count];
            int escalera = 1;



            for (int cont = 0; cont < this.NumerosDiferentes.Count; cont++)
            {
                NumerosOrdenados[cont] = 0;

                for (int comp = 0; comp < this.NumerosDiferentes.Count; comp++)
                {
                    if (NumerosOrdenados[cont] < this.NumerosDiferentes[comp] && (cont >= 1 && this.NumerosDiferentes[comp] < NumerosOrdenados[cont - 1]))
                    {
                        NumerosOrdenados[cont] = this.NumerosDiferentes[comp];
                        PalosOrdenados[cont] = this.PalosNumerosDiferentes[comp];
                    }
                    else if (NumerosOrdenados[cont] < this.NumerosDiferentes[comp] && cont == 0)
                    {
                        NumerosOrdenados[cont] = this.NumerosDiferentes[comp];
                        PalosOrdenados[cont] = this.PalosNumerosDiferentes[comp];
                    }
                }
            }

            for (int cont = 0; cont < this.NumerosDiferentes.Count; cont++)
            {
                Console.Write(NumerosOrdenados[cont] + " ");

                Console.Write("\n");
            }
            Console.Write("\n");

            for (int cont = 0; cont < this.PalosNumerosDiferentes.Count; cont++)
            {
                foreach (int l in PalosOrdenados[cont]) 
                {
                    Console.Write(l + " ");
                }
                Console.Write("\n");
            }
            Console.Write("\n");

            for (int cont = 1; cont < this.NumerosDiferentes.Count; cont++)
            {
                if (escalera < 5)
                {
                    if (NumerosOrdenados[cont - 1] - 1 == NumerosOrdenados[cont])
                    {
                        escalera++;
                    }
                    else
                    {
                        escalera = 1;
                    }
                }
                else if (escalera >= 5 && NumerosOrdenados[0] == 13)
                {
                    actualizarPuntuacionMano(9);
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}
