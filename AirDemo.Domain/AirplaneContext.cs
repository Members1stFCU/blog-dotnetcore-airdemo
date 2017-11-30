using Microsoft.EntityFrameworkCore;

namespace AirDemo.Domain
{
    public class AirplaneContext : DbContext
    {
        public AirplaneContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Airplane> Airplanes { get; private set; }
    }
}