using System;
using System.Collections.Generic;
using Modelos;
using Datos;

namespace Negocio
{
    public class NProduccionProductos
    {
        private DProduccionProductos datosProduccion = new DProduccionProductos();

        
        public string Insertar(ProduccionProductos produccion)
        {
            
            if (produccion.producto == null || produccion.producto.id <= 0)
            {
                return "Debe seleccionar un producto válido";
            }

            if (produccion.cantidad <= 0)
            {
                return "La cantidad debe ser mayor a cero";
            }

            if (produccion.fecha > DateTime.Now)
            {
                return "La fecha no puede ser futura";
            }

            try
            {
                bool resultado = datosProduccion.Insertar(produccion);
                if (resultado)
                {
                    return "Producción registrada correctamente";
                }
                else
                {
                    return "Error al registrar la producción";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        
        public List<ProduccionProductos> Listar()
        {
            try
            {
                return datosProduccion.Listar();
            }
            catch (Exception)
            {
                return new List<ProduccionProductos>();
            }
        }

        
        public ProduccionProductos BuscarPorId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return null;
                }
                return datosProduccion.BuscarPorId(id);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public string Actualizar(ProduccionProductos produccion)
        {

            if (produccion.id <= 0)
            {
                return "El ID de la producción no es válido";
            }

            if (produccion.producto == null || produccion.producto.id <= 0)
            {
                return "Debe seleccionar un producto válido";
            }

            if (produccion.cantidad <= 0)
            {
                return "La cantidad debe ser mayor a cero";
            }

            if (produccion.fecha > DateTime.Now)
            {
                return "La fecha no puede ser futura";
            }

            try
            {
                bool resultado = datosProduccion.Actualizar(produccion);
                if (resultado)
                {
                    return "Producción actualizada correctamente";
                }
                else
                {
                    return "Error al actualizar la producción";
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
                return "El ID de la producción no es válido";
            }

            try
            {
                bool resultado = datosProduccion.Eliminar(id);
                if (resultado)
                {
                    return "Producción eliminada correctamente";
                }
                else
                {
                    return "Error al eliminar la producción";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}