using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace groU
{
    public class DALPublicacion
    {
        DbConnect MainCnx;
        DbConnect AuxCnx;
        Publicacion post;

        public DALPublicacion()
        {
            MainCnx = new DbConnect();
        }

        public Publicacion ObtenerPublicacion(int IdPublicacion)
        {
            try
            {
                SqlDataReader reader = MainCnx.CreateQuery("SELECT * FROM Publicaciones WHERE " + IdPublicacion + " = IdPublicacion");
                post = new Publicacion();

                while (reader.Read())
                {
                    post.IdPublicacion = (int)reader["IdPublicacion"];
                    post.RIdUsuario = (int)reader["RIdUsuario"];
                    post.FechaPublicacion = (DateTime)reader["FechaPublicacion"];
                    post.Likes = (int)reader["Likes"];
                    post.Contenido = (string)groUtils.GestionarNulos(reader["Contenido"]);
                    post.Imagen = (byte?[])groUtils.GestionarNulos(reader["Imagen"]);
                    post.NumComentarios = (int)reader["NumComentarios"];
                }
                reader.Close();

                post.ComentariosPublicacion = new List<int>();
                reader = MainCnx.CreateQuery("SELECT IdComentario FROM Comentarios WHERE " + IdPublicacion + " = RIdPublicacion AND RIdComentario IS NULL ORDER BY IdComentario DESC");
                while (reader.Read())
                {
                    post.ComentariosPublicacion.Add((int)reader["IdComentario"]);
                }
                reader.Close();

                post.NombreCreador = new DALUsuario().ObtenerUsuario(post.RIdUsuario).NombreUsuario;

                return post;
            }
            catch (Exception e) { return null; }
        }

        public List<Publicacion> ObtenerPublicacion(List<int> IdsPosts, int index)
        {
            List<Publicacion> posts = new List<Publicacion>();
            int cont = 0;

            foreach (int IdPost in IdsPosts)
            {
                if (cont >= index && cont < index + 10)
                {
                    posts.Add(ObtenerPublicacion(IdPost));
                }
                cont++;
            }

            return posts;
        }

        public List<Publicacion> ObtenerPublicacionesDestacadas(int index)
        {
            try
            {
                AuxCnx = new DbConnect();
                List<Publicacion> posts = new List<Publicacion>();
                int cont = 0;

                SqlDataReader reader = AuxCnx.CreateQuery("SELECT * FROM Publicaciones ORDER BY IdPublicacion DESC, Likes DESC");
                while (reader.Read())
                {
                    if (cont >= index && cont < index + 10)
                    {
                        posts.Add(ObtenerPublicacion((int)reader["IdPublicacion"]));
                    }
                    cont++;
                }
                reader.Close();

                return posts;
            }
            catch (Exception e) { return null; }
        }

        public void NuevaPublicacion(int IdUsuario, string contenido)
        {
            try
            {
                string cadena = @"EXEC dbo.NuevaPublicacion
                                        @RIdUsuario,
                                        @Contenido";
                SqlCommand comando = new SqlCommand(cadena, MainCnx.Db);

                SqlParameter RIdUsuario = new SqlParameter("@RIdUsuario", System.Data.SqlDbType.Int);
                RIdUsuario.Value = IdUsuario;
                SqlParameter Contenido = new SqlParameter("@Contenido", System.Data.SqlDbType.VarChar, 500);
                Contenido.Value = contenido;

                comando.Parameters.Add(RIdUsuario);
                comando.Parameters.Add(Contenido);

                comando.ExecuteNonQuery();
            }
            catch (Exception e) { }
        }

        public List<Publicacion> BuscarPublicaciones(string search, int index)
        {
            try
            {
                AuxCnx = new DbConnect();
                List<Publicacion> posts = new List<Publicacion>();
                int cont = 0;

                SqlDataReader reader = AuxCnx.CreateQuery("SELECT * FROM Publicaciones WHERE Contenido LIKE '%" + search + "%' ORDER BY FechaPublicacion DESC, Likes DESC");
                while (reader.Read())
                {
                    if (cont >= index && cont < index + 10)
                    {
                        posts.Add(ObtenerPublicacion((int)reader["IdPublicacion"]));
                    }
                    cont++;
                }
                reader.Close();

                return posts;
            }
            catch (Exception e) { return null; }
        }
    }
}