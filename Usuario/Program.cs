using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using EspacioUsuario;

namespace punto2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //uruario para comunicarme con la api
                HttpClient usuarioHttp = new HttpClient();
                string apiurl = "https://jsonplaceholder.typicode.com/users";
                //pedimos los datos  a la api
                HttpResponseMessage respuesta = await usuarioHttp.GetAsync(apiurl);
                //verificamos que salio todo bien
                respuesta.EnsureSuccessStatusCode();

                JsonSerializerOptions opcionJson = new()
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true
                };

                //leemos el contenido que envio la api
                string Jsoncrudo = await respuesta.Content.ReadAsStringAsync();
                List<Usuarios> usuarios = JsonSerializer.Deserialize<List<Usuarios>>(Jsoncrudo, opcionJson);

                Console.WriteLine("==Usuarios==");

                if (usuarios is null)
                {
                    Console.WriteLine("No es posible leer los usuarios");
                    return;
                }

                for (int i = 0; i < 5; i++)
                {
                    var u = usuarios[i];
                    Console.WriteLine("Nombre: " + u.Name + " Correo Electronico:" + u.Email + " Domicilia: " + u.address.street + u.address.city);
                }

                const string archivoSalida = "Usuarios.json";
                // convertimos los objetos de la lista a tipo json con serialize
                string jsonGuardado = JsonSerializer.Serialize(usuarios, opcionJson);
                //guardamo el archivo json
                await File.WriteAllTextAsync(archivoSalida, jsonGuardado);
                Console.WriteLine($"\nSe guardaron {usuarios.Count} usuarios en '{archivoSalida}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }
    }
}
