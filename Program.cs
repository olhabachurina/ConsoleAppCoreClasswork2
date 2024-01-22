namespace ConsoleAppCoreClasswork2
{
    namespace ConsoleAppCoreClasswork1
    {
        using Microsoft.EntityFrameworkCore;
        using Microsoft.EntityFrameworkCore.Diagnostics;
        using System.Collections.Generic;
        using System.Diagnostics;
        using System.Linq;
        class Program
        {
            static void Main()
            {
                DisplayDepartments();
                AddDepartments();
                DisplayDepartments();
            }
            static void DisplayDepartments()
            {
                using (var context = new ApplicationContext())
                {
                    
                    var allDepartments = context.Departments.ToList();

                   
                    Console.WriteLine("Departments:");
                    foreach (var department in allDepartments)
                    {
                        Console.WriteLine($"{department.DepartmentId}\t{department.DepartmentName}");
                    }
                    Console.WriteLine();
                }
            }

            static void AddDepartments()
            {
                using (var context = new ApplicationContext())
                {
                    context.Departments.AddRange(
                        new Department { DepartmentName = "IT" },
                        new Department { DepartmentName = "Marketing" },
                        new Department { DepartmentName = "Finance" }
                    );

                    context.SaveChanges();
                }
            }
        }
       
        public class ApplicationContext : DbContext
            {

            public DbSet<Department> Departments { get; set; }
            public ApplicationContext()
                {
                    Database.EnsureDeleted();
                    Database.EnsureCreated();
                }
                protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                {
                    optionsBuilder.UseSqlServer(@"Server=DESKTOP-4PCU5RA\SQLEXPRESS;Database=Hobby;Integrated Security=True;TrustServerCertificate=True;");
                    optionsBuilder.LogTo(e => Debug.WriteLine(e), new[] { RelationalEventId.CommandExecuted });
                }
            }
        }
    }
public class Department
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
}


