using System;
using System.Net.Mail;

namespace GestionDeInventario
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventario inventario = new Inventario();
            Console.WriteLine("Bienvenido al sistema de gestión de inventario.");
            Console.WriteLine("¿Cuántos productos desea ingresar?");
            int cantidad = int.Parse(Console.ReadLine());

            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine($"\nProducto {i+1}:");
                Console.WriteLine("Nombre: ");
                string Nombre = Console.ReadLine();

                Console.WriteLine("Precio: ");
                decimal Precio =decimal.Parse(Console.ReadLine());
                Producto producto = new Producto(Nombre, Precio);
                inventario.AgregarProductos(producto);
            }
            Console.WriteLine("\nIngrese el precio mínimo para filtrar los productos:");
            decimal precioMinimo= decimal.Parse(Console.ReadLine());
            var productosFiltrados = inventario.FiltrarYOrdenarProductos(precioMinimo);

            Console.WriteLine("\nProductos filtrados y ordenados:");
            foreach(var producto in productosFiltrados)
            {
                producto.MostrarInformacion();
            }
        }
    }
}