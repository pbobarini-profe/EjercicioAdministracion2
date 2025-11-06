using System;
using System.Collections.Generic;
using Modelos;
using Datos;

namespace Negocio
{
    public class NProductos
    {
        private DProductos datosProductos = new DProductos();


        public string Insertar(Productos producto)
        {
            if (string.IsNullOrEmpty(producto.descripcion))
            {
                return "La descripción no puede estar vacía";
            }

            if (producto.descripcion.Length > 200)
            {
                return "La descripción no puede superar los 200 caracteres";
            }

            try
            {
                bool resultado = datosProductos.Insertar(producto);
                if (resultado)
                {
                    return "Producto insertado correctamente";
                }
                else
                {
                    return "Error al insertar el producto";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public List<Productos> Listar()
        {
            try
            {
                return datosProductos.Listar();
            }
            catch (Exception)
            {
                return new List<Productos>();
            }
        }


        public Productos BuscarPorId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return null;
                }
                return datosProductos.BuscarPorId(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string Actualizar(Productos producto)
        {
            if (producto.id <= 0)
            {
                return "El ID del producto no es válido";
            }

            if (string.IsNullOrEmpty(producto.descripcion))
            {
                return "La descripción no puede estar vacía";
            }

            if (producto.descripcion.Length > 200)
            {
                return "La descripción no puede superar los 200 caracteres";
            }

            try
            {
                bool resultado = datosProductos.Actualizar(producto);
                if (resultado)
                {
                    return "Producto actualizado correctamente";
                }
                else
                {
                    return "Error al actualizar el producto";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string Eliminar(int id)
        {
            if (id <= 0)
            {
                return "El ID del producto no es válido";
            }

            try
            {
                bool resultado = datosProductos.Eliminar(id);
                if (resultado)
                {
                    return "Producto eliminado correctamente";
                }
                else
                {
                    return "Error al eliminar el producto";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}