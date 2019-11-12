﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace NullableSampleApp
{
#nullable enable

    public class Startup
    {
        public Startup()
        {
            Services = RegisterServices();
        }

        public IServiceProvider Services { get; }

        public IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();
            // ... register services
            services.AddDbContext<BooksContext>();
            return services.BuildServiceProvider();
        }
    }

#nullable restore
}
