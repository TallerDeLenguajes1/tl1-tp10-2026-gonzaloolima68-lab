using System.Net.Http;
using System.Text.Json;
using TareasV2;
using System;

namespace tp10
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //creo un cliente que se comunicara con la api
            HttpClient clienteHttp = new();
            //guardamos la dreccion de la api en una constante
            const string urlApi = "https://jsonplaceholder.typicode.com/todos/";
            //nombre del archivo en donde se guardara
            const string archivoSalida = "tareas.json";

            //creamos la opciones para trabajar con JSON
            JsonSerializerOptions opcionesJson = new()
            {
                //opcion para ignorar entre mayuscula y minuscula
                PropertyNameCaseInsensitive = true,
                //opcion para guardar el archivo JSON ordenado 
                WriteIndented = true
            };

            Console.WriteLine("=== Ejercicio 1: Tareas (v2 - script) === \n");

            //pedimos todos los datos de la urlapi
            HttpResponseMessage respuesta = await clienteHttp.GetAsync(urlApi);

            //verificamos que salio todo bien
            respuesta.EnsureSuccessStatusCode();

            //leemos el contenido que devolvio la Api
            string jsonCrudo = await respuesta.Content.ReadAsStringAsync();

            //convertimos los texto del json en una lista de objetos con deserialize
            List<Tareas> tareas = JsonSerializer.Deserialize<List<Tareas>>(jsonCrudo, opcionesJson);

            //verificamos si no hubo problemas en agregar objetos a la lista
            if (tareas is null)
            {
                Console.WriteLine("No se pudieron leer las tareas.");
                return;
            }

            Console.WriteLine("--- Tareas PENDIENTES ---");

            foreach(Tareas tarea in tareas)
            {
                //verificamos si la tarea no esta completada
                if (!tarea.Completed)
                {
                    Console.WriteLine($" [{tarea.Id}] {tarea.Title} -> Pendiente");
                }
            }

            Console.WriteLine("\n--- Tareas COMPLETADAS ---");

            foreach(Tareas tarea in tareas)
            {
                //verificamos si la tarea ya esta completada
                if (tarea.Completed)
                {
                    Console.WriteLine($"  [{tarea.Id}] {tarea.Title} -> Completada");
                }
            }
            
            //convertimos los objetos de la lista a tipo json con serialize
            string jsonGuardado = JsonSerializer.Serialize(tareas, opcionesJson);
            //guardamo el archivo json
            await File.WriteAllTextAsync(archivoSalida, jsonGuardado);

            Console.WriteLine($"\nSe guardaron {tareas.Count} tareas en '{archivoSalida}'");
        }
    }
}