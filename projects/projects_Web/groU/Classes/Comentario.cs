using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace groU
{
    public class Comentario
    {
        //Datos del comentario
        private int idComentario;
        private int RidUsuario;
        private int? RidComentario;
        private int RidPublicacion;
        private string contenido;
        private int numSubComentarios;

        //Relaciones del comentario
        private List<int> subComentarios;

        //Variables extras
        private string nombreCreador;


        public int IdComentario { get => idComentario; set => idComentario = value; }
        public int RIdUsuario { get => RidUsuario; set => RidUsuario = value; }
        public int? RIdComentario { get => RidComentario; set => RidComentario = value; }
        public int RIdPublicacion { get => RidPublicacion; set => RidPublicacion = value; }
        public string Contenido { get => contenido; set => contenido = value; }
        public int NumSubComentarios { get => numSubComentarios; set => numSubComentarios = value; }

        public List<int> SubComentarios { get => subComentarios; set => subComentarios = value; }

        public string NombreCreador { get => nombreCreador; set => nombreCreador = value; }



        public override string ToString()
        {
            return @"<div class='row'>
                        <div class='col-12' align='left'>
                            <h6><a onclick='verUsuario(" + RIdUsuario + ")'><span>" + NombreCreador + @"</span></a>: </h6>
                        </div>
                    </div>

                    <div class='row'>
                        <div class='col-10 offset-1' align='left'>"
                            + Contenido +
                        @"</div>
                    </div>

                    <div class='row'>
                        <div class='col-12' align='left'>
                            <p><a onclick='comentarComentario()'><span>Comentar</span></a></p>
                        </div>
                    </div>";
        }

        public static string ToString(List<Comentario> comments)
        {
            string content = "";

            foreach (Comentario comment in comments)
            {
                content += @"<div class='row'>
                                <div class='col-10 offset-1 publicacion' align='left'>
                                    <div class='row'>
                                        <div class='col-12'>
                                            <h6><a onclick='verUsuario(" + comment.RIdUsuario + ")'><span>" + comment.NombreCreador + @"</span></a>: </h6>
                                        </div>
                                    </div>
                                    <div class='row'>
                                        <div class='col-11 offset-1'>
                                            <p>" + comment.Contenido + @"</p>
                                        </div>
                                    </div>

                                    <div class='row'>
                                        <div class='col-12'>
                                            <p><a onclick='verComentario(" + comment.IdComentario + ")'><span>Comentarios(" + comment.NumSubComentarios + @")</span></a></p>
                                        </div>
                                    </div>
                                </div>
                            </div>";
            }

            return content;
        }
    }
}