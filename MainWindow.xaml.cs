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

namespace Kasino_T12_A
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Spielen_Click(object sender, RoutedEventArgs e)
        {
            Spielen();
        }

        private void TB_Einsatz_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Spielen();
            }
        }

        private void Spielen()
        {
            Random rnd = new Random();
            int einsatz, guthaben, zahl1, zahl2, zahl3, gewinn;
            bool eingebeKorrekt;
            guthaben = Convert.ToInt32(TB_Guthaben.Text);

            eingebeKorrekt = int.TryParse(TB_Einsatz.Text, out einsatz);

            if (!eingebeKorrekt)
            {
                MessageBox.Show("Der Einsatz muss eine ganze Zahl sein!", "Falsche Eingabe");
                TB_Einsatz.Text = "";
                TB_Einsatz.Focus();
            }
            else if (einsatz <= 0)
            {
                MessageBox.Show("Der Einsatz muss größer als Null sein!", "Einsatz=0");
                TB_Einsatz.Text = "";
                TB_Einsatz.Focus();
            }
            else if (einsatz > guthaben)
            {
                MessageBox.Show("Der Einsatz darf nicht größer als das Guthaben sein!", "Zu wenig Guthaben");
                TB_Einsatz.Text = "";
                TB_Einsatz.Focus();
            }
            else
            {
                guthaben -= einsatz;
                TB_Guthaben.Text = guthaben.ToString();
                zahl1 = rnd.Next(1, 8);
                zahl2 = rnd.Next(1, 8);
                zahl3 = rnd.Next(1, 8);
                TB_Zahl1.Text = zahl1.ToString();
                TB_Zahl2.Text = zahl2.ToString();
                TB_Zahl3.Text = zahl3.ToString();

                if (zahl1 == zahl2 && zahl1 == zahl3)
                {
                    gewinn = einsatz * 3;
                    guthaben += gewinn;
                    MessageBox.Show("Sie haben " + gewinn + " gewonnen!", "Hauptgewinn!");
                    TB_Guthaben.Text = guthaben.ToString();
                }
                else if (zahl1 == zahl2 || zahl2 == zahl3 || zahl1 == zahl3)
                {
                    gewinn = einsatz + 3;
                    guthaben += gewinn;
                    MessageBox.Show("Sie haben " + gewinn + " gewonnen!", "Gewonnen!");
                    TB_Guthaben.Text = guthaben.ToString();
                }
                else
                {
                    MessageBox.Show("Leider kein Gewinn!", "Verloren!");
                }

                if (guthaben == 0)
                {
                    //Messagebox vorbereiten
                    string messageBoxText = "Das Spiel ist vorbei. Nochmal?";
                    string caption = "Ende";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxImage icon = MessageBoxImage.Warning;

                    //Messagebox anzeigen und Ergebnis speichern
                    MessageBoxResult result;
                    result = MessageBox.Show(messageBoxText, caption, button, icon);

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            TB_Guthaben.Text = "5";
                            TB_Einsatz.Text = "";
                            break;
                        case MessageBoxResult.No:
                            Application.Current.Shutdown();
                            break;
                    }
                }
            }
        }


    }
}
