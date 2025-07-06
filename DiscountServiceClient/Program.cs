using DiscountServiceGrpc;
using Grpc.Net.Client;

var channel = GrpcChannel.ForAddress("https://localhost:50541");
var client = new Discount.DiscountClient(channel);

var watch = System.Diagnostics.Stopwatch.StartNew();

Console.WriteLine($"Start {DateTime.Now:mm:ss}");

var call1 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call2 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call3 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call4 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call5 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call6 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call7 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call8 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call9 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call10 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call11 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call12 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call13 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call14 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call15 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call16 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call17 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call18 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call19 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });
var call20 = client.GenerateAsync(new GenerateRequest { Count = 2000, Length = 8 });


await Task.WhenAll(
    call1.ResponseAsync,
    call2.ResponseAsync,
    call3.ResponseAsync,
    call4.ResponseAsync,
    call5.ResponseAsync,
    call6.ResponseAsync,
    call7.ResponseAsync,
    call8.ResponseAsync,
    call9.ResponseAsync,
    call10.ResponseAsync,
    call11.ResponseAsync,
    call12.ResponseAsync,
    call13.ResponseAsync,
    call14.ResponseAsync,
    call15.ResponseAsync,
    call16.ResponseAsync,
    call17.ResponseAsync,
    call18.ResponseAsync,
    call19.ResponseAsync,
    call20.ResponseAsync
);

watch.Stop();
Console.WriteLine($"end of process {watch.ElapsedMilliseconds}");

//var useResponse = await client.UseCodeAsync(new UseCodeRequest { Code = "ABC12345" });

Console.ReadLine();
