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
using Scala.Oefening03.Core;

namespace Scala.Oefening03.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Vloot vloot;
        bool isNieuw;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vloot = new Vloot();
            StartSituatie();
            ClearControls();
            VulListbox();
        }
        private void StartSituatie()
        {
            grpOnzeVloot.IsEnabled = true;
            grpDetails.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
        }
        private void BewerkSituatie()
        {
            grpOnzeVloot.IsEnabled = false;
            grpDetails.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
        }
        private void ClearControls()
        {
            txtMerk.Text = "";
            txtSerie.Text = "";
            txtKleur.Text = "";
            txtKW.Text = "";
            txtNummerplaat.Text = "";
            dtpInDienst.SelectedDate = null;
            SetDefaultColors();
            lblErrors.Visibility = Visibility.Hidden;
        }
        private void SetDefaultColors()
        {
            txtMerk.Background = Brushes.White;
            txtSerie.Background = Brushes.White;
            txtKleur.Background = Brushes.White;
            txtNummerplaat.Background = Brushes.White;
            txtKW.Background = Brushes.White;
            dtpInDienst.Background = Brushes.White;

            txtMerk.Foreground = Brushes.Black;
            txtSerie.Foreground = Brushes.Black;
            txtKleur.Foreground = Brushes.Black;
            txtNummerplaat.Foreground = Brushes.Black;
            txtKW.Foreground = Brushes.Black;
            dtpInDienst.Foreground = Brushes.Black;
        }
        private void VulListbox()
        {
            lstVloot.ItemsSource = null;
            lstVloot.ItemsSource = vloot.Voertuigen;
        }
        private void lstVloot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if (lstVloot.SelectedItem == null)
                return;
            Voertuig voertuig = (Voertuig)lstVloot.SelectedItem;
            txtMerk.Text = voertuig.Merk;
            txtSerie.Text = voertuig.Serie;
            txtKleur.Text = voertuig.Kleur;
            txtKW.Text = voertuig.KW.ToString();
            txtNummerplaat.Text = voertuig.Nummerplaat;
            dtpInDienst.SelectedDate = voertuig.InDienst;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            isNieuw = true;
            ClearControls();
            BewerkSituatie();
            txtMerk.Focus();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstVloot.SelectedItem == null)
                return;

            isNieuw = false;
            BewerkSituatie();
            txtMerk.Focus();
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstVloot.SelectedItem == null)
                return;
            if (MessageBox.Show("Ben je zeker?", "Voertuig wissen", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                Voertuig voertuig = (Voertuig)lstVloot.SelectedItem;
                vloot.RemoveVoertuig(voertuig);
                VulListbox();
                lstVloot.SelectedIndex = -1;
                ClearControls();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultColors();

            string merk;
            string serie;
            string kleur;
            string nrPlaat;
            int kW = 0;
            DateTime inDienst = DateTime.MinValue;

            merk = txtMerk.Text.Trim();
            serie = txtSerie.Text.Trim();
            kleur = txtKleur.Text.Trim();
            nrPlaat = txtNummerplaat.Text.Trim();

            bool fouten = false;
            lblErrors.Content = "";
            if(merk.Length == 0)
            {
                fouten = true;
                lblErrors.Content += "Merknaam is vereist !\n";
                txtMerk.Background = Brushes.LightPink;
                txtMerk.Foreground = Brushes.Red;
            }
            if (serie.Length == 0)
            {
                fouten = true;
                lblErrors.Content += "Serienaam is vereist !\n";
                txtSerie.Background = Brushes.LightPink;
                txtSerie.Foreground = Brushes.Red;
            }
            if (kleur.Length == 0)
            {
                fouten = true;
                lblErrors.Content += "Kleur is vereist !\n";
                txtKleur.Background = Brushes.LightPink;
                txtKleur.Foreground = Brushes.Red;
            }
            if(nrPlaat.Length == 0)
            {
                fouten = true;
                lblErrors.Content += "Nummerplaat is vereist !\n";
                txtNummerplaat.Background = Brushes.LightPink;
                txtNummerplaat.Foreground = Brushes.Red;
            }
            try
            {
                kW = int.Parse(txtKW.Text);
            }
            catch
            {
                fouten = true;
                lblErrors.Content += "Geef een geldige waarde voor KW !\n";
                txtKW.Background = Brushes.LightPink;
                txtKW.Foreground = Brushes.Red;
            }
            if(dtpInDienst.SelectedDate == null)
            {
                fouten = true;
                lblErrors.Content += "Geef een datum in dienst op !\n";
                dtpInDienst.Background = Brushes.LightPink;
                dtpInDienst.Foreground = Brushes.Red;
            }
            else
            {
                inDienst = dtpInDienst.SelectedDate.Value;
            }
            if (dtpInDienst.SelectedDate > DateTime.Today)
            {
                fouten = true;
                lblErrors.Content += "Indienstname kan niet in de toekomst liggen !\n";
                dtpInDienst.Background = Brushes.LightPink;
                dtpInDienst.Foreground = Brushes.Red;
            }
            if(fouten)
            {
                lblErrors.Visibility = Visibility.Visible;
                return;
            }
            Voertuig voertuig;
            if (isNieuw)
            {
                voertuig = new Voertuig(merk, serie, kleur, nrPlaat, kW, inDienst);
                vloot.AddVoertuig(voertuig);
            }
            else
            {
                voertuig = (Voertuig)lstVloot.SelectedItem;
                voertuig.Merk = merk;
                voertuig.Serie = serie;
                voertuig.Kleur = kleur;
                voertuig.Nummerplaat = nrPlaat;
                voertuig.KW = kW;
                voertuig.InDienst = inDienst;
            }
            StartSituatie();
            VulListbox();
            lstVloot.SelectedItem = voertuig;
            lstVloot_SelectionChanged(null, null);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            StartSituatie();
            if(lstVloot.SelectedIndex > -1)
            {
                lstVloot_SelectionChanged(null, null);
            }
        }
    }
}
