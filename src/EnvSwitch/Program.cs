using EnvSwitch.Logic;

using Microsoft.Extensions.DependencyInjection;

using var cts = new CancellationTokenSource();

var sc = new ServiceCollection();
sc.AddSingleton<IApplication, Application>();
var provider = sc.BuildServiceProvider();
using var scope = provider.CreateScope();
var app = scope.ServiceProvider.GetRequiredService<IApplication>();
await app.RunAsync(args, cts.Token);
