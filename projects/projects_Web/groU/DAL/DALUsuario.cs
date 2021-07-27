using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace groU
{
    public class DALUsuario
    {
        DbConnect MainCnx;
        DbConnect AuxCnx;
        Usuario user;

        public DALUsuario()
        {
            MainCnx = new DbConnect();
        }

        public Usuario ObtenerUsuario(int IdUsuario)
        {
            try
            {
                SqlDataReader reader = MainCnx.CreateQuery("SELECT * FROM Usuarios WHERE " + IdUsuario + " = IdUsuario");
                user = new Usuario();

                while (reader.Read())
                {
                    user.IdUsuario = (int)reader["IdUsuario"];
                    user.NombreUsuario = (string)reader["NombreUsuario"];
                    user.Nombre = (string)groUtils.GestionarNulos(reader["Nombre"]);
                    user.Apellidos = (string)groUtils.GestionarNulos(reader["Apellidos"]);
                    user.Correo = (string)reader["Correo"];
                    user.Contrasena = (string)reader["Contrasena"];
                    user.Estado = (string)groUtils.GestionarNulos(reader["Estado"]);
                    user.Bio = (string)groUtils.GestionarNulos(reader["Bio"]);
                    user.Pais = (string)groUtils.GestionarNulos(reader["Pais"]);
                    user.Ciudad = (string)groUtils.GestionarNulos(reader["Ciudad"]);
                    user.Telefono = (int?)groUtils.GestionarNulos(reader["Telefono"]);
                    user.FechaNacimiento = (DateTime)reader["FechaNacimiento"];
                    user.FotoPerfil = (byte?[])groUtils.GestionarNulos(reader["FotoPerfil"]);
                    user.NumeroBonos = (int)groUtils.GestionarNulos(reader["NumeroBonos"]);
                }
                reader.Close();

                user.ContactosUsuario = new List<int>();
                reader = MainCnx.CreateQuery("SELECT IdUsuario FROM dbo.MostrarContactos(" + IdUsuario + ") ORDER BY NombreUsuario ASC");
                while (reader.Read())
                {
                    user.ContactosUsuario.Add((int)reader["IdUsuario"]);
                }
                reader.Close();

                user.PublicacionesUsuario = new List<int>();
                reader = MainCnx.CreateQuery("SELECT IdPublicacion FROM dbo.MisPublicaciones(" + IdUsuario + ") ORDER BY IdPublicacion DESC");
                while (reader.Read())
                {
                    user.PublicacionesUsuario.Add((int)reader["IdPublicacion"]);
                }
                reader.Close();

                user.PublicacionesContactosUsuario = new List<int>();
                reader = MainCnx.CreateQuery("SELECT IdPublicacion FROM dbo.PublicacionesContactos(" + IdUsuario + ") ORDER BY IdPublicacion DESC, Likes DESC");
                while (reader.Read())
                {
                    user.PublicacionesContactosUsuario.Add((int)reader["IdPublicacion"]);
                }
                reader.Close();

                user.PublicacionesFavoritasUsuario = new List<int>();
                reader = MainCnx.CreateQuery("SELECT RIdPublicacion FROM LikePublicacion WHERE " + IdUsuario + " = RIdUsuario ORDER BY RIdPublicacion DESC");
                while (reader.Read())
                {
                    user.PublicacionesFavoritasUsuario.Add((int)reader["RIdPublicacion"]);
                }
                reader.Close();

                user.EventosUsuario = new List<int>();
                reader = MainCnx.CreateQuery("SELECT IdEvento FROM Eventos WHERE " + IdUsuario + " = RIdUsuario ORDER BY IdEvento DESC");
                while (reader.Read())
                {
                    user.EventosUsuario.Add((int)reader["IdEvento"]);
                }
                reader.Close();

                user.EventosActivosUsuario = new List<int>();
                reader = MainCnx.CreateQuery("SELECT IdEvento FROM dbo.MostrarEventosActivos(" + IdUsuario + ") ORDER BY FechaEvento ASC");
                while (reader.Read())
                {
                    user.EventosActivosUsuario.Add((int)reader["IdEvento"]);
                }
                reader.Close();

                return user;
            }
            catch (Exception e) { return null; }
        }

        public List<Usuario> ObtenerUsuario(List<int> IdsUsers, int index)
        {
            List<Usuario> users = new List<Usuario>();
            int cont = 0;

            foreach (int IdUser in IdsUsers)
            {
                if (cont >= index && cont < index + 10)
                {
                    users.Add(ObtenerUsuario(IdUser));
                }
                cont++;
            }

            return users;
        }

        public int IniciarSesion(string user, string pass) 
        {
            try
            {
                string cadena = @"SELECT * FROM dbo.IniciarSesionUsuario (@NombreUsuario, @Contrasena)";
                SqlCommand comando = new SqlCommand(cadena, MainCnx.Db);

                SqlParameter NombreUsuario = new SqlParameter("@NombreUsuario", System.Data.SqlDbType.VarChar, 100);
                NombreUsuario.Value = user;
                SqlParameter Contrasena = new SqlParameter("@Contrasena", System.Data.SqlDbType.VarChar, 50);
                Contrasena.Value = pass;

                comando.Parameters.Add(NombreUsuario);
                comando.Parameters.Add(Contrasena);

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read()) { return (int)reader["IdUsuario"]; }
                else { return 0; }
            }
            catch (Exception e) { return 0; }
        }

        public int Registrarse(string user, string mail, string pass, string date) 
        {
            try
            {
                string cadena = @"EXEC dbo.DarAltaUsuario
                                        @NombreUsuario,
                                        @Correo,
                                        @Contrasena,
                                        @FechaNacimiento";
                SqlCommand comando = new SqlCommand(cadena, MainCnx.Db);

                SqlParameter NombreUsuario = new SqlParameter("@NombreUsuario", System.Data.SqlDbType.VarChar, 100);
                NombreUsuario.Value = user;
                SqlParameter Correo = new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100);
                Correo.Value = mail;
                SqlParameter Contrasena = new SqlParameter("@Contrasena", System.Data.SqlDbType.VarChar, 50);
                Contrasena.Value = pass;
                SqlParameter FechaNacimiento = new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.Date);
                FechaNacimiento.Value = date;

                comando.Parameters.Add(NombreUsuario);
                comando.Parameters.Add(Correo);
                comando.Parameters.Add(Contrasena);
                comando.Parameters.Add(FechaNacimiento);

                comando.ExecuteNonQuery();
                return IniciarSesion(user, pass);
            }
            catch (Exception e) { return 0; }
        }

        public void LikePost(int IdUsuario, int IdPublicacion)
        {
            try
            {
                string cadena = @"EXEC dbo.DarLikePublicacion
                                        @RIdUsuario,
                                        @RIdPublicacion";
                SqlCommand comando = new SqlCommand(cadena, MainCnx.Db);

                SqlParameter RIdUsuario = new SqlParameter("@RIdUsuario", System.Data.SqlDbType.Int);
                RIdUsuario.Value = IdUsuario;
                SqlParameter RIdPublicacion = new SqlParameter("@RIdPublicacion", System.Data.SqlDbType.Int);
                RIdPublicacion.Value = IdPublicacion;

                comando.Parameters.Add(RIdUsuario);
                comando.Parameters.Add(RIdPublicacion);

                comando.ExecuteNonQuery();
            }
            catch (Exception e) { }
        }

        public void AgregarContacto(int IdUsuario, int IdContacto)
        {
            try
            {
                string cadena = "EXEC dbo.AgregarContacto @RIdUsuario, @RIdContacto";
                SqlCommand comando = new SqlCommand(cadena, MainCnx.Db);

                SqlParameter RIdUsuario = new SqlParameter("@RIdUsuario", System.Data.SqlDbType.Int);
                RIdUsuario.Value = IdUsuario;
                SqlParameter RIdContacto = new SqlParameter("@RIdContacto", System.Data.SqlDbType.Int);
                RIdContacto.Value = IdContacto;

                comando.Parameters.Add(RIdUsuario);
                comando.Parameters.Add(RIdContacto);

                comando.ExecuteNonQuery();
            }
            catch (Exception e) { }
        }

        public void ApuntarseEvento(int IdUsuario, int IdEvento)
        {
            try
            {
                string cadena = "INSERT INTO UsuarioEvento VALUES (@RIdUsuario, @RIdEvento)";
                SqlCommand comando = new SqlCommand(cadena, MainCnx.Db);

                SqlParameter RIdUsuario = new SqlParameter("@RIdUsuario", System.Data.SqlDbType.Int);
                RIdUsuario.Value = IdUsuario;
                SqlParameter RIdEvento = new SqlParameter("@RIdEvento", System.Data.SqlDbType.Int);
                RIdEvento.Value = IdEvento;

                comando.Parameters.Add(RIdUsuario);
                comando.Parameters.Add(RIdEvento);

                comando.ExecuteNonQuery();
            }
            catch (Exception e) { }
            
        }

        public List<Usuario> BuscarUsuario(string search, int index)
        {
            try
            {
                AuxCnx = new DbConnect();
                List<Usuario> users = new List<Usuario>();
                int cont = 0;

                SqlDataReader reader = AuxCnx.CreateQuery("SELECT * FROM Usuarios WHERE NombreUsuario LIKE '%" + search + "%' ORDER BY NombreUsuario ASC");
                while (reader.Read())
                {
                    if (cont >= index && cont < index + 10)
                    {
                        users.Add(ObtenerUsuario((int)reader["IdUsuario"]));
                    }
                    cont++;
                }
                reader.Close();

                return users;
            }
            catch (Exception e) { return null; }
        }
    }
}