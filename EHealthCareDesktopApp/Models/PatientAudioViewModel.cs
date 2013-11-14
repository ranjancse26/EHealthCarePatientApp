using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDesktopApp.Models
{
    public partial class PatientAudioViewModel 
    {
        private long audioId;
        private byte[] audio;
        private System.DateTime dateTime { get; set; }
        private string fileName { get; set; }

        public byte[] Audio
        {
            get { return audio; }
            set
            {
                if (audio != value)
                {
                    audio = value;
                    OnPropertyChanged("Audio");
                }
            }
        }

        public DateTime DateTime
        {
            get { return dateTime; }
            set
            {
                if (dateTime != value)
                {
                    dateTime = value;
                    OnPropertyChanged("DateTime");
                }
            }
        }

        public string FileName
        {
            get { return fileName; }
            set
            {
                if (fileName != value)
                {
                    fileName = value;
                    OnPropertyChanged("FileName");
                }
            }
        }

        public long AudioId
        {
            get { return audioId; }
            set
            {
                if (audioId != value)
                {
                    audioId = value;
                    OnPropertyChanged("AudioId");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
