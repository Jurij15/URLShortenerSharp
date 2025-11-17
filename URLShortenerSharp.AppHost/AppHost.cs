var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.URLShortenerSharp>("Main");

builder.Build().Run();
