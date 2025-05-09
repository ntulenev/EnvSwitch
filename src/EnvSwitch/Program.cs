using Abstractions;

using EnvSwitch.Utility;

using Infrastructure;
using Infrastructure.Configuration;

using Logic;
using Logic.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var cts = new CancellationTokenSource();

var builder = Host.CreateDefaultBuilder()
  .ConfigureServices((hostContext, services) =>
  {
      _ = services.Configure<ProfilesConfiguration>(
          hostContext.Configuration.GetSection(nameof(ProfilesConfiguration)));
      _ = services.Configure<WorkstationConfiguration>(
    hostContext.Configuration.GetSection(nameof(WorkstationConfiguration)));
      _ = services.AddSingleton<IApplication, Application>();
      _ = services.AddSingleton<IEnvManager, EnvManager>();
      _ = services.AddSingleton<IOutputProcessor, OutputProcessor>();
      _ = services.AddSingleton<IProfileManager, ProfileManager>();
      _ = services.AddSingleton<IWorkstationManager, WorkstationManager>();
      _ = services.AddSingleton<IEnvironmentProvider, SystemEnvironmentProvider>();
  });
var host = builder.Build();
using var scope = host.Services.CreateScope();
var app = scope.ServiceProvider.GetRequiredService<IApplication>();
await app.RunAsync(args, cts.Token);





