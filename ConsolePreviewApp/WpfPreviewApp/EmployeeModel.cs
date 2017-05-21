namespace WpfPreviewApp
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EmployeeModel : DbContext
    {
        // Your context has been configured to use a 'EmployeeModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WpfPreviewApp.EmployeeModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'EmployeeModel' 
        // connection string in the application configuration file.
        public EmployeeModel()
            : base("name=EmployeeModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Employee> Employees { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}