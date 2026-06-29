using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace punto2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            //uruario para comunicarme con la api
            HttpClient usuarioHttp=new HttpClient();
            string apiurl="https://jsonplaceholder.typicode.com/users";
            //pedimos los datos  a la api
            HttpResponseMessage respuesta= await usuarioHttp.GetAsync(apiurl);

            respuesta.EnsureSuccessStatusCode();



        }
    }
}
