using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ACCESO
    {
        private SqlConnection conexion;

        public void Abrir()
        {
            conexion = new SqlConnection();
            //conexion.ConnectionString = "Initial Catalog=ing-soft-santander-vega; Data Source=.; Integrated Security=SSPI"; //UAI
            //conexion.ConnectionString = "Initial Catalog=ing-soft-santander-vega; Data Source=010-345-06-8472\\Navegador; Integrated Security=SSPI"; //UAI2
            conexion.ConnectionString = "Initial Catalog=ing-soft-santander-vega; Data Source=DESKTOP-2QVGDMU\\SQLEXPRESS; Integrated Security=SSPI"; //mi casa
            conexion.Open();
        }
        public void Cerrar()
        {
            conexion.Close();
            conexion = null;
            GC.Collect();
        }

        private SqlCommand CrearComando(string sql, List<SqlParameter> parametros = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conexion;

            if (parametros != null)
            {

                cmd.Parameters.AddRange(parametros.ToArray());
            }

            return cmd;
        }

        public int Escribir(string sql, List<SqlParameter> parameters)
        {
            SqlCommand cmd = CrearComando(sql, parameters);
            
            int filas = 0;
            try
            {
                filas = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                filas = -1;
                Console.WriteLine(ex.Message);
            }
            return filas;
        }

        public int LeerEscalar(string sql, List<SqlParameter> parameters = null)
        {
            SqlCommand cmd = CrearComando(sql, parameters);
            int id = 0;

            id = int.Parse(cmd.ExecuteScalar().ToString());

            return id;
        }

        public DataTable Leer(string sql, List<SqlParameter> parameters = null)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = CrearComando(sql, parameters);
            DataTable tabla = new DataTable();
            try
            {
                adapter.Fill(tabla);
                adapter.Dispose();
            }
            catch (Exception ex)
            {
                tabla = null;
            }
            adapter = null;
            return tabla;
        }

        public SqlDataReader Leer(string sql)
        {
            SqlCommand cmd = CrearComando(sql);
            SqlDataReader lector = null;
            try
            {
                lector = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                lector = null;
            }
            return lector;
        }

        public SqlParameter CrearParametro(string nombre, string valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombre;
            parametro.Value = valor;
            parametro.DbType = DbType.String;
            return parametro;
        }

        public SqlParameter CrearParametro(string nombre, int valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombre;
            parametro.Value = valor;
            parametro.DbType = DbType.Int32;
            return parametro;
        }

        public SqlParameter CrearParametro(string nombre, DateTime valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombre;
            parametro.Value = valor;
            parametro.DbType = DbType.DateTime;
            return parametro;
        }

        public SqlParameter CrearParametro(string nombre, TimeSpan valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombre;
            parametro.Value = valor;
            parametro.DbType = DbType.Time;
            return parametro;
        }

        public int EjecutarProcedimiento(string sql, List<SqlParameter> parameters = null, int commandTimeout = 30)
        {
            SqlCommand cmd = CrearComando(sql, parameters);
            cmd.CommandTimeout = commandTimeout;

            return cmd.ExecuteNonQuery();
        }
    }
}