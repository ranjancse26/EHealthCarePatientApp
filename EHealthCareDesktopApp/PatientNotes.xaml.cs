using MahApps.Metro.Controls;
using EHealthCareDataAccess;
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

namespace EHealthCareDesktopApp
{
    /// <summary>
    /// Interaction logic for PatientNotes.xaml
    /// </summary>
    public partial class PatientNotes : MetroWindow
    {
        public PatientNotes()
        {
            InitializeComponent();
        }

        private void AddNotes(object sender, RoutedEventArgs e)
        {
            var inkCanvasPad = new InkPadWindow();
            inkCanvasPad.InkPadAddedEvent += inkCanvasPad_InkPadAddedEvent;
            inkCanvasPad.ShowDialog();
        }

        void inkCanvasPad_InkPadAddedEvent(string message)
        {
            if (message.Equals("Success"))
            {
                LoadPatientNotes();
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void LoadPatientNotes()
        {
            var patientNotesRepository = new PatientNotesRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
            listViewNotes.ItemsSource = patientNotesRepository.GetAllNotes(EHealthCareDesktopApp.Properties.Settings.Default.PatientID);
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            var inkCanvasPad = new InkPadWindow();
            inkCanvasPad.IsUpdated = true;
            inkCanvasPad.PatientNotesId = int.Parse(((Button)sender).CommandParameter.ToString());
            inkCanvasPad.ShowDialog();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadPatientNotes();
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            int patientNoteId = int.Parse(((Button)sender).CommandParameter.ToString());
            var patientNotesRepository = new PatientNotesRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
            patientNotesRepository.DeletePatientNotes(EHealthCareDesktopApp.Properties.Settings.Default.PatientID, patientNoteId);
            LoadPatientNotes();
        }
    }
}
