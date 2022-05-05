using Sample.TodoApp;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => 
        webBuilder.UseStartup<Startup>())
    .Build()
    .Run();