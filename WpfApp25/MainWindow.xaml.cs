using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp25.Model;

namespace WpfApp25
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class  MainWindow : Window, INotifyPropertyChanged
    {






        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            Globals.dataProvider = new LocalDataProvider();
            PlaneList = Globals.dataProvider.GetPlanes();

            PlaneNameList = Globals.dataProvider.GetPlaneName().ToList();
            PlaneNameList.Insert(0, new PlaneName{ Title = "Все имена" });


        }






        public event PropertyChangedEventHandler PropertyChanged;

        private void Invalidate()
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("PlaneList"));
        }

   

       

            private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public List<PlaneName> PlaneNameList { get; set; }

        private void NameFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedBreed= (NameFilterComboBox.SelectedItem as PlaneName).Title;
            Invalidate();
        }


        private string SearchFilter = "";

        private void SearchFilter_KeyUp(object sender, KeyEventArgs e)
        {
            SearchFilter = SearchFilterTextBox.Text;
            Invalidate();

        }


       
        public string SelectedBreed = "Все имена";







        private IEnumerable<Plane> _PlaneList = null;
        public IEnumerable<Plane> PlaneList
        {
            get
            {
                var res = _PlaneList;
               res= res.Where(c => (SelectedBreed == "Все имена" || c.Name == SelectedBreed));
            if (SearchFilter != "")
                    res = res.Where(c => c.Name.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0);

                return res;

            }

            set
            {
                _PlaneList = value;
            }
        }

       
        


        private void SearchFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchFilter = SearchFilterTextBox.Text;
            Invalidate();
        }

        private bool SortAsc = true;
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SortAsc = (sender as RadioButton).Tag.ToString() == "1";
            Invalidate();
        }
    }

    
}
