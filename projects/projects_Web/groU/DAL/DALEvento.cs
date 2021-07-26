using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace groU
{
    public class DALEvento
    {
        DbConnect MainCnx;
        DbConnect AuxCnx;
        Evento evento;

        public DALEvento()
        {
            MainCnx = new DbConnect();
        }

        public Evento ObtenerEvento(int IdEvento)
        {
            try
            {
                SqlDataReader reader = MainCnx.CreateQuery("SELECT * FROM Eventos WHERE " + IdEvento + " = IdEvento");
                evento = new Evento();

                while (reader.Read())
                {
                    evento.IdEvento = (int)reader["IdEvento"];
                    evento.RIdUsuario = (int)reader["RIdUsuario"];
                    evento.NombreEvento = (string)groUtils.GestionarNulos(reader["NombreEvento"]);
                    evento.Imagen = (byte?[])groUtils.GestionarNulos(reader["Imagen"]);
                    evento.Contenido = (string)groUtils.GestionarNulos(reader["Contenido"]);
                    evento.FechaCreacion = (DateTime)reader["FechaCreacion"];
                    evento.FechaEvento = (DateTime)reader["FechaEvento"];
                }
                reader.Close();

                evento.UsuariosEvento = new List<int>();
                reader = MainCnx.CreateQuery("SELECT RIdUsuario FROM UsuarioEvento WHERE " + IdEvento + " = RIdEvento");
                while (reader.Read())
                {
                    evento.UsuariosEvento.Add((int)reader["RIdUsuario"]);
                }
                reader.Close();

                evento.NombreCreador = new DALUsuario().ObtenerUsuario(evento.RIdUsuario).NombreUsuario;

                return evento;
            }
            catch (Exception e) { return null; }
        }

        public List<Evento> ObtenerEvento(List<int> IdsEventos, int index)
        {
            List<Evento> eventos = new List<Evento>();
            int cont = 0;

            foreach (int IdEvento in IdsEventos)
            {
                if (cont >= index && cont < index + 10)
                {
                    eventos.Add(ObtenerEvento(IdEvento));
                }
                cont++;
            }

            return eventos;
        }

        public List<Evento> ObtenerEventosActivos(int index)
        {
            try
            {
                AuxCnx = new DbConnect();
                List<Evento> eventos = new List<Evento>();
                int cont = 0;

                SqlDataReader reader = AuxCnx.CreateQuery("SELECT * FROM EventosActivos() ORDER BY FechaEvento ASC;");
                while (reader.Read())
                {
                    if (cont >= index && cont < index + 10)
                    {
                        eventos.Add(ObtenerEvento((int)reader["IdEvento"]));
                    }
                    cont++;
                }
                reader.Close();

                return eventos;
            }
            catch (Exception e) { return null; }
        }

        public void NuevoEvento(int IdUsuario, string nombreEvento, string contenido, string fechaEvento)
        {
            try
            {
                string newPost = @"EXEC dbo.NuevoEvento
                                        @RIdUsuario,
                                        @NombreEvento,
                                        @Contenido,
                                        @FechaEvento";
                SqlCommand cmd = new SqlCommand(newPost, MainCnx.Db);

                SqlParameter RIdUsuario = new SqlParameter("@RIdUsuario", System.Data.SqlDbType.Int);
                RIdUsuario.Value = IdUsuario;
                SqlParameter NombreEvento = new SqlParameter("@NombreEvento", System.Data.SqlDbType.VarChar, 50);
                NombreEvento.Value = nombreEvento;
                SqlParameter Contenido = new SqlParameter("@Contenido", System.Data.SqlDbType.VarChar, 500);
                Contenido.Value = contenido;
                SqlParameter FechaEvento = new SqlParameter("@FechaEvento", System.Data.SqlDbType.Date);
                FechaEvento.Value = fechaEvento;

                cmd.Parameters.Add(RIdUsuario);
                cmd.Parameters.Add(NombreEvento);
                cmd.Parameters.Add(Contenido);
                cmd.Parameters.Add(FechaEvento);

                cmd.ExecuteNonQuery();
            } catch (Exception e) { }
        }

        public List<Evento> BuscarEventos(string search, int index)
        {
            try
            {
                AuxCnx = new DbConnect();
                List<Evento> eventos = new List<Evento>();
                int cont = 0;

                SqlDataReader reader = AuxCnx.CreateQuery("SELECT * FROM EventosActivos() WHERE NombreEvento LIKE '%" + search + "%' ORDER BY FechaEvento ASC");
                while (reader.Read())
                {
                    if (cont >= index && cont < index + 10)
                    {
                        eventos.Add(ObtenerEvento((int)reader["IdEvento"]));
                    }
                    cont++;
                }
                reader.Close();

                return eventos;
            }
            catch (Exception e) { return null; }
        }
    }
}