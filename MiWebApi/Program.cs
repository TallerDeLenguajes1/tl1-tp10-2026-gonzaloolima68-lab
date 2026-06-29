using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Miweb;
using Microsoft.VisualBasic;


namespace punto3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                string urlApi = "https://jsonplaceholder.typicode.com/posts";

                HttpClient dato = new HttpClient();
                //pedimos los datos  a la api
                HttpResponseMessage respuesta = await dato.GetAsync(urlApi);
                //verificamos que salio todo bien
                respuesta.EnsureSuccessStatusCode();

                string jsoncrudo = await respuesta.Content.ReadAsStringAsync();
                List<Info> Infoclase = JsonSerializer.Deserialize<List<Info>>(jsoncrudo);

                if (Infoclase is null)
                {
                    Console.WriteLine("no se pudo enlistar las informacion");
                    return;
                }

                Console.WriteLine("Informacion de los primeros 5");
                for (int i = 0; i < 5; i++)
                {
                    var inf = Infoclase[i];
                    Console.WriteLine("id: " + inf.Id);
                    Console.WriteLine("Id usuario: " + inf.UserId);
                    Console.WriteLine("Titulo: " + inf.Title);
                    Console.WriteLine("Cuerpo: " + inf.Body);
                }
                const string archivoSalida = "info.json";

                string jsonGuardado = JsonSerializer.Serialize(Infoclase);

                await File.WriteAllTextAsync(archivoSalida, jsonGuardado);
            }
            catch (Exception ex)
            {

                Console.WriteLine("ERRO " + ex.Message);
            }
        }
    }
}


