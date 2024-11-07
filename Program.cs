using System;

namespace GestionDeInventario
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventario inventario = new Inventario();
            Console.WriteLine("Bienvenido al sistema de gestión de inventario.");

            while (true)
            {
                Console.WriteLine("\nSeleccione una opción:");
                Console.WriteLine("1. Agregar productos");
                Console.WriteLine("2. Filtrar y ordenar productos por precio mínimo");
                Console.WriteLine("3. Actualizar precio de un producto");
                Console.WriteLine("4. Eliminar un producto");
                Console.WriteLine("5. Contar productos por rango de precio");
                Console.WriteLine("6. Generar reporte de los productos ingresados");
                Console.WriteLine("7. Salir");

                Console.Write("Opción: ");
                if (!int.TryParse(Console.ReadLine(), out int opcion) || opcion < 1 || opcion > 7)
                {
                    Console.WriteLine("Opción inválida. Intente de nuevo.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        AgregarProductos(inventario);
                        break;
                    case 2:
                        FiltrarYOrdenarProductos(inventario);
                        break;
                    case 3:
                        ActualizarPrecioProducto(inventario);
                        break;
                    case 4:
                        EliminarProducto(inventario);
                        break;
                    case 5:
                        ContarProductosPorRangoDePrecio(inventario);
                        break;
                    case 6:
                        GenerarReporteResumido(inventario);
                        break;
                    case 7:
                        Console.WriteLine("Saliendo del sistema. ¡Hasta luego!");
                        return;
                }
            }
        }

        static void AgregarProductos(Inventario inventario)
        {
            Console.Write("¿Cuántos productos desea ingresar? ");
            if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
            {
                Console.WriteLine("Cantidad inválida. Debe ser un número entero positivo.");
                return;
            }

            for (int i = 0; i < cantidad; i++)
            {

                Console.WriteLine($"\nProducto {i + 1}:");

                string nombre;
                while (true)
                {
                    Console.Write("Nombre: ");
                    nombre = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nombre))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("El nombre no puede estar vacío o ser solo espacios en blanco. Inténtelo de nuevo.");
                    }
                }

                decimal precio;
                while (true)
                {
                    Console.Write("Precio: ");
                    if (decimal.TryParse(Console.ReadLine(), out precio) && precio > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Precio inválido. Debe ser un número positivo.");
                    }
                }

                Producto producto = new Producto(nombre, precio);
                inventario.AgregarProductos(producto);
                Console.WriteLine($"Producto '{nombre}' agregado al inventario.");
            }
        }

        static void FiltrarYOrdenarProductos(Inventario inventario)
        {
            Console.Write("\nIngrese el precio mínimo para filtrar los productos: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal precioMinimo) || precioMinimo < 0)
            {
                Console.WriteLine("Precio mínimo inválido. Debe ser un número positivo.");
                return;
            }

            var productosFiltrados = inventario.FiltrarYOrdenarProductos(precioMinimo);
            Console.WriteLine("\nProductos filtrados y ordenados:");
            foreach (var producto in productosFiltrados)
            {
                producto.MostrarInformacion();
            }
        }

        static void ActualizarPrecioProducto(Inventario inventario)
        {
            string nombreProducto;
            while (true)
            {
                Console.Write("\nIngrese el nombre del producto para actualizar el precio: ");
                nombreProducto = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nombreProducto))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("El nombre no puede estar vacío o ser solo espacios en blanco. Inténtelo de nuevo.");
                }
            }

            decimal nuevoPrecio;
            while (true)
            {
                Console.Write("Ingrese el nuevo precio: ");
                if (decimal.TryParse(Console.ReadLine(), out nuevoPrecio) && nuevoPrecio > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Precio inválido. Debe ser un número positivo.");
                }
            }

            inventario.ActualizarPrecio(nombreProducto, nuevoPrecio);
        }

        static void EliminarProducto(Inventario inventario)
        {
            Console.Write("\nIngrese el nombre del producto a eliminar: ");
            string nombreProducto = Console.ReadLine();
            inventario.EliminarProducto(nombreProducto);
        }

        static void ContarProductosPorRangoDePrecio(Inventario inventario)
        {
            Console.WriteLine("\nConteo de productos por rango de precio:");
            inventario.ContarProductosPorRangoDePrecio();
        }


        static void GenerarReporteResumido(Inventario inventario)
        {
            var productos = inventario.ObtenerProductos().ToList();
            if (productos.Count == 0)
            {
                Console.WriteLine("El inventario está vacío. No hay datos disponibles para generar el reporte.");
                return;
            }

            int totalProductos = productos.Count;
            decimal precioPromedio = productos.Average(p => p.Precio);
            var productoMasCaro = productos.OrderByDescending(p => p.Precio).First();
            var productoMasBarato = productos.OrderBy(p => p.Precio).First();

            Console.WriteLine("\nReporte Resumido del Inventario:");
            Console.WriteLine($"Número total de productos: {totalProductos}");
            Console.WriteLine($"Precio promedio de los productos: {precioPromedio:C}");
            Console.WriteLine($"Producto con el precio más alto: {productoMasCaro.Nombre} - {productoMasCaro.Precio:C}");
            Console.WriteLine($"Producto con el precio más bajo: {productoMasBarato.Nombre} - {productoMasBarato.Precio:C}");
        }
    }
}
