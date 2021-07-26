using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace groU
{
    public class DALComentario
    {
        DbConnect MainCnx;
        Comentario comment;

        public DALComentario()
        {
            MainCnx = new DbConnect();
        }

        public Comentario ObtenerComentario(int IdComentario)
        {
            try
            {
                SqlDataReader reader = MainCnx.CreateQuery("SELECT * FROM Comentarios WHERE " + IdComentario + " = IdComentario");
                comment = new Comentario();

                while (reader.Read())
                {
                    comment.IdComentario = (int)reader["IdComentario"];
                    comment.RIdUsuario = (int)reader["RIdUsuario"];
                    comment.RIdComentario = (int?)groUtils.GestionarNulos(reader["RIdComentario"]);
                    comment.RIdPublicacion = (int)reader["RIdPublicacion"];
                    comment.Contenido = (string)groUtils.GestionarNulos(reader["Comentario"]);
                    comment.NumSubComentarios = (int)reader["NumSubComentarios"];
                }
                reader.Close();

                comment.SubComentarios = new List<int>();
                reader = MainCnx.CreateQuery("SELECT IdComentario FROM Comentarios WHERE " + IdComentario + " = RIdComentario ORDER BY IdComentario DESC");
                while (reader.Read())
                {
                    comment.SubComentarios.Add((int)reader["IdComentario"]);
                }
                reader.Close();

                comment.NombreCreador = new DALUsuario().ObtenerUsuario(comment.RIdUsuario).NombreUsuario;

                return comment;
            }
            catch (Exception e) { return null; }
        }

        public List<Comentario> ObtenerComentario(List<int> IdsComments, int index)
        {
            List<Comentario> comments = new List<Comentario>();
            int cont = 0;

            foreach (int IdComment in IdsComments)
            {
                if (cont >= index && cont < index + 10)
                {
                    comments.Add(ObtenerComentario(IdComment));
                }
                cont++;
            }

            return comments;
        }

        public void ComentarPublicacion(int IdUsuario, int IdPublicacion, string contenido)
        {
            try
            {
                string newPost = @"EXEC dbo.ComentarPublicacion
                                    @RIdUsuario,
                                    @RIdPublicacion,
                                    @Comentario";
                SqlCommand cmd = new SqlCommand(newPost, MainCnx.Db);

                SqlParameter RIdUsuario = new SqlParameter("@RIdUsuario", System.Data.SqlDbType.Int);
                RIdUsuario.Value = IdUsuario;
                SqlParameter RIdPublicacion = new SqlParameter("@RIdPublicacion", System.Data.SqlDbType.Int);
                RIdPublicacion.Value = IdPublicacion;
                SqlParameter Comentario = new SqlParameter("@Comentario", System.Data.SqlDbType.VarChar, 300);
                Comentario.Value = contenido;

                cmd.Parameters.Add(RIdUsuario);
                cmd.Parameters.Add(RIdPublicacion);
                cmd.Parameters.Add(Comentario);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { }
        }

        public void ComentarComentario(int IdUsuario, int IdComentario, string contenido)
        {
            try
            {
                string newPost = @"EXEC dbo.ComentarComentario
                                    @RIdUsuario,
                                    @RIdComentario,
                                    @Comentario";
                SqlCommand cmd = new SqlCommand(newPost, MainCnx.Db);

                SqlParameter RIdUsuario = new SqlParameter("@RIdUsuario", System.Data.SqlDbType.Int);
                RIdUsuario.Value = IdUsuario;
                SqlParameter RIdComentario = new SqlParameter("@RIdComentario", System.Data.SqlDbType.Int);
                RIdComentario.Value = IdComentario;
                SqlParameter Comentario = new SqlParameter("@Comentario", System.Data.SqlDbType.VarChar, 300);
                Comentario.Value = contenido;

                cmd.Parameters.Add(RIdUsuario);
                cmd.Parameters.Add(RIdComentario);
                cmd.Parameters.Add(Comentario);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { }
        }
    }
}