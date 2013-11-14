using EHealthCareDataAccess;
using EHealthCareDesktopApp.Models;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using NAudio.Wave;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace EHealthCareDesktopApp
{
    /// <summary>
    /// Interaction logic for UploadAudio.xaml
    /// </summary>
    public partial class UploadAudio : MetroWindow
    {
        private int selectedPatientAudioIndex;
        private bool isPlayButtonEnabled = false;
        private bool isUploadButtonEnabled = false;
        private WaveFormat inputWaveFormat;
        private ObservableCollection<PatientAudioViewModel> patientAudioFilesCollection;
        private int appointmentID;

        public UploadAudio(int appointmentID)
        {
            InitializeComponent();
            this.appointmentID = appointmentID;
        }

        private void RecordButtonClick(object sender, RoutedEventArgs e)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\VoiceRecorder\\VoiceRecorder.exe";
            Process.Start(path);
        }

        private void PlayButtonClick(object sender, RoutedEventArgs e)
        {
            Play();
        }

        private void UploadButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var byteArray = File.ReadAllBytes(InputFile);
                var appointmentDataRepository = new AppointmentDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
                    appointmentDataRepository.Upload(new AudioAppointment{
                        AppointmentId = this.appointmentID,
                        DateTime = DateTime.Now,
                        PatientId = EHealthCareDesktopApp.Properties.Settings.Default.PatientID,
                        FileName = InputFile.Substring(InputFile.LastIndexOf("\\") + 1, (InputFile.Length - InputFile.LastIndexOf("\\") - 1)),
                        Audio = byteArray,
               });

               LoadAudioFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BrowseFolderButtonClick(object sender, RoutedEventArgs e)
        {
            SelectInputFile();
        }

        public string InputFile
        {
            get;
            set;
        }

        public string InputFormat
        {
            get;
            set;
        }

        public ObservableCollection<PatientAudioViewModel> PatientAudioFilesCollection
        {
            get { return patientAudioFilesCollection; }
            set
            {
                if (patientAudioFilesCollection != value)
                {
                    patientAudioFilesCollection = value;
                    OnPropertyChanged("PatientAudioFilesCollection");
                }
            }
        }

        public int SelectedPatientAudioIndex
        {
            get
            {
                return selectedPatientAudioIndex;
            }
            set
            {
                if (selectedPatientAudioIndex != value)
                {
                    selectedPatientAudioIndex = value;
                    OnPropertyChanged("SelectedIndex");
                }
            }
        }

        public bool IsPlayButtonEnabled
        {
            get { return isPlayButtonEnabled; }
            set
            {
                if (isPlayButtonEnabled != value)
                {
                    isPlayButtonEnabled = value;
                    OnPropertyChanged("IsPlayButtonEnabled");
                }
            }
        }

        public bool IsUploadButtonEnabled
        {
            get { return isUploadButtonEnabled; }
            set
            {
                if (isUploadButtonEnabled != value)
                {
                    isUploadButtonEnabled = value;
                    OnPropertyChanged("IsUploadButtonEnabled");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SelectInputFile()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Audio files|*.wav;";
            if (ofd.ShowDialog() == true)
            {
                if (TryOpenInputFile(ofd.FileName))
                    InputFile = ofd.FileName;
            }
        }
        
        private bool TryOpenInputFile(string file)
        {
            bool isValid = false;
            try
            {
                using (var reader = new MediaFoundationReader(file))
                {
                    InputFormat = reader.WaveFormat.ToString();
                    inputWaveFormat = reader.WaveFormat;
                }
                isValid = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Not a supported input file ({0})", e.Message));
            }
            return isValid;
        }
        
        private void Play()
        {
            if (SelectedPatientAudioIndex > -1)
            {
                var byteArray = PatientAudioFilesCollection[SelectedPatientAudioIndex].Audio;
                var stream = new MemoryStream(byteArray);
                var soundPlayer = new SoundPlayer(stream);
                soundPlayer.Play();
            }
        }

        private void LoadAudioFiles()
        {
            try
            {
                PatientAudioFilesCollection.Clear();
                var appointmentDataRepository = new AppointmentDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
                var patientAudioAppointments = appointmentDataRepository.GetAudioAppointments(EHealthCareDesktopApp.Properties.Settings.Default.PatientID);
                foreach (var audioAppointment in patientAudioAppointments)
                {
                    PatientAudioFilesCollection.Add(new PatientAudioViewModel
                    {
                        DateTime = audioAppointment.DateTime,
                        FileName = audioAppointment.FileName,
                        AudioId = audioAppointment.AudioId,
                        Audio = audioAppointment.Audio                       
                    });
                }
                this.listUploadedAudioFiles.ItemsSource = PatientAudioFilesCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Problem in loading audio files: {0}", ex.Message));
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            PatientAudioFilesCollection = new ObservableCollection<PatientAudioViewModel>();
            LoadAudioFiles();
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            var appointmentDataRepository = new AppointmentDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
            try
            { 
                var id = int.Parse(((Button)sender).CommandParameter.ToString());
                appointmentDataRepository.DeleteAudioAppointment(EHealthCareDesktopApp.Properties.Settings.Default.PatientID, id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Problem in deleting audio file: {0}", ex.Message));
            }

            LoadAudioFiles();
        }   
    }
}
