using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace groU
{
    public class Publicacion
    {
        //Datos de la publicación
        private int idPublicacion;
        private int RidUsuario;
        private DateTime fechaPublicacion;
        private int likes;
        private string contenido;
        private byte?[] imagen;
        private int numComentarios;

        //Relaciones de la publicación
        private List<int> comentariosPublicacion;

        //Variables extras
        private string nombreCreador;


        public int IdPublicacion { get => idPublicacion; set => idPublicacion = value; }
        public int RIdUsuario { get => RidUsuario; set => RidUsuario = value; }
        public DateTime FechaPublicacion { get => fechaPublicacion; set => fechaPublicacion = value; }
        public int Likes { get => likes; set => likes = value; }
        public string Contenido { get => contenido; set => contenido = value; }
        public byte?[] Imagen { get => imagen; set => imagen = value; }
        public int NumComentarios { get => numComentarios; set => numComentarios = value; }

        public List<int> ComentariosPublicacion { get => comentariosPublicacion; set => comentariosPublicacion = value; }

        public string NombreCreador { get => nombreCreador; set => nombreCreador = value; }



        public override string ToString()
        {
            return @"<div class='row'>
                         <div class='col-12'>
                             @Imagen
                         </div>
                         <div class='col-12' align='left'>
                             <br>
                             <h6>" + Contenido + @"</h6>
                             <br>
                         </div>
                     </div>
                   
                     <div class='row' align='left'>
                         <div class='col-10 offset-1'>
                             <p><a onclick='darLikePublicacion(" + IdPublicacion + ")'><span>Like(" + Likes + @")</span></a>/<a onclick='comentarPublicacion()'><span>Comentar</span></a></p>
                         </div>
                     </div>
                   
                     <div class='row' align='right'>
                         <div class='col-12'>
                             <p>Publicado por: <a onclick='verUsuario(" + RIdUsuario + ")'><span>" + NombreCreador + "</span></a> el " + FechaPublicacion.ToString("d") + @"</p>
                         </div>
                     </div>";
        }

        public static string ToString(List<Publicacion> posts)
        {
            string content = "";

            foreach (Publicacion post in posts)
            {
                content += @"<div class='row'>
                                <div class='col-10 offset-1 publicacion'>
                                    <div class='row'>
                                        <div class='col-12'>
                                            @Imagen
                                        </div>
                                        <div class='col-12' align='left'>
                                            <br>
                                            <h6>" + post.Contenido + @"</h6>
                                            <br>
                                        </div>
                                    </div>

                                    <div class='row' align='left'>
                                        <div class='col-10 offset-1'>
                                            <p><a onclick='darLikePublicacion(" + post.IdPublicacion + ")'><span>Like(" + post.Likes + ")</span></a>/<a onclick='verPublicacion(" + post.IdPublicacion + ")'><span>Comentarios(" + post.NumComentarios + @")</span></a></p>
                                        </div>
                                    </div>

                                    <div class='row' align='right'>
                                        <div class='col-12'>
                                            <p>Publicado por: <a onclick='verUsuario(" + post.RIdUsuario + ")'><span>" + post.NombreCreador + "</span></a> el " + post.FechaPublicacion.ToString("d") + @"</p>
                                        </div>
                                    </div>
                                </div>
                            </div>";
            }

            return content;
        }
    }
}