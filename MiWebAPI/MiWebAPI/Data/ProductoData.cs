using MiWebAPI.Models;
using System.Data.SqlClient;
using System.Data;
using MiWebAPI.Controllers;

namespace MiWebAPI.Data
{
    public class ProductoData
    {

        private readonly string conexion;

        public ProductoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<ProductoDetalle>> Lista()
        {
            List<ProductoDetalle> lista = new List<ProductoDetalle>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_ProductoDetalle_Listar", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new ProductoDetalle
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            NombreProducto = reader["NombreProducto"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Cantidad = Convert.ToInt32(reader["Id"]),
                            Total = Convert.ToDecimal(reader["Precio"]),
                            Descripcion = reader["Descripcion"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<ProductoDetalle> Obtener(int Id)
        {
            ProductoDetalle objeto = new ProductoDetalle();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_ProductoDetalle_Obtener", con);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new ProductoDetalle
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            NombreProducto = reader["Nombre_Producto"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Cantidad = Convert.ToInt32(reader["Cantidad"]),
                            Total = Convert.ToDecimal(reader["Total"]),
                            Descripcion = reader["Descripcion"].ToString()
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(ProductoDetalle objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_ProductoDetalle_Agregar", con);
                cmd.Parameters.AddWithValue("@nombreProducto", objeto.NombreProducto);
                cmd.Parameters.AddWithValue("@precio", objeto.Precio);
                cmd.Parameters.AddWithValue("@cantidad", objeto.Cantidad);
                cmd.Parameters.AddWithValue("@total", objeto.Total);
                cmd.Parameters.AddWithValue("@descripcion", objeto.Descripcion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Editar(ProductoDetalle objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_ProductoDetalle_Editar", con);
                cmd.Parameters.AddWithValue("@nombreProducto", objeto.NombreProducto);
                cmd.Parameters.AddWithValue("@precio", objeto.Precio);
                cmd.Parameters.AddWithValue("@cantidad", objeto.Cantidad);
                cmd.Parameters.AddWithValue("@total", objeto.Total);
                cmd.Parameters.AddWithValue("@descripcion", objeto.Descripcion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(int Id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_ProductoDetalle_Eliminar", con);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
