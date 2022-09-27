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

namespace Валюты
{
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

        private static string currency;

        private void cbLand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbland.SelectedIndex == 0)
            {
                spForeignland.Visibility = Visibility.Collapsed;
                spForeignOut.Visibility = Visibility.Collapsed;
                spHomeLand.Visibility = Visibility.Visible;
                spHomeOut.Visibility = Visibility.Collapsed;
            }
            else
            {
                spHomeLand.Visibility = Visibility.Collapsed;
                spHomeOut.Visibility = Visibility.Collapsed;
                spForeignland.Visibility = Visibility.Visible;
                spForeignOut.Visibility = Visibility.Collapsed;
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            double res = 0;

            if (cbland.SelectedIndex == 0)
            {
                tbHomeOut.Text = currency;
                switch (tbHomeOut.Text)
                {
                    case "Доллары: ":
                        res = (Convert.ToDouble(tbRUBinp) + Convert.ToDouble(tbKOPinp)) * 0.017;
                        break;
                    case "Евро: ":
                        res = (Convert.ToDouble(tbRUBinp) + Convert.ToDouble(tbKOPinp)) * 0.018;
                        break;
                    case "Тенге: ":
                        res = (Convert.ToDouble(tbRUBinp) + Convert.ToDouble(tbKOPinp)) * 8.33;
                        break;
                    case "Гривны: ":
                        res = (Convert.ToDouble(tbRUBinp) + Convert.ToDouble(tbKOPinp)) * 0.63;
                        break;
                    case "Лиры: ":
                        res = (Convert.ToDouble(tbRUBinp) + Convert.ToDouble(tbKOPinp)) * 0.32;
                        break;
                    default:
                        break;
                }
                tbHomeOut.Text += Convert.ToString(res);
                spHomeOut.Visibility = Visibility.Visible;
            }
            else
            {
                tbRubOut.Text = "Рубли: ";
                tbKopOut.Text = "Копейки: ";
                switch (tbForeignInp.Text)
                {
                    case "Доллары":
                        res = Convert.ToDouble(tbForeignVal.Text) * 58.31;
                        break;
                    case "Евро":
                        res = Convert.ToDouble(tbForeignVal.Text) * 56.18;
                        break;
                    case "Тенге":
                        res = Convert.ToDouble(tbForeignVal.Text) * 0.12;
                        break;
                    case "Гривны":
                        res = Convert.ToDouble(tbForeignVal.Text) * 1.58;
                        break;
                    case "Лиры":
                        res = Convert.ToDouble(tbForeignVal.Text) * 3.15;
                        break;
                    default:
                        break;
                }
                tbRubOut.Text += Convert.ToString(Math.Floor(res));
                tbKopOut.Text += Convert.ToString(Math.Round(res - Math.Floor(res), 2) * 100);
                spForeignOut.Visibility = Visibility.Visible;
            }
        }

        private void rbUSD_Click(object sender, RoutedEventArgs e)
        {
            tbForeignInp.Text = rbUSD.Content.ToString();
            spForeignOut.Visibility = Visibility.Collapsed;
        }

        private void rbEUR_Click(object sender, RoutedEventArgs e)
        {
            tbForeignInp.Text = rbEUR.Content.ToString();
            spForeignOut.Visibility = Visibility.Collapsed;
        }

        private void rbKZT_Click(object sender, RoutedEventArgs e)
        {
            tbForeignInp.Text = rbKZT.Content.ToString();
            spForeignOut.Visibility = Visibility.Collapsed;
        }

        private void rbUAH_Click(object sender, RoutedEventArgs e)
        {
            tbForeignInp.Text = rbUAH.Content.ToString();
            spForeignOut.Visibility = Visibility.Collapsed;
        }

        private void rbTRY_Click(object sender, RoutedEventArgs e)
        {
            tbForeignInp.Text = rbTRY.Content.ToString();
            spForeignOut.Visibility = Visibility.Collapsed;
        }

        private void rbUSDout_Click(object sender, RoutedEventArgs e)
        {
            currency = rbTRYout.Content.ToString() + ": ";
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
    }
}