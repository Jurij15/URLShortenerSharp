var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("URLShortSharpDockerEnv");

builder.AddProject<Projects.URLShortenerSharp>("Main");

builder.Build().Run();
