using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningASPdotNet.Data
{
    public class DataContext : DbContext
    {
        // Cho phép truy vấn và lưu CSDL
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Character> Characters => Set<Character>();
    }
}