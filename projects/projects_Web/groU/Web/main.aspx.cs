using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace groU
{
    public partial class main : System.Web.UI.Page
    {
        private HttpCookie[] cookies;
        private Usuario user;

        protected void Page_Load(object sender, EventArgs e)
        {
            cookies = new HttpCookie[3];
            cookies[0] = Request.Cookies.Get("user");
            cookies[1] = Request.Cookies.Get("main");
            cookies[2] = Request.Cookies.Get("content");

            if (cookies[0] == null)
            {
                Response.Redirect("index.aspx");
            }

            user = new DALUsuario().ObtenerUsuario(Int32.Parse(cookies[0].Value));

            if (cookies[2].Value == "0")
            {
                loadSMT(null, null);
            }

            updateUserInfo_Click(null, null);
        }

        protected void endSession_Click(object sender, EventArgs e)
        {
            Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["main"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["content"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["idComentario"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["idPost"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["idEvento"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["idUsuario"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["subcomentario"].Expires = DateTime.Now.AddDays(-1);
            Session.Abandon();
            Response.Redirect("main.aspx");
        }


        //Método para cargar mas contenido
        protected void loadSMT(object sender, EventArgs e)
        {
            Refresh.Text = "";
            switch (cookies[1].Value)
            {
                case "0":
                    mainMain_Click(null, null);
                    break;
                case "1":
                    mainPosts_Click(null, null);
                    break;
                case "2":
                    mainContPost_Click(null, null);
                    break;
                case "3":
                    mainEvents_Click(null, null);
                    break;
                case "4":
                    mainContacts_Click(null, null);
                    break;
                case "5":
                    userAssistance_Click(null, null);
                    break;
                case "6":
                    userEvents_Click(null, null);
                    break;
                case "7":
                    favs_Click(null, null);
                    break;
                case "8":
                    showPost_Click(null, null);
                    break;
                case "9":
                    showComment_Click(null, null);
                    break;
                case "10":
                    showContactsContact_Click(null, null);
                    break;
                case "11":
                    showUsersEvent_Click(null, null);
                    break;
                case "12":
                    buscarPublicaciones_Click(null, null);
                    break;
                case "13":
                    buscarEventos_Click(null, null);
                    break;
                case "14":
                    buscarUsuarios_Click(null, null);
                    break;
            }
            Refresh.Text = "yes";
        }





        //Métodos del buscador
        protected void buscarPublicaciones_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            buscador.Style.Clear();
            buscarPublicaciones.Style.Clear();
            DoRefresh("12", "");
            mainContent.Text += Publicacion.ToString(new DALPublicacion().BuscarPublicaciones(buscador.Text, Int32.Parse(cookies[2].Value)));
            CargarContenido();
            Refresh.Text = "yes";
        }

        protected void buscarEventos_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            buscador.Style.Clear();
            buscarEventos.Style.Clear();
            DoRefresh("13", "");
            mainContent.Text += Evento.ToString(new DALEvento().BuscarEventos(buscador.Text, Int32.Parse(cookies[2].Value)));
            CargarContenido();
            Refresh.Text = "yes";
        }

        protected void buscarUsuarios_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            buscador.Style.Clear();
            buscarUsuarios.Style.Clear();
            DoRefresh("14", "");
            mainContent.Text += Usuario.ToString(new DALUsuario().BuscarUsuario(buscador.Text, Int32.Parse(cookies[2].Value)));
            CargarContenido();
            Refresh.Text = "yes";
        }





        //Métodos del menú de la derecha
        protected void mainMain_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            buscador.Style.Clear();
            buscarPublicaciones.Style.Clear();
            DoRefresh("0", "");
            mainContent.Text += Publicacion.ToString(new DALPublicacion().ObtenerPublicacionesDestacadas(Int32.Parse(cookies[2].Value)));
            CargarContenido();
        }

        protected void mainPosts_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            string mainText = @"<div class='row'>
					                <div class='col-12'>
						                <h3 class='contenthover' onclick='nuevaPublicacion()'>Crear publicación</h3>
					                </div>
				                </div>";
            DoRefresh("1", mainText);
            mainContent.Text += Publicacion.ToString(new DALPublicacion().ObtenerPublicacion(user.PublicacionesUsuario, Int32.Parse(cookies[2].Value)));
            CargarContenido();
        }

        protected void mainContPost_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            DoRefresh("2", "");
            mainContent.Text += Publicacion.ToString(new DALPublicacion().ObtenerPublicacion(user.PublicacionesContactosUsuario, Int32.Parse(cookies[2].Value)));
            CargarContenido();
        }

        protected void mainEvents_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            buscador.Style.Clear();
            buscarEventos.Style.Clear();
            DoRefresh("3", "");
            mainContent.Text += Evento.ToString(new DALEvento().ObtenerEventosActivos(Int32.Parse(cookies[2].Value)));
            CargarContenido();
        }

        protected void mainContacts_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            buscador.Style.Clear();
            buscarUsuarios.Style.Clear();
            DoRefresh("4", "");
            mainContent.Text += Usuario.ToString(new DALUsuario().ObtenerUsuario(user.ContactosUsuario, Int32.Parse(cookies[2].Value)));
            CargarContenido();
        }





        //Métodos del espacio que contiene la información del usuario en la izquierda
        protected void updateUserInfo_Click(object sender, EventArgs e)
        {
            userInfo.Text = @"
                            <div class='profImg'></div>
                            <hr>
                            <h3>" + user.NombreUsuario + @"</h3>
                            <h5>Estado: " + user.Estado + @"</h5>
                            <br>
                            <h4 class='contenthover'>Contactos: " + user.ContactosUsuario.Count + @"</h4>
                            <h4 class='contenthover' onclick='eventosUsuario()'>Mis eventos: " + user.EventosUsuario.Count + @"</h4>
                            <h4 class='contenthover' onclick='eventosAsistir()'>Eventos colaborados: " + user.EventosActivosUsuario.Count + @"</h4>
                            <h4 class='contenthover'>Mis publicaciones: " + user.PublicacionesUsuario.Count + @"</h4>
                            <h4 class='contenthover' onclick='favoritos()'>Favoritos: " + user.PublicacionesFavoritasUsuario.Count + @"</h4>
                            <h4 class='contenthover'>Bonos: " + user.NumeroBonos + "</h4>";
        }

        protected void userAssistance_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            CambiarContenido("5", "");
            mainContent.Text += Evento.ToString(new DALEvento().ObtenerEvento(user.EventosActivosUsuario, Int32.Parse(cookies[2].Value)));
            CargarContenido();
        }

        protected void userEvents_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            string mainText = @"<div class='row'>
					                <div class='col-12'>
						                <h3 class='contenthover' onclick='nuevoEvento()'>Crear Evento</button></h3>
					                </div>
				                </div>";
            DoRefresh("6", mainText);
            mainContent.Text += Evento.ToString(new DALEvento().ObtenerEvento(user.EventosUsuario, Int32.Parse(cookies[2].Value)));
            CargarContenido();
        }

        protected void favs_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            DoRefresh("7", "");
            mainContent.Text += Publicacion.ToString(new DALPublicacion().ObtenerPublicacion(user.PublicacionesFavoritasUsuario, Int32.Parse(cookies[2].Value)));
            CargarContenido();
        }




        //Métodos al clickear encima de un tipo de elemento(muestran información detallada del mismo y comentarios en caso de que los tenga)
        protected void showUser_Click(object sender, EventArgs e)
        {
            HttpCookie usuario = Request.Cookies.Get("idUsuario");
            DoRefresh("99", "");
            foreach (int id in user.ContactosUsuario)
            {
                if (id.ToString() == usuario.Value)
                {
                    mainForm.Text = "<h5>Es tu contacto<h5>";
                }
            }
            mainForm.Text += new DALUsuario().ObtenerUsuario(Int32.Parse(usuario.Value)).ToString();
        }

        protected void showEvent_Click(object sender, EventArgs e)
        {
            HttpCookie evento = Request.Cookies.Get("idEvento");
            DoRefresh("99", "");
            foreach (int id in user.EventosActivosUsuario)
            {
                if (id.ToString() == evento.Value)
                {
                    mainForm.Text = "<h5>Estás inscrito a este evento<h5>";
                }
            }
            mainForm.Text += new DALEvento().ObtenerEvento(Int32.Parse(evento.Value)).ToString();
        }

        protected void showPost_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            HttpCookie publicacion = Request.Cookies.Get("idPost");
            Publicacion post = new DALPublicacion().ObtenerPublicacion(Int32.Parse(publicacion.Value));

            DoRefresh("8", post.ToString());
            mainContent.Text += Comentario.ToString(new DALComentario().ObtenerComentario(post.ComentariosPublicacion, Int32.Parse(cookies[2].Value)));
            CargarContenido();
        }

        protected void showComment_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            HttpCookie comentario = Request.Cookies.Get("idComentario");
            Comentario comment = new DALComentario().ObtenerComentario(Int32.Parse(comentario.Value));

            if (Request.Cookies.Get("subcomentario") != null)
            { ReiniciarContenido("9", comment.ToString()); }
            else 
            { DoRefresh("9", comment.ToString()); }

            mainContent.Text += Comentario.ToString(new DALComentario().ObtenerComentario(comment.SubComentarios, Int32.Parse(cookies[2].Value)));
            CargarContenido();
        }

        protected void showContactsContact_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            HttpCookie contacto = Request.Cookies.Get("idUsuario");
            Usuario contactito = new DALUsuario().ObtenerUsuario(Int32.Parse(contacto.Value));
            DoRefresh("10", "");
            mainContent.Text += Usuario.ToString(new DALUsuario().ObtenerUsuario(contactito.ContactosUsuario, Int32.Parse(cookies[2].Value)));
            CargarContenido();
        }

        protected void showUsersEvent_Click(object sender, EventArgs e)
        {
            UpdateButton1.Style.Clear();
            HttpCookie evento = Request.Cookies.Get("idEvento");
            Evento eventito = new DALEvento().ObtenerEvento(Int32.Parse(evento.Value));
            DoRefresh("11", "");
            mainContent.Text += Usuario.ToString(new DALUsuario().ObtenerUsuario(eventito.UsuariosEvento, Int32.Parse(cookies[2].Value)));
            CargarContenido();
        }





        //Métodos para interactuar en la red social
        protected void mainNewPost_Click(object sender, EventArgs e)
        {
            Refresh.Text = "yes";
            new DALPublicacion().NuevaPublicacion(Int32.Parse(cookies[0].Value), "<p>" + Content.Text.Replace("\n", "<br>") + "</p>");
            user = new DALUsuario().ObtenerUsuario(Int32.Parse(cookies[0].Value));

            Content.Text = "";
            updateUserInfo_Click(null, null);
            mainPosts_Click(null, null);
        }

        protected void newEvent_Click(object sender, EventArgs e)
        {
            Refresh.Text = "yes";
            new DALEvento().NuevoEvento(Int32.Parse(cookies[0].Value), Title.Text, "<p>" + Content.Text.Replace("\n", "<br>") + "</p>", Date.Text);
            user = new DALUsuario().ObtenerUsuario(Int32.Parse(cookies[0].Value));

            Title.Text = "";
            Content.Text = "";
            Date.Text = "";
            updateUserInfo_Click(null, null);
            userEvents_Click(null, null);
        }

        protected void likePost_Click(object sender, EventArgs e)
        {
            Refresh.Text = "yes";
            HttpCookie post = Request.Cookies.Get("idPost");
            new DALUsuario().LikePost(Int32.Parse(cookies[0].Value), Int32.Parse(post.Value));
        }

        protected void commentPost_Click(object sender, EventArgs e)
        {
            Refresh.Text = "yes";
            HttpCookie comment = Request.Cookies.Get("idPost");
            new DALComentario().ComentarPublicacion(Int32.Parse(cookies[0].Value), Int32.Parse(comment.Value), Content.Text.Replace("\n", "<br>"));

            Content.Text = "";
            showPost_Click(null, null);
        }

        protected void commentComment_Click(object sender, EventArgs e)
        {
            Refresh.Text = "yes";
            HttpCookie toComment = Request.Cookies.Get("idComentario");
            new DALComentario().ComentarComentario(Int32.Parse(cookies[0].Value), Int32.Parse(toComment.Value), Content.Text.Replace("\n", "<br>"));

            Content.Text = "";
            showComment_Click(null, null);
        }

        protected void addContact_Click(object sender, EventArgs e)
        {
            Refresh.Text = "yes";
            HttpCookie contacto = Request.Cookies.Get("idUsuario");
            new DALUsuario().AgregarContacto(Int32.Parse(cookies[0].Value), Int32.Parse(contacto.Value));
            user = new DALUsuario().ObtenerUsuario(Int32.Parse(cookies[0].Value));
            updateUserInfo_Click(null, null);
            showUser_Click(null, null);
        }

        protected void addEvent_Click(object sender, EventArgs e)
        {
            Refresh.Text = "yes";
            HttpCookie evento = Request.Cookies.Get("idEvento");
            new DALUsuario().ApuntarseEvento(Int32.Parse(cookies[0].Value), Int32.Parse(evento.Value));
            user = new DALUsuario().ObtenerUsuario(Int32.Parse(cookies[0].Value));
            updateUserInfo_Click(null, null);
            showEvent_Click(null, null);
        }





        //Métodos auxiliares
        protected void DoRefresh(string content, string MainText)
        {
            if (Refresh.Text != "")
            { ReiniciarContenido(content, MainText); Refresh.Text = ""; }
            else
            { CambiarContenido(content, MainText); }
        }

        protected void CambiarContenido(string content, string MainText)
        {
            if (cookies[1].Value != content)
            {
                ReiniciarContenido(content, MainText);
            }
        }

        protected void ReiniciarContenido(string content, string MainText)
        {
            mainForm.Text = MainText;
            mainContent.Text = "";
            cookies[2].Expires = DateTime.Now.AddDays(-1);

            if (Request.Cookies["subcomentario"] != null)
            {
                Response.Cookies["subcomentario"].Expires = DateTime.Now.AddDays(-1);
            }

            cookies[2] = new HttpCookie("content", "0");
            Response.Cookies.Add(cookies[2]);

            HttpCookie cookie = new HttpCookie("main", content);
            Response.Cookies.Add(cookie);
        }

        protected void CargarContenido()
        {
            int index = Int32.Parse(cookies[2].Value);
            index += 10;
            HttpCookie cookie = new HttpCookie("content", index.ToString());
            Response.Cookies.Add(cookie);
        }
    }
}