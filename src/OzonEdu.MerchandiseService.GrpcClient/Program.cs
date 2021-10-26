using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using OzonEdu.MerchandiseService.Grpc;

namespace OzonEdu.MerchandiseService.GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = 
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            
            using var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
            {
                HttpHandler = httpHandler
            });
            var client = new MerchandiseServiceGrpc.MerchandiseServiceGrpcClient(channel);

            try
            {
                var info = await client.RequestMerchAsync(new AddRequestMerch {ItemName = "item to add"});
                Console.WriteLine(info);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e);
            }
            
            try
            {
                var info = await client.GetRequestMerchInfoAsync(new GetMerchInfoRequest {RequestId = 1});
                Console.WriteLine(info);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}