using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace Валюты
{
    struct Money
    {
        public int rub;
        public int kop;
        public double usd;
        public double eur;
        public double inr;
        public double czk;
        public double cny;

        public override string ToString()
        {
            string rub = this.rub != 0 ? Convert.ToString(this.rub) : "";
            string kop = this.kop != 0 ? Convert.ToString(this.kop) : "";
            string usd = this.usd != 0 ? Convert.ToString(this.usd) : "";
            string eur = this.eur != 0 ? Convert.ToString(this.eur) : "";
            string inr = this.inr != 0 ? Convert.ToString(this.inr) : "";
            string czk = this.czk != 0 ? Convert.ToString(this.czk) : "";
            string cny = this.cny != 0 ? Convert.ToString(this.cny) : "";

            return rub + ";" + kop + ";" + usd + ";" + eur + ";" + inr + ";" + czk + ";" + cny;
        }
    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double USD = 57.57;
        private const double EUR = 54.4;
        private const double KZT = 0.120773;
        private const double UAH = 1.56;
        private const double TRY = 0.321543;

        private const double INR = 0.706;
        private const double CZK = 2.29;
        private const double CNY = 8.13;

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
                            res = (Convert.ToInt32(tbRUBinp.Text) + Convert.ToDouble(tbKOPinp.Text) / 100) * (1 / USD);
                            break;
                        case "Евро: ":
                            res = (Convert.ToInt32(tbRUBinp.Text) + Convert.ToDouble(tbKOPinp.Text) / 100) * (1 / EUR);
                            break;
                        case "Тенге: ":
                            res = (Convert.ToInt32(tbRUBinp.Text) + Convert.ToDouble(tbKOPinp.Text) / 100) * (1 / KZT);
                            break;
                        case "Гривны: ":
                            res = (Convert.ToInt32(tbRUBinp.Text) + Convert.ToDouble(tbKOPinp.Text) / 100) * (1 / UAH);
                            break;
                        case "Лиры: ":
                            res = (Convert.ToInt32(tbRUBinp.Text) + Convert.ToDouble(tbKOPinp.Text) / 100) * (1 / TRY);
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
                            res = Convert.ToDouble(tbForeignVal.Text) * USD;
                            break;
                        case "Евро: ":
                            res = Convert.ToDouble(tbForeignVal.Text) * EUR;
                            break;
                        case "Тенге: ":
                            res = Convert.ToDouble(tbForeignVal.Text) * KZT;
                            break;
                        case "Гривны: ":
                            res = Convert.ToDouble(tbForeignVal.Text) * UAH;
                            break;
                        case "Лиры: ":
                            res = Convert.ToDouble(tbForeignVal.Text) * TRY;
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
            string path = Environment.CurrentDirectory;
            path = path.Substring(0, path.Length - 9);
            path += "dataMoney.csv";
            getData(path, list);
            inputData("newDataMoney.csv", convertCurrency(list));
            MessageBox.Show("Новый файл находится в папке \"Валюты\\bin\\debug\\\"", "Обработка файла", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        static List<Money> convertCurrency(List<Money> list)
        {
            List<Money> newList = new List<Money>();

            for (int i = 0; i < list.Count; i++)
            {
                int rub = list[i].rub;
                int kop = list[i].kop;
                double usd = list[i].usd;
                double eur = list[i].eur;
                double inr = list[i].inr;
                double czk = list[i].czk;
                double cny = list[i].cny;

                if (rub != 0)
                {
                    usd = rub * (1 / USD);
                    eur = rub * (1 / EUR);
                    inr = rub * (1 / INR);
                    czk = rub * (1 / CZK);
                    cny = rub * (1 / CNY);
                }

                if (kop != 0)
                {
                    usd += kop / 100.0 * (1 / USD);
                    eur += kop / 100.0 * (1 / EUR);
                    inr += kop / 100.0 * (1 / INR);
                    czk += kop / 100.0 * (1 / CZK);
                    cny += kop / 100.0 * (1 / CNY);
                }

                if (usd != 0 && rub == 0 && kop == 0)
                {
                    rub = (int)Math.Floor(usd * USD);
                    double a = Math.Round(usd * USD - Math.Floor(usd * USD), 2) * 100;
                    kop = (int)a;
                }

                if (eur != 0 && rub == 0 && kop == 0)
                {
                    rub = (int)Math.Floor(eur * EUR);
                    double a = Math.Round(eur * EUR - Math.Floor(eur * EUR), 2) * 100;
                    kop = (int)a;
                }

                if (inr != 0 && rub == 0 && kop == 0)
                {
                    rub = (int)Math.Floor(inr * INR);
                    double a = Math.Round(inr * INR - Math.Floor(inr * INR), 2) * 100;
                    kop = (int)a;
                }

                if (czk != 0 && rub == 0 && kop == 0)
                {
                    rub = (int)Math.Floor(czk * CZK);
                    double a = Math.Round(czk * CZK - Math.Floor(czk * CZK), 2) * 100;
                    kop = (int)a;
                }

                if (cny != 0 && rub == 0 && kop == 0)
                {
                    rub = (int)Math.Floor(cny * CNY);
                    double a = Math.Round(cny * CNY - Math.Floor(cny * CNY), 2) * 100;
                    kop= (int)a;
                }

                newList.Add(new Money
                {
                    rub = rub,
                    kop = kop,
                    usd = Math.Round(usd, 2),
                    eur = Math.Round(eur, 2),
                    inr = Math.Round(inr, 2),
                    czk = Math.Round(czk, 2),
                    cny = Math.Round(cny, 2)
                }) ;
            }

            return newList;
        }

        static void getData(string path, List<Money> list)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] array = sr.ReadLine().Split(';');

                        if (array[0] == "" || !int.TryParse(array[0], out int result))
                        {
                            array[0] = null;
                        }
                        if (array[1] == "" || !int.TryParse(array[1], out result))
                        {
                            array[1] = null;
                        }
                        if (array[2] == "" || !double.TryParse(array[2], out double res))
                        {
                            array[2] = null;
                        }
                        if (array[3] == "" || !double.TryParse(array[3], out res))
                        {
                            array[3] = null;
                        }
                        if (array[4] == "" || !double.TryParse(array[4], out res))
                        {
                            array[4] = null;
                        }
                        if (array[5] == "" || !double.TryParse(array[5], out res))
                        {
                            array[5] = null;
                        }
                        if (array[6] == "" || !double.TryParse(array[6], out res))
                        {
                            array[6] = null;
                        }

                        list.Add(new Money()
                        {
                            rub = Convert.ToInt32(array[0]),
                            kop = Convert.ToInt32(array[1]),
                            usd = Convert.ToDouble(array[2]),
                            eur = Convert.ToDouble(array[3]),
                            inr = Convert.ToDouble(array[4]),
                            czk = Convert.ToDouble(array[5]),
                            cny = Convert.ToDouble(array[6])
                        });
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не найден файл. Поместите файл dataMoney.csv в папку \"Валюты\"", "Обработка файла", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        static void inputData(string path, List<Money> list)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Rubles;Kopecks;Dollars;Euro;Rupees;Crowns;Yuan");
                foreach (Money item in list)
                {
                    if (item.ToString() != "0;0;0;0;0;0;0" && item.ToString() != ";;;;;;")
                    {
                        sw.WriteLine(item.ToString());
                    }
                }
            }
        }

    }
}