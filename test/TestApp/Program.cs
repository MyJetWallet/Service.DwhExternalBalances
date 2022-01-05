using System;
using System.Text.Json;
using System.Threading.Tasks;
using ProtoBuf.Grpc.Client;
using Service.DwhExternalBalances.Client;
using Service.DwhExternalBalances.Grpc.Models;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;

            Console.Write("Press enter to start");
            Console.ReadLine();


            var factory = new DwhExternalBalancesClientFactory("http://localhost:80");
            var client = factory.GetDwhExternalBalancesService();

            var resp = await  client.GetAllBalancesAsync();
            Console.WriteLine(JsonSerializer.Serialize(resp));

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
