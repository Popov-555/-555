1.Вставить разметку
2.Создать папку модель
3.Создать класс (Допустим у меня Самолеты Plane)
4 Пишем 
public class Plane
    {
        public string Name { get; set; }
        public string Price  { get; set; }
        public string Fuel { get; set; }


    }
}
5.Создать  интерфейс IDataProvider и класс LocalDataProvider, реализующий этот интерфейс
6.Это в IDataProvider
{
    interface IDataProvider
    {
        IEnumerable<Plane> GetPlanes();
    }
}
7.LocalDataProvider
public class LocalDataProvider : IDataProvider
    {

        public IEnumerable<Plane> GetPlanes()

        {
            return new Plane[] {
                new Plane{Name="Airbus A320neo",Price="1900",Fuel="700" },
                new Plane { Name = "Boeing 777X", Price = "3000", Fuel = "900" },
                new Plane { Name = "Boeing 737 MAX", Price = "2000", Fuel = "1100" }
            };
        }
    }
}
8.В простарнстве имен проекта создадим класс Globals,
{
    class Globals
    {
        public static IDataProvider dataProvider;
    }
}
9.MainWindow.xaml.cs
  public partial class MainWindow : Window
    {
        public IEnumerable<Plane> PlaneList { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            Globals.dataProvider = new LocalDataProvider();
            PlaneList = Globals.dataProvider.GetPlanes();

        }
       
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



    }

}
10.Теперь, имея данные для отображения, мы можем разместить в разметке элемент DataGrid и "привязать" к нему данные:
 <DataGrid
    Grid.Row="1"
    Grid.Column="1"
    CanUserAddRows="False"
    AutoGenerateColumns="False"
    ItemsSource="{Binding PlaneList}" >
            <DataGrid.Columns>
                <DataGridTextColumn
            Header="Имя"
            Binding="{Binding Name}"/>
                <DataGridTextColumn
            Header="Цена"
            Binding="{Binding Price }"/>
                <DataGridTextColumn
            Header="Топливо"
            Binding="{Binding Fuel}"/>
               
               
            </DataGrid.Columns>

        </DataGrid>
        если светиться ошибка НЕСОГЛАСОВАНОСТЬ .то добавить в классе public
1ГОТОВО
11.Создаем фильтрацию по словарю 1 создаем класс PlaneName.cs(нужно написать свой) и если нет добавить    public class MedDiagnoz
    {
        public string Title { get; set; }
    }
12.Создаем в классе главного окна свойство для хранения справочника 
   public List<PlaneName> PlaneNameList { get; set; }
13.В интерфейс поставщика данных (IDataProvider) добавляем метод для получения списка

    interface IDataProvider
    {
       IEnumerable<PlaneName> GetPlaneName();}
14.Реализуем этот метод в LocalDataProvider
 public IEnumerable<PlaneName> GetPlaneName()
        {
            return new PlaneName[] {
        new PlaneName{ Title="Airbus A320neo" },
        new PlaneName{ Title="Boeing 777X" },
        new PlaneName{ Title="Boeing 737 MAX" },
    };
        }
15.Добавляем код в главное поле кодов
PlaneNameList = Globals.dataProvider.GetPlaneName().ToList();
            PlaneNameList.Insert(0, new PlaneName{ Title = "Все имена" });

16,Добавил в (во WrapPanel):
 <Label 
    Content="Имя:"
    VerticalAlignment="Center"/>

            <ComboBox
     Name="NameFilterComboBox"
    SelectionChanged="NameFilterComboBox_SelectionChanged"
    VerticalAlignment="Center"
    MinWidth="100"
    SelectedIndex="0"
    ItemsSource="{Binding PlaneNameList}">

                <ComboBox.ItemTemplate>
                    <DataTemplate>
                        <Label 
                Content="{Binding Title}"/>
                    </DataTemplate>
                </ComboBox.ItemTemplate>
            </ComboBox>
        </WrapPanel>
17.Добавляем интерфейс окну

public partial class MainWindow : Window, INotifyPropertyChanged
Реализуем интерфейс

public event PropertyChangedEventHandler PropertyChanged;
(либо написав либо нажав релизовать интерфейс)

private void Invalidate()
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("PlaneList"));
        }


18.Добавляем  
 private void NameFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedBreed= (NameFilterComboBox.SelectedItem as PlaneName).Title;
            Invalidate();
        }
19 ГОТОВЫЙ КОД с реализацией интерфейса


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




        public string SelectedBreed = "Все имена";

        private IEnumerable<Plane> _PlaneList = null;
        public IEnumerable<Plane> PlaneList
        {
            get
            {
                return _PlaneList
                    .Where(c => (SelectedBreed == "Все имена" || c.Name == SelectedBreed));
            }
            set
            {
                _PlaneList = value;
            }
        }
    }

    20Поиск,Сортировка В разметке окна (в элемент WrapPanel) добавляем элемент для ввода теста - TextBox



  
 <Label 
  Content="искать" 
  VerticalAlignment="Center"/>
          <TextBox
  Width="100"
  VerticalAlignment="Center"
  x:Name="SearchFilterTextBox" 
  KeyUp="SearchFilter_KeyUp" TextChanged="SearchFilterTextBox_TextChanged"/>
21
В коде окна создаем переменную для хранения строки поиска и запоминаем её в обработчике ввода текста (SearchFilter_KeyUp)



       private string SearchFilter = "";

      private void SearchFilter_KeyUp(object sender, KeyEventArgs e)
      {
          SearchFilter = SearchFilterTextBox.Text;
          Invalidate();

      }
Отредактироал гет 

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
сортировка

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
![](./Без названия.png)
