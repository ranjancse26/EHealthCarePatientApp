using System;
using System.IO;
using System.Net;
using System.Windows;                       //STANDARD for WPF App
using System.Windows.Controls;              //STANDARD for WPF App
using System.Windows.Data;                  //STANDARD for WPF App
using System.Windows.Media.Animation;       //STANDARD for WPF App
using System.Windows.Navigation;            //STANDARD for WPF App
using System.Windows.Controls.Primitives;   //STANDARD for WPF App
using System.Windows.Media;                 //For : DrawingGroup
using System.Windows.Shapes;                //For : Geometric shapes like Line
using System.Windows.Input;                 //For : ExecutedRoutedEventArgs
using Microsoft.Win32;                      //For : OpenFileDialog / SaveFileDialog
using System.Windows.Ink;                   //For : InkCanvas
using System.Windows.Markup;                //For : XamlWriter
using System.Windows.Media.Imaging;         //For : BitmapImage etc etc
using System.Windows.Input.StylusPlugIns;
using MahApps.Metro.Controls;
using EHealthCareDesktopApp.Helpers;
using EHealthCareDataAccess;   //For : DrawingAttributes

namespace EHealthCareDesktopApp
{
    public partial class InkPadWindow : MetroWindow
	{
        public int PatientNotesId { get; set; }
        public bool IsUpdated { get; set; }

        public delegate void InkPadAddedDelegate(string message);
        public event InkPadAddedDelegate InkPadAddedEvent;

        // Make the pad 4 inches by 5 inches.
        public static readonly double widthCanvas=8*96;
        public static readonly double heightCanvas=5*96;
        
        public InkPadWindow()
		{
            this.InitializeComponent();

            this.IsUpdated = false;
            this.dtPicker.SelectedDate = DateTime.Now.Date;
            this.radInk.IsChecked = true;
            this.fishButtons.Magnification = 3.5;

            // Draw blue horizontal lines 1/4 inch apart.
            double y = 24;

            while (y < heightCanvas)
            {
                Line line = new Line();
                line.X1 = 0;
                line.Y1 = y;
                line.X2 = widthCanvas;
                line.Y2 = y;
                line.Stroke = Brushes.LightBlue;
                this.inkCanv.Children.Add(line);

                y += 24;
            }
		}

        // New command: just clear all the strokes.
        private void btnNew_Click(object sender, RoutedEventArgs args)
        {
            this.inkCanv.Strokes.Clear();            
        }

        // Save button Click
        private void btnSave_Click(object sender, RoutedEventArgs args)
        {
           var inkCanvasHelper = new InkCanvasHelper();
           var inkCanvasStrokes = inkCanvasHelper.StrokesToBase64(this.inkCanv.Strokes);

           try
           {
               var patientNote = new PatientNote
               {
                   PatientId = EHealthCareDesktopApp.Properties.Settings.Default.PatientID,
                   Subject = txtSubject.Text.Trim(),     
                   Date = dtPicker.SelectedDate.Value,
                   UniqueIdentifier = EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier,
                   Notes = inkCanvasStrokes
               };

               var patientRepository = new PatientNotesRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);

               if (IsUpdated)
               {
                   patientRepository.UpdatePatientNotes(PatientNotesId, patientNote);
                   this.Close();
               }
               else
               {
                   patientRepository.SavePatientNotes(patientNote);
               }

               if (InkPadAddedEvent != null)
                   InkPadAddedEvent("Success");
           }
           catch (Exception ex)
           {
               if (InkPadAddedEvent != null)
                   InkPadAddedEvent(ex.Message);
           }
        }
        
        // Cut command: cut all selected strokes
        private void btnCut_Click(object sender, RoutedEventArgs args)
        {
            if (this.inkCanv.GetSelectedStrokes().Count > 0)
                this.inkCanv.CutSelection();
        }

        // Copy command: copy all selected strokes
        private void btnCopy_Click(object sender, RoutedEventArgs args)
        {
            if (this.inkCanv.GetSelectedStrokes().Count > 0)
                this.inkCanv.CopySelection();
        }

        // Paste command: paste all selected strokes
        private void btnPaste_Click(object sender, RoutedEventArgs args)
        {
            if (this.inkCanv.CanPaste())
                this.inkCanv.Paste();
        }

        // Delete command: delete all selected strokes
        private void btnDelete_Click(object sender, RoutedEventArgs args)
        {
            if (this.inkCanv.GetSelectedStrokes().Count > 0)
            {
                foreach (Stroke strk in this.inkCanv.GetSelectedStrokes())
                    this.inkCanv.Strokes.Remove(strk); 
            }
        }

        // SelectAll command: select all strokes
        private void btnSelectAll_Click(object sender, RoutedEventArgs args)
        {
            this.inkCanv.Select(this.inkCanv.Strokes);
        }

        private void rad_Click(object sender, RoutedEventArgs e)
        {
            RadioButton rad = sender as RadioButton;
            this.inkCanv.EditingMode = (InkCanvasEditingMode)rad.Tag;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
          
        private void penSize_Click(object sender, RoutedEventArgs e)
        {
            RadioButton rad = sender as RadioButton;
            DrawingAttributes inkDA = new DrawingAttributes();
            inkDA.Width = rad.FontSize;
            inkDA.Height = rad.FontSize;
            inkDA.Color = this.inkCanv.DefaultDrawingAttributes.Color;
            inkDA.IsHighlighter = this.inkCanv.DefaultDrawingAttributes.IsHighlighter;
            this.inkCanv.DefaultDrawingAttributes = inkDA;
            this.expB.IsExpanded = false;
        }

        private void btnStylusSettings_Click(object sender, RoutedEventArgs e)
        {
            StylusSettings dlg = new StylusSettings();
            dlg.Owner = this;
            dlg.DrawingAttributes = this.inkCanv.DefaultDrawingAttributes;
            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                this.inkCanv.DefaultDrawingAttributes = dlg.DrawingAttributes;
            }
        }

        private void btnFormat_Click(object sender, RoutedEventArgs e)
        {
            StylusSettings dlg = new StylusSettings();
            dlg.Owner = this;

            // Try getting the DrawingAttributes of the first selected stroke.
            StrokeCollection strokes = this.inkCanv.GetSelectedStrokes();

            if (strokes.Count > 0)
                dlg.DrawingAttributes = strokes[0].DrawingAttributes;
            else
                dlg.DrawingAttributes = this.inkCanv.DefaultDrawingAttributes;

            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                // Set the DrawingAttributes of all the selected strokes.
                foreach (Stroke strk in strokes)
                    strk.DrawingAttributes = dlg.DrawingAttributes;
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (IsUpdated)
            {
                var inkCanvasHelper = new InkCanvasHelper();          
                var patientRepository = new PatientNotesRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
                var patientNote = patientRepository.GetPatientNoteById(EHealthCareDesktopApp.Properties.Settings.Default.PatientID, PatientNotesId);
                if (patientNote != null)
                {
                    txtSubject.Text = patientNote.Subject;
                    dtPicker.SelectedDate = patientNote.Date;
                    this.inkCanv.Strokes.Clear();
                    this.inkCanv.Strokes = inkCanvasHelper.Base64ToStrokes(patientNote.Notes);
                }
            }
        }
    }
}