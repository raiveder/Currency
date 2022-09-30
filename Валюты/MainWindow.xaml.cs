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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Валюты
{
    struct Money
    {
        public string rub;
        public string kop;
        public string usd;
        public string eur;
        public string nir;
        public string isk;
        public string cny;

        public override string ToString()
        {
            return rub + ";" + kop + ";" + usd + ";" + eur + ";" + nir + ";" + isk + ";" + cny;
        }
    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            spForeignland.Visibility = Visibility.Collapsed;
            spForeignOut.Visibility = Visibility.Collapsed;
            spHomeOut.Visibility = Visibility.Collapsed;
            cbland.SelectedIndex = 0;
        }

        private string currency;

        private void cbLand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currency = null;
            if (cbland.SelectedIndex == 0)
            {
                spForeignland.Visibility = Visibility.Collapsed;
                spForeignOut.Visibility = Visibility.Collapsed;
                spHomeland.Visibility = Visibility.Visible;
                spHomeOut.Visibility = Visibility.Collapsed;
                tbRUBinp.Text = null;
                tbKOPinp.Text = null;

                rbUSDout.IsChecked = false;
                rbEURout.IsChecked = false;
                rbKZTout.IsChecked = false;
                rbUAHout.IsChecked = false;
                rbTRYout.IsChecked = false;

                spChoiceHomeland.Visibility = Visibility.Collapsed;
            }
            else
            {
                spHomeland.Visibility = Visibility.Collapsed;
                spHomeOut.Visibility = Visibility.Collapsed;
                spForeignland.Visibility = Visibility.Visible;
                spForeignOut.Visibility = Visibility.Collapsed;
                tbForeignVal.Text = null;
                spForeignInp.Visibility = Visibility.Collapsed;

                rbUSD.IsChecked = false;
                rbEUR.IsChecked = false;
                rbKZT.IsChecked = false;
                rbUAH.IsChecked = false;
                rbTRY.IsChecked = false;
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            double res = -1;

            if (cbland.SelectedIndex == 0)
            {
                if (tbRUBinp.Text == "" || tbKOPinp.Text == "")
                {
                    spHomeOut.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Введите количество рублей и копеек", "Ввод значений", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                tbHomeOut.Text = currency;

                try
                {
                    if (Convert.ToInt32(tbRUBinp.Text) < 0)
                    {
                        spHomeOut.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Количество рублей не может быть меньше 0", "Ввод значений", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    if (Convert.ToInt32(tbKOPinp.Text) < 0 || Convert.ToInt32(tbKOPinp.Text) > 99)
                    {
                        spHomeOut.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Введите количество копеек от 0 до 99", "Ввод значений", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    if (Convert.ToInt32(tbRUBinp.Text) == 0 && Convert.ToInt32(tbKOPinp.Text) == 0)
                    {
                        spHomeOut.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Количество рублей и копеек не может быть равно 0", "Ввод значений", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    switch (tbHomeOut.Text)
                    {
                        case "Доллары: ":
                            res = (Convert.ToInt32(tbRUBinp.Text) + Convert.ToDouble(tbKOPinp.Text) / 100) * 0.017;
                            break;
                        case "Евро: ":
                            res = (Convert.ToInt32(tbRUBinp.Text) + Convert.ToDouble(tbKOPinp.Text) / 100) * 0.018;
                            break;                                       
                        case "Тенге: ":                                 
                            res = (Convert.ToInt32(tbRUBinp.Text) + Convert.ToDouble(tbKOPinp.Text) / 100) * 8.33;
                            break;                                        
                        case "Гривны: ":                                  
                            res = (Convert.ToInt32(tbRUBinp.Text) + Convert.ToDouble(tbKOPinp.Text) / 100) * 0.63;
                            break;                                       
                        case "Лиры: ":                                    
                            res = (Convert.ToInt32(tbRUBinp.Text) + Convert.ToDouble(tbKOPinp.Text) / 100) * 0.32;
                            break;
                        default:
                            MessageBox.Show("Выберите валюту, в которую необходимо конвертировать рубли", "Выбор валюты", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Введите количество рублей и копеек корректно", "Ввод значений", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                if (res != -1)
                {
                    tbHomeOut.Text += Convert.ToString(Math.Round(res, 2));
                    spHomeOut.Visibility = Visibility.Visible;
                }
            }
            else
            {
                tbRubOut.Text = "Рубли: ";
                tbKopOut.Text = "Копейки: ";
                try
                {
                    if (Convert.ToDouble(tbForeignVal.Text) <= 0)
                    {
                        spHomeOut.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Количество валюты не может быть меньше 0", "Ввод значений", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    switch (tbForeignInp.Text)
                    {

                        case "Доллары: ":
                            res = Convert.ToDouble(tbForeignVal.Text) * 58.31;
                            break;
                        case "Евро: ":
                            res = Convert.ToDouble(tbForeignVal.Text) * 56.18;
                            break;          
                        case "Тенге: ":     
                            res = Convert.ToDouble(tbForeignVal.Text) * 0.12;
                            break;         
                        case "Гривны: ":   
                            res = Convert.ToDouble(tbForeignVal.Text) * 1.58;
                            break;          
                        case "Лиры: ":      
                            res = Convert.ToDouble(tbForeignVal.Text) * 3.15;
                            break;
                        default:
                            MessageBox.Show("Выберите валюту, из которой необходимо конвертировать", "Выбор валюты", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Введите количество конвертируемой валюты корректно", "Ввод значений", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                if (res != -1)
                {
                    tbRubOut.Text += Convert.ToString(Math.Floor(res));
                    tbKopOut.Text += Convert.ToString(Math.Round(res - Math.Floor(res), 2) * 100);
                    spForeignOut.Visibility = Visibility.Visible;
                }
            }
        }

        private void rbUSD_Click(object sender, RoutedEventArgs e)
        {
            tbForeignInp.Text = rbUSD.Content.ToString() + ": ";
            spForeignOut.Visibility = Visibility.Collapsed;
            spForeignInp.Visibility = Visibility.Visible;
        }

        private void rbEUR_Click(object sender, RoutedEventArgs e)
        {
            tbForeignInp.Text = rbEUR.Content.ToString() + ": ";
            spForeignOut.Visibility = Visibility.Collapsed;
            spForeignInp.Visibility = Visibility.Visible;
        }

        private void rbKZT_Click(object sender, RoutedEventArgs e)
        {
            tbForeignInp.Text = rbKZT.Content.ToString() + ": ";
            spForeignOut.Visibility = Visibility.Collapsed;
            spForeignInp.Visibility = Visibility.Visible;
        }

        private void rbUAH_Click(object sender, RoutedEventArgs e)
        {
            tbForeignInp.Text = rbUAH.Content.ToString() + ": ";
            spForeignOut.Visibility = Visibility.Collapsed;
            spForeignInp.Visibility = Visibility.Visible;
        }

        private void rbTRY_Click(object sender, RoutedEventArgs e)
        {
            tbForeignInp.Text = rbTRY.Content.ToString() + ": ";
            spForeignOut.Visibility = Visibility.Collapsed;
            spForeignInp.Visibility = Visibility.Visible;
        }

        private void rbUSDout_Click(object sender, RoutedEventArgs e)
        {
            currency = rbUSDout.Content.ToString() + ": ";
            spHomeOut.Visibility = Visibility.Collapsed;
        }

        private void rbEURout_Click(object sender, RoutedEventArgs e)
        {
            currency = rbEURout.Content.ToString() + ": ";
            spHomeOut.Visibility = Visibility.Collapsed;
        }

        private void rbKZTout_Click(object sender, RoutedEventArgs e)
        {
            currency = rbKZTout.Content.ToString() + ": ";
            spHomeOut.Visibility = Visibility.Collapsed;
        }

        private void rbUAHout_Click(object sender, RoutedEventArgs e)
        {
            currency = rbUAHout.Content.ToString() + ": ";
            spHomeOut.Visibility = Visibility.Collapsed;
        }

        private void rbTRYout_Click(object sender, RoutedEventArgs e)
        {
            currency = rbTRYout.Content.ToString() + ": ";
            spHomeOut.Visibility = Visibility.Collapsed;
        }

        private void tbKOPinp_TextChanged(object sender, TextChangedEventArgs e)
        {
            spChoiceHomeland.Visibility = Visibility.Visible;
        }

        private void btnFile_Click(object sender, RoutedEventArgs e)
        {
            List<Money> list = new List<Money>();
            getData("dataMoney.csv", list);

        }

        static List<Money> createList(List<Money> list)
        {
            List<Money> newList = new List<Money>();

            for (int i = 0; i < list.Count; i++)
            {
                string rub = list[i].rub;
                string kop = list[i].kop;
                string usd = list[i].usd;
                string eur = list[i].eur;
                string nir = list[i].nir;
                string isk = list[i].isk;
                string cny = list[i].cny;

                //if (rub || !int.TryParse(rub, out int result))
                //{
                //    rub = null;
                //}

                //newList.Add(new Money
                //{

                //});
            }

            return newList;
        }

        static void getData(string path, List<Money> list)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] array = sr.ReadLine().Split(';');

                    string rub = array[0];
                    string kop = array[1];
                    string usd = array[2];
                    string eur = array[3];
                    string nir = array[4];
                    string isk = array[5];
                    string cny = array[6];

                    if (rub == "" || !int.TryParse(rub, out int result))
                    {
                        rub = null;
                    }
                    if (kop == "" || !int.TryParse(kop, out result))
                    {
                        kop = null;
                    }
                    if (usd == "" || !int.TryParse(usd, out result))
                    {
                        usd = null;
                    }
                    if (eur == "" || !int.TryParse(eur, out result))
                    {
                        eur = null;
                    }
                    if (nir == "" || !int.TryParse(nir, out result))
                    {
                        nir = null;
                    }
                    if (isk == "" || !int.TryParse(isk, out result))
                    {
                        isk = null;
                    }
                    if (cny == "" || !int.TryParse(cny, out result))
                    {
                        cny = null;
                    }

                    list.Add(new Money()
                    {
                        rub = rub,
                        kop = kop,
                        usd = usd,
                        eur = eur,
                        nir = nir,
                        isk = isk,
                        cny = cny
                    });
                }
            }
        }

        static void inputData(string path, List<Money> list)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (Money item in list)
                {
                    sw.WriteLine(item.ToString());
                }
            }
        }

    }
}