using Orleans.Runtime;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
//mayb add tablo and blob client here
builder.UseOrleans();

var app = builder.Build();

app.MapDefaultEndpoints();

//app.Run();

app.MapGet("/", () => "OK");

await app.RunAsync();

//public sealed class CounterGrain(
//    [PersistentState("count")] IPersistentState<int> count) : ICounterGrain
//{
//    public ValueTask<int> Get()
//    {
//        return ValueTask.FromResult(count.State);
//    }

//    public async ValueTask<int> Increment()
//    {
//        var result = ++count.State;
//        await count.WriteStateAsync();
//        return result;
//    }
//}