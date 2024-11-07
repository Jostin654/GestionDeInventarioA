using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeInventario
{
    public class Inventario
    {
        private List<Producto> productos;

        public Inventario()
        {
            productos = new List<Producto>();
        }

        public void AgregarProductos(Producto producto)
        {
            productos.Add(producto);
        }

        public IEnumerable<Producto> FiltrarYOrdenarProductos(decimal precioMinimo) 
        {
            return productos
            .Where(p=> p.Precio>precioMinimo)
            .OrderBy(p=> p.Precio);
        }


        public void ActualizarPrecio(string nombreProducto, decimal nuevoPrecio)
        {
            var productosActualizados = productos
                .Select(p =>
                {
                    if (p.Nombre.Equals(nombreProducto, StringComparison.OrdinalIgnoreCase) && nuevoPrecio > 0)
                    {
                        p.Precio = nuevoPrecio;
                        Console.WriteLine($"Precio actualizado de '{p.Nombre}': {nuevoPrecio:C}");
                    }
                    else if (nuevoPrecio <= 0 && p.Nombre.Equals(nombreProducto, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("El precio debe ser positivo.");
                    }
                    return p;
                })
                .ToList();

            productos = productosActualizados;
        }


        public void EliminarProducto(string nombreProducto)
        {
            var productoAEliminar = productos.FirstOrDefault(p => p.Nombre.Equals(nombreProducto, StringComparison.OrdinalIgnoreCase));
            if (productoAEliminar != null)
            {
                productos.Remove(productoAEliminar);
                Console.WriteLine($"Producto '{nombreProducto}' eliminado del inventario.");
            }
            else
            {
                Console.WriteLine($"Producto '{nombreProducto}' no encontrado.");
            }
        }



    }
}
