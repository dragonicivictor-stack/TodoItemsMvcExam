using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoItems.Models;

namespace ToDoItems.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoItems.Models.ToDoListEntry> ToDoListEntry { get; set; } = default!;
    }
}
