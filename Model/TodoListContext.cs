using Microsoft.EntityFrameworkCore;

namespace TodoListAPI.Model
{
    public class TodoListContext : DbContext
    {
        private IConfiguration _configuration;
        public TodoListContext(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TodoListInstance"));
        }
        public virtual DbSet<TodoItem> TodoItems { get; set; }
    }
}
