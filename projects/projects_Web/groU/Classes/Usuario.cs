using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace groU
{
    public class Usuario
    {
        //Datos del usuario
        private int idUsuario;
        private string nombreUsuario;
        private string nombre;
        private string apellidos;
        private string correo;
        private string contrasena;
        private string estado;
        private string bio;
        private string pais;
        private string ciudad;
        private int? telefono;
        private DateTime fechaNacimiento;
        private byte?[] fotoPerfil;
        private int numeroBonos;

        //Relaciones del usuario
        private List<int> contactosUsuario;
        private List<int> publicacionesUsuario;
        private List<int> publicacionesContactosUsuario;
        private List<int> publicacionesFavoritasUsuario;
        private List<int> eventosUsuario;
        private List<int> eventosActivosUsuario;



        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Contrasena { get => contrasena; set => contrasena = value; }
        public string Estado { get => estado; set => estado = value; }
        public string Bio { get => bio; set => bio = value; }
        public string Pais { get => pais; set => pais = value; }
        public string Ciudad { get => ciudad; set => ciudad = value; }
        public int? Telefono { get => telefono; set => telefono = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public byte?[] FotoPerfil { get => fotoPerfil; set => fotoPerfil = value; }
        public int NumeroBonos { get => numeroBonos; set => numeroBonos = value; }

        public List<int> ContactosUsuario { get => contactosUsuario; set => contactosUsuario = value; }
        public List<int> PublicacionesUsuario { get => publicacionesUsuario; set => publicacionesUsuario = value; }
        public List<int> PublicacionesContactosUsuario { get => publicacionesContactosUsuario; set => publicacionesContactosUsuario = value; }
        public List<int> PublicacionesFavoritasUsuario { get => publicacionesFavoritasUsuario; set => publicacionesFavoritasUsuario = value; }
        public List<int> EventosUsuario { get => eventosUsuario; set => eventosUsuario = value; }
        public List<int> EventosActivosUsuario { get => eventosActivosUsuario; set => eventosActivosUsuario = value; }



        public override string ToString()
        {
            string content = "";
            content += @"<div class='row'>
                           <div class='col-3 offset-1' align='center'>
                               @Imagen
                           </div>
                           <div class='col-7 offset-1' align='left'>
                               <h5>" + NombreUsuario + "(" + Correo + @")</h5>";

            if (Nombre != null)
            {
                content += "<h6>Nombre: " + Nombre + "</h6>";
            }
            if (Apellidos != null)
            {
                content += "<h6>Apellidos: " + Apellidos + "</h6>";
            }

            content += "<h6>Fecha de nacimiento: " + FechaNacimiento.ToString("d") + @"</h6>
                                </div>
                                <hr>
                            </div>

                            <div class='row'>
                                <div class='col-5' align='left'>
                                    <h5>Estado: </h5>
                                    <p>" + Estado + @"</p>
                                </div>
                                <div class='col-7' align='left'>
                                    <h5>Sobre mí: </h5>
                                    <p>" + Bio + @"</p>
                                </div>
                            </div>

                            <div class='row'>
                                <div class='col-10 offset-1' align='left'>
                                    <h5>Información de contacto: </h5>";
            if (Ciudad != null && Pais != null)
            {
                content += "<h6>" + Ciudad + "(" + Pais + ")</h6>";
            }
            else
            {
                if (Ciudad != null)
                {
                    content += "Ciudad: <h6>" + Ciudad + "</h6";
                }
                if (Pais != null)
                {
                    content += "País: <h6>" + Pais + "</h6";
                }
            }

            if (Telefono != null)
            {
                content += "<h6>Télefono: " + Telefono + "</h6>";
            }

            content +=      @"</div>
                            <div class='col-12' align='right'>
                                <p><a onclick='agregarContacto(" + IdUsuario + @")'><span>Añadir/eliminar contacto</span></a></p>
                                <h5><a onclick='verContactosContacto(" + IdUsuario + ")'><span>Ver Contactos de " + NombreUsuario + @"</span></a></h5>
                            </div>
                        </div>";

            return content;
        }

        public static string ToString(List<Usuario> users)
        {
            string content = "";

            foreach (Usuario user in users)
            {
                content += @"<div class='row'>
                                    <div class='col-10 offset-1 publicacion'>
                                        <div class='row'>
                                            <div class='col-12' align='left'>
                                                <h5><a onclick='verUsuario(" + user.IdUsuario + ")'><span>" + user.NombreUsuario + "(" + user.Correo + ")</span></a></h5>";
                if (user.Nombre != null && user.Apellidos != null)
                {
                    content += "<p>" + user.Nombre + " " + user.Apellidos + "</p>";
                }
                else
                {
                    if (user.Nombre != null)
                    {
                        content += "Nombre: <h6>" + user.Nombre + "</h6";
                    }
                    if (user.Apellidos != null)
                    {
                        content += "Apellidos: <h6>" + user.Apellidos + "</h6";
                    }
                }

                if (user.Ciudad != null && user.Pais != null)
                {
                    content += "<h6>" + user.Ciudad + "(" + user.Pais + ")</h6>";
                }
                else
                {
                    if (user.Ciudad != null)
                    {
                        content += "Ciudad: <h6>" + user.Ciudad + "</h6";
                    }
                    if (user.Pais != null)
                    {
                        content += "País: <h6>" + user.Pais + "</h6";
                    }
                }

                content +=                  @"</div>
                                        </div>
                                    </div>
                                </div>";
            }

            return content;
        }
    }
}