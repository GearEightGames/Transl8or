using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Transl8or.Globals;
using Transl8or.ProjectSystem;

namespace Transl8or
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        ProjectManager manager;

        public Main()
        {
            manager = new ProjectManager();
            InitializeComponent();
        }

        public void CreateProject(ProjectInfo project)
        {
            manager.Create(project);
            SetupUI();
        }

        private void SetupUI()
        {
            tvKeys.DataContext = manager.ProjectInstance.Keys;
            tvKeys.ItemsSource = manager.ProjectInstance.Keys.Keys;
            manager.ProjectInstance.Keys.KeyAdded += () => { tvKeys.ItemsSource = null; tvKeys.ItemsSource = manager.ProjectInstance.Keys.Keys; };
        }

        public void LoadProject(ProjectInfo project)
        {
            manager.Load(project);
            SetupUI();
        }

        private void NewClick(object sender, RoutedEventArgs e)
        {

        }

        private void OpenClick(object sender, RoutedEventArgs e)
        {

        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {

        }

        private void SaveAsClick(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddKey_Click(object sender, RoutedEventArgs e)
        {
            manager.ProjectInstance.AddKey(tbNewKey.Text);
        }

        private void btnFilterReset_Click(object sender, RoutedEventArgs e)
        {
            tbFilter.Text = "";
        }

        private void tbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            manager.SetFilter(tbFilter.Text);
        }
    }
}
