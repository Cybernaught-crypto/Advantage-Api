using Microsoft.EntityFrameworkCore;

namespace Advantage.Api.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbcontextOptions<ApiContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Server> Servers { get; set; }
    }
}
    }
}
}
}