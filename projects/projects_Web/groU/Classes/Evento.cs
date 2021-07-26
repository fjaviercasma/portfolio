using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace groU
{
    public class Evento
    {
        //Datos del evento
        private int idEvento;
        private int RidUsuario;
        private string nombreEvento;
        private byte?[] imagen;
        private string contenido;
        private DateTime fechaCreacion;
        private DateTime fechaEvento;

        //Relaciones del evento
        private List<int> usuariosEvento;

        //Variables extras
        private string nombreCreador;


        public int IdEvento { get => idEvento; set => idEvento = value; }
        public int RIdUsuario { get => RidUsuario; set => RidUsuario = value; }
        public string NombreEvento { get => nombreEvento; set => nombreEvento = value; }
        public byte?[] Imagen { get => imagen; set => imagen = value; }
        public string Contenido { get => contenido; set => contenido = value; }
        public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
        public DateTime FechaEvento { get => fechaEvento; set => fechaEvento = value; }

        public List<int> UsuariosEvento { get => usuariosEvento; set => usuariosEvento = value; }

        public string NombreCreador { get => nombreCreador; set => nombreCreador = value; }



        public override string ToString()
        {
            return @"<div class='row'>
                         <div class='col-10 offset-1' align='left'>
                             @Imagen
                         </div>
                     </div>

                     <div class='row'>
                         <div class='col-10 offset-1' align='left'>
                             <h5>" + NombreEvento + @"</h5>
                             <h6>Organizado por: <a onclick='verUsuario(" + RIdUsuario + ")'><span>" + NombreCreador + @"</span></a></h6>
                             <h6>Fecha evento: " + FechaEvento.ToString("d") + @"</h6>
                             <h6>Fecha creacion evento: " + FechaCreacion.ToString("d") + @"</h6>
                         </div>
                         <hr>
                         <div class='col-10 offset-1' align='left'>
                             <h5>Información del evento:</h5>
                             <h6>" + Contenido + @"</h6>
                         </div>
                         <div class='col-10 offset-1' align='left'>
                             <h5><a onclick='inscribirseEvento(" + IdEvento + @")'><span>Asistir/no asistir evento</span></a></h5>
                             <h5><a onclick='verUsuariosEvento(" + IdEvento + @")'><span>Ver lista de participantes</span></a></h5>
                         </div>
                     </div>";
        }

        public static string ToString(List<Evento> eventos)
        {
            string content = "";

            foreach (Evento evento in eventos)
            {
                content += @"<div class='row'>
                                    <div class='col-10 offset-1 publicacion'>
                                        <div class='row'>
                                            <div class='col-10 offset-1' align='left'>
                                                <h5><a onclick='mostrarEvento(" + evento.IdEvento + ")'><span>" + evento.NombreEvento + @"</span></a></h5>
                                                <h6>Organizado por: <a onclick='verUsuario(" + evento.RIdUsuario + ")'><span>" + evento.NombreCreador + @"</span></a></h6>
                                                <h6>Fecha evento: " + evento.FechaEvento.ToString("d") + @"</h6>
                                            </div>
                                        </div>
                                    </div>
                                </div>";
            }

            return content;
        }
    }
}