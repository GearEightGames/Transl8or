using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Transl8or.Globals;
using Transl8or.ProjectSystem;
using Validation = Transl8or.Globals.Validation;

namespace Transl8or
{
    /// <summary>
    /// Interaction logic for Entry.xaml
    /// </summary>
    public partial class Entry : Window
    {
        private ObservableCollection<ProjectInfo> recent;
        private bool isValidFolder;
        private bool isValidName;

        public ObservableCollection<ProjectInfo> Recent { get { return recent; } }

        public bool IsValid { get { return isValidFolder && isValidName; } }

        public Entry()
        {
            DataContext = this;
            recent = new ObservableCollection<ProjectInfo>();
            InitializeComponent();

            btnCreate.IsEnabled = false;
            LoadRecentProjects();

            System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(ProjectSystem.Languages.Language));
            s.Serialize(System.Xml.XmlWriter.Create("test.xml"), ProjectSystem.Languages.Language.FromCulture(ProjectSystem.Languages.CultureLink.Cultures["de-DE"]));
        }

        private void LoadRecentProjects()
        {
            if (Settings.ProjectsCount == 0)
                lblWarning.Visibility = Visibility.Visible;
            else
            {
                foreach (var item in Settings.Projects.OrderBy(s => s.Creation))
                {
                    recent.Add(item);
                }
            }
        }

        private void tbDirectory_TextChanged(object sender, TextChangedEventArgs e)
        {
            isValidFolder = (Validation.IsValidDirectory(tbDirectory.Text));
            tbDirectory.BorderBrush = isValidFolder ? new SolidColorBrush(Color.FromRgb(179, 179, 179)) : new SolidColorBrush(Color.FromRgb(255, 179, 179));

            if (isValidFolder)
            {
                lblInfo.Content = Validation.IsUnityDirectory(new DirectoryInfo(tbDirectory.Text)) ? Strings.IS_UNITY : string.Empty;
                lblInfo.Content = Validation.ContainsProject(new DirectoryInfo(tbDirectory.Text)) ? Strings.ALREADY_EXISTS : lblInfo.Content;
                isValidFolder = string.IsNullOrWhiteSpace((string)lblInfo.Content);
            }

            btnCreate.IsEnabled = IsValid;
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            isValidName = tbName.Text.Length > 0;
            tbDirectory.BorderBrush = isValidName ? new SolidColorBrush(Color.FromRgb(179, 179, 179)) : new SolidColorBrush(Color.FromRgb(255, 179, 179));

            btnCreate.IsEnabled = IsValid;
        }

        private void btnDirectory_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
            if (fbd.ShowDialog() == true)
                tbDirectory.Text = fbd.SelectedPath;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            ProjectInfo project = new ProjectInfo(tbName.Text, tbDirectory.Text);
            Settings.Projects.Add(project);
            Main mainWindow = new Main();
            mainWindow.CreateProject(project);
            mainWindow.Show();
            Close();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Button origin = sender as Button;
            ProjectInfo info = origin.Tag as ProjectInfo;
            Main mainWindow = new Main();
            mainWindow.LoadProject(info);
            mainWindow.Show();
            Close();
        }
    }
}
