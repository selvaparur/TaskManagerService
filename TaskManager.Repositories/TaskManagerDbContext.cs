using System.Data.Entity;
using TaskManager.BusinessEntities;

namespace TaskManager.DAO
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext():base("name=TaskManagerDb")
        {
        }

        public DbSet<ParentTask> ParentTasks { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }

    }
}
