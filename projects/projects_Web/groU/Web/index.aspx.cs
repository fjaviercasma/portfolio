using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace groU
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies.Get("user") != null)
            {
                Response.Redirect("main.aspx");
            }
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            int IdUsuario = new DALUsuario().IniciarSesion(loginUser.Text, loginPass.Text);

            if (IdUsuario != 0)
            {
                HttpCookie session = new HttpCookie("user", IdUsuario.ToString());
                HttpCookie cookie1 = new HttpCookie("main", "0");
                HttpCookie cookie2 = new HttpCookie("content", "0");
                Response.Cookies.Add(session);
                Response.Cookies.Add(cookie1);
                Response.Cookies.Add(cookie2);
                Response.Redirect("main.aspx");
            }
        }

        protected void signBtn_Click(object sender, EventArgs e)
        {
            int IdUsuario = new DALUsuario().Registrarse(signUser.Text, signMail.Text, signPass.Text, signDate.Text);

            if (IdUsuario != 0)
            {
                HttpCookie session = new HttpCookie("user", IdUsuario.ToString());
                HttpCookie cookie1 = new HttpCookie("main", "0");
                HttpCookie cookie2 = new HttpCookie("content", "0");
                Response.Cookies.Add(session);
                Response.Cookies.Add(cookie1);
                Response.Cookies.Add(cookie2);
                Response.Redirect("main.aspx");
            }
        }
    }
}