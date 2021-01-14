using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
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
using MySql.Data.MySqlClient;

namespace Kontaktverwaltung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Person> kontakte = new List<Person>();

        public MainWindow()
        {
            InitializeComponent();
            readData();
            personenlisteNamenHinzufuegen();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try{
                string selectedName = cmbKontaktnamen.SelectedItem.ToString();
                foreach (Person person in kontakte)
                {
                    if (person.getId() == selectedName)
                    {
                        txtVorname.Text = person.getVorname();
                        txtNachname.Text = person.getNachname();
                        txtTelefonnummer.Text = person.getTelefonnummer();
                        txtAdresse.Text = person.getAdresse();
                        txtEmail.Text = person.getEmail();
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cmbKontaktnamen.SelectedIndex = -1;
            btnHinzufuegen.Visibility = Visibility.Visible;
            textboxenLeeren();
            textboxenReadOnlyHandle(false);
        }

        private void BtnHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=kontaktverwaltung;";
            string query = $"INSERT INTO kontakte(vorname, nachname, telefonnummer, adresse, email) VALUES('{txtVorname.Text}','{txtNachname.Text}','{txtTelefonnummer.Text}','{txtAdresse.Text}','{txtEmail.Text}')";
                
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            kontakte.Add(new Person(txtVorname.Text, txtNachname.Text, txtTelefonnummer.Text, txtAdresse.Text, txtEmail.Text));
            personenlisteNamenHinzufuegen();
            textboxenReadOnlyHandle(true);
            btnHinzufuegen.Visibility = Visibility.Hidden;
            textboxenLeeren();
        }

        private void personenlisteNamenHinzufuegen()
        {
            cmbKontaktnamen.Items.Clear();
            foreach (Person person in kontakte)
            {
                cmbKontaktnamen.Items.Add(person.getId());
            }
        }

        private void textboxenReadOnlyHandle(bool readOnly)
        {
            txtVorname.IsReadOnly = readOnly;
            txtNachname.IsReadOnly = readOnly;
            txtTelefonnummer.IsReadOnly = readOnly;
            txtAdresse.IsReadOnly = readOnly;
            txtEmail.IsReadOnly = readOnly;
        }

        private void textboxenLeeren()
        {
            txtVorname.Clear();
            txtNachname.Clear();
            txtTelefonnummer.Clear();
            txtAdresse.Clear();
            txtEmail.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=kontaktverwaltung;";

            try
            {
                int i = 0;
                string selectedName = cmbKontaktnamen.SelectedItem.ToString();
                string vorname = selectedName.Split(' ')[0];
                string nachname = selectedName.Split(' ')[1];
                string query = $"DELETE FROM `kontakte` WHERE vorname = '{vorname}' AND nachname = '{nachname}'";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();


                databaseConnection.Close();

                foreach (Person person in kontakte)
                {
                    if (person.getId() == selectedName)
                    {
                        kontakte.RemoveAt(i);
                        personenlisteNamenHinzufuegen();
                        textboxenLeeren();
                        return;
                    }
                    i++;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void readData()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=kontaktverwaltung;";
            string query = "SELECT * FROM kontakte";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        kontakte.Add(new Person(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5)));
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
