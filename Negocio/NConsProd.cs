using Datos;
using Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NConsProd
    {
        public static void Create(ConsumoProduccion p)
        {
            try
            {
                DConsProd.Create(p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Update(ConsumoProduccion p)
        {
            try
            {
                DConsProd.Update(p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Delete(int id)
        {
            try
            {
                DConsProd.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<ConsumoProduccion> Get()
        {
            try
            {
                return DConsProd.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<A_DtoConsProd> GetDto()
        {
            try
            {
                List<ConsumoProduccion> detalle = DConsProd.GetAll();
                List<A_DtoConsProd> lista = new List<A_DtoConsProd>();
                foreach (var item in detalle)
                {
                    A_DtoConsProd dto = new A_DtoConsProd
                    {
                        Id = item.id,
                        ProductoId = item.produccionProducto.id,
                        Producto = item.produccionProducto.producto.descripcion,
                        InsumoId = item.consumoInsumo.id,
                        Insumo = item.consumoInsumo.insumo.descripcion,
                        Observaciones = item.observaciones
                    };
                    lista.Add(dto);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}