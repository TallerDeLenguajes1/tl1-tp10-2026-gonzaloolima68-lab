using System.Net.Http;
using System.Text.Json;
using TareasV2;
using System;

namespace tp9
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient clienteHttp = new();
            const string urlApi = "https://jsonplaceholder.typicode.com/todos/";
            const string archivoSalida = "tareas.json";

            JsonSerializerOptions opcionesJson = new()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

            Console.WriteLine("=== Ejercicio 1: Tareas (v2 - script) === \n");

            HttpResponseMessage respuesta = await clienteHttp.GetAsync(urlApi);
            respuesta.EnsureSuccessStatusCode();

            string jsonCrudo = await respuesta.Content.ReadAsStringAsync();

            List<Tareas> tareas = JsonSerializer.Deserialize<List<Tareas>>(jsonCrudo, opcionesJson);

            if (tareas is null)
            {
                Console.WriteLine("No se pudieron leer las tareas.");
                return;
            }

            Console.WriteLine("--- Tareas PENDIENTES ---");

            foreach(Tareas tarea in tareas)
            {
                if (!tarea.Completed)
                {
                    Console.WriteLine($" [{tarea.Id}] {tarea.Title} -> Pendiente");
                }
            }

            Console.WriteLine("\n--- Tareas COMPLETADAS ---");

            foreach(Tareas tarea in tareas)
            {
                if (tarea.Completed)
                {
                    Console.WriteLine($"  [{tarea.Id}] {tarea.Title} -> Completada");
                }
            }

            string jsonGuardado = JsonSerializer.Serialize(tareas, opcionesJson);
            await File.WriteAllTextAsync(archivoSalida, jsonGuardado);

            Console.WriteLine($"\nSe guardaron {tareas.Count} tareas en '{archivoSalida}'");
        }
    }
}