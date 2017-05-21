using System.Collections.Generic;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;

namespace WpfPreviewApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Employee> employees;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            dataGrid.SelectedCellsChanged += DataGridOnSelectedCellsChanged;
        }

        private void DataGridOnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs selectedCellsChangedEventArgs)
        {
            if(employees.Count==0) return;
            
            var selectedItem = (Employee)dataGrid.SelectedItem;
            textBox.Text = selectedItem.Id.ToString();
            textBox1.Text = selectedItem.FirstName;
            textBox2.Text = selectedItem.LastName;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new EmployeeModel())
            {
                employees = await db.Employees.ToListAsync();
                dataGrid.ItemsSource = employees;
                if (employees.Count == 0) dataGrid.ItemsSource = new List<Employee>();
                dataGrid.Items.Refresh();
            }
        }

        //add
        private async void button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new EmployeeModel())
            {
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Please fill in corresponding fields");
                    return;
                }
                var employeeToAdd = new Employee { FirstName = textBox1.Text, LastName = textBox2.Text };
                db.Employees.Add(employeeToAdd);
                await db.SaveChangesAsync();

                employees.Add(employeeToAdd);
                dataGrid.Items.Refresh();

                textBox1.Text = textBox2.Text = string.Empty;
            }
        }

        //update
        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new EmployeeModel())
            {
                var selectedEmployee = dataGrid.SelectedItem as Employee;
                if (selectedEmployee == null) return;

                var id = selectedEmployee.Id;
                var employeeToUpdate = await db.Employees.FindAsync(id);
                if (employeeToUpdate == null) return;

                employeeToUpdate.FirstName = selectedEmployee.FirstName;
                employeeToUpdate.LastName = selectedEmployee.LastName;
                db.Entry(employeeToUpdate).State = EntityState.Modified;

                await db.SaveChangesAsync();

                dataGrid.Items.Refresh();
            }
        }

        //delete
        private async void button2_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = dataGrid.SelectedItem as Employee;

            using (var db = new EmployeeModel())
            {
                if (selectedEmployee != null) db.Entry(selectedEmployee).State = EntityState.Deleted;
                await db.SaveChangesAsync();

                employees.Remove(selectedEmployee);
                dataGrid.Items.Refresh();
            }
        }
    }
}
