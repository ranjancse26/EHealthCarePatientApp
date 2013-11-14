using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using SmartLoginOverlayDemo2.Models;
using SoftArcs.WPFSmartLibrary.MVVMCommands;
using SoftArcs.WPFSmartLibrary.MVVMCore;
using SoftArcs.WPFSmartLibrary.SmartUserControls;
using EHealthCareDataAccess;
using EHealthCareDesktopApp;
using EHealthCareDesktopApp.Properties;
using EHealthCareDesktopApp.Icons;
using System.Windows.Threading;

namespace SmartLoginOverlayDemo2.ViewModels
{
    public class LoginViewModel : ViewModelBase
	{
        public LoginViewModel()
		{
			if (ViewModelHelper.IsInDesignModeStatic == false)
			{
				this.initializeAllCommands();
    		}
		}

		#region Public Properties

		public string UserName
		{
			get { return GetValue( () => UserName ); }
			set
			{
				SetValue( () => UserName, value );
    		}
		}

		public string Password
		{
			get { return GetValue( () => Password ); }
			set { SetValue( () => Password, value ); }
		}

        public string UserImageSource
		{
			get { return GetValue( () => UserImageSource ); }
			set { SetValue( () => UserImageSource, value ); }
		}

		#endregion // Public Properties

		#region Submit Command Handler

		public ICommand SubmitCommand { get; private set; }

		private void ExecuteSubmit(object commandParameter)
		{
			var accessControlSystem = commandParameter as SmartLoginOverlay;

			if (accessControlSystem != null)
			{            
				if (this.validateUser( this.UserName, this.Password ) == true)
				{
					accessControlSystem.Unlock();
				}
				else
				{
					accessControlSystem.ShowWrongCredentialsMessage();
				}
    		}
		}

		private bool CanExecuteSubmit(object commandParameter)
		{
			return !string.IsNullOrEmpty( this.Password );
		}

		#endregion // Submit Command Handler

		#region Private Methods

		private void initializeAllCommands()
		{
			this.SubmitCommand = new ActionCommand( this.ExecuteSubmit, this.CanExecuteSubmit );
		}

		private bool validateUser(string username, string password)
		{
            var patientDataRepository = new PatientDataRepository();
            password = EncryptDecrypt.EncryptData(password);
            var patient = patientDataRepository.GetPatientByUserNamePassword(username, password);
            if (patient != null)
            {
                if (patient.UserName.Equals(username) && patient.Password.Equals(password))
                {
                    EHealthCareDesktopApp.Properties.Settings.Default.PatientID = patient.PatientId;
                    EHealthCareDesktopApp.Properties.Settings.Default.PatientName = patient.FirstName + "  " + patient.LastName;
                    EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier = patient.UniqueIdentifier;
                    EHealthCareDesktopApp.Properties.Settings.Default.Save();
                    return true;
                }
            }

            return false;
		}

		#endregion
	}
}
