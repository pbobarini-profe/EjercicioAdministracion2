using System;
using System.Collections.Generic;
using Datos;
using Modelos;

namespace Negocio
{
    public class NProductos
    {
        private DProductos dproducto;

        public NProductos()
        {
            dproducto = new DProductos();
        }

        
        private string ValidarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                return "El nombre del producto es obligatorio.";

            if (producto.Nombre.Length > 100)
                return "El nombre no puede superar los 100 caracteres.";

            if (producto.PrecioUnitario <= 0)
                return "El precio unitario debe ser mayor a 0.";

            if (producto.Stock < 0)
                return "El stock no puede ser negativo.";

            if (producto.StockMinimo < 0)
                return "El stock mínimo no puede ser negativo.";

            return null; 
        }


        public (bool exito, string mensaje) Insertar(Producto producto)
        {
            try
            {
                
                string error = ValidarProducto(producto);
                if (error != null)
                    return (false, error);

                
                producto.FechaCreacion = DateTime.Now;

                
                bool resultado = DProducto.Insertar(producto);

                if (resultado)
                    return (true, "Producto insertado correctamente.");
                else
                    return (false, "No se pudo insertar el producto.");
            }
            catch (Exception ex)
            {
                return (false, "Error al insertar producto: " + ex.Message);
            }
        }

        
        public (bool exito, List<Producto> productos, string mensaje) ObtenerTodos()
        {
            try
            {
                List<Producto> productos = productosDatos.ObtenerTodos();
                return (true, productos, "Productos obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                return (false, null, "Error al obtener productos: " + ex.Message);
            }
        }

        
        public (bool exito, Producto producto, string mensaje) ObtenerPorId(int id)
        {
            try
            {
                if (id <= 0)
                    return (false, null, "ID inválido.");

                Producto producto = DProductos.ObtenerPorId(id);

                if (producto != null)
                    return (true, producto, "Producto encontrado.");
                else
                    return (false, null, "Producto no encontrado.");
            }
            catch (Exception ex)
            {
                return (false, null, "Error al obtener producto: " + ex.Message);
            }
        }

        
        public (bool exito, string mensaje) Actualizar(Producto producto)
        {
            try
            {
                
                string error = ValidarProducto(producto);
                if (error != null)
                    return (false, error);

                if (producto.IdProducto <= 0)
                    return (false, "ID de producto inválido.");

                
                bool resultado = productosDatos.Actualizar(producto);

                if (resultado)
                    return (true, "Producto actualizado correctamente.");
                else
                    return (false, "No se pudo actualizar el producto.");
            }
            catch (Exception ex)
            {
                return (false, "Error al actualizar producto: " + ex.Message);
            }
        }

        
        public (bool exito, string mensaje) Eliminar(int id)
        {
            try
            {
                if (id <= 0)
                    return (false, "ID inválido.");

                
                var resultadoProducto = ObtenerPorId(id);
                if (!resultadoProducto.exito)
                    return (false, "El producto no existe.");

                
                bool resultado = productosDatos.Eliminar(id);

                if (resultado)
                    return (true, "Producto eliminado correctamente.");
                else
                    return (false, "No se pudo eliminar el producto.");
            }
            catch (Exception ex)
            {
                
                if (ex.Message.Contains("REFERENCE") || ex.Message.Contains("FK"))
                    return (false, "No se puede eliminar el producto porque tiene producciones asociadas.");

                return (false, "Error al eliminar producto: " + ex.Message);
            }
        }

        
        public (bool exito, List<Producto> productos, string mensaje) BuscarPorNombre(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    return ObtenerTodos();

                List<Producto> productos = productosDatos.BuscarPorNombre(nombre);
                return (true, productos, "Búsqueda completada.");
            }
            catch (Exception ex)
            {
                return (false, null, "Error al buscar productos: " + ex.Message);
            }
        }

        
        public (bool exito, List<Producto> productos, string mensaje) ObtenerActivos()
        {
            try
            {
                var resultado = ObtenerTodos();
                if (resultado.exito)
                {
                    var productosActivos = resultado.productos.FindAll(p => p.Activo);
                    return (true, productosActivos, "Productos activos obtenidos.");
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return (false, null, "Error al obtener productos activos: " + ex.Message);
            }
        }

        
        public (bool exito, List<Producto> productos, string mensaje) ObtenerProductosConBajoStock()
        {
            try
            {
                var resultado = ObtenerTodos();
                if (resultado.exito)
                {
                    var productosBajoStock = resultado.productos.FindAll(p => p.NecesitaReposicion());
                    return (true, productosBajoStock, $"Se encontraron {productosBajoStock.Count} productos con bajo stock.");
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return (false, null, "Error al obtener productos con bajo stock: " + ex.Message);
            }
        }
    }
}
