using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;

namespace EHealthCareDesktopApp.Helpers
{
    public class InkCanvasHelper
    {
        public string StrokesToBase64(StrokeCollection inkStrokes)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Save strokes from InkCanvas to a stream as ISF.
                    inkStrokes.Save(ms);
                    // Convert bytes from stream to Base64 text.
                    byte[] isfBytes = ms.ToArray();
                    return Convert.ToBase64String(isfBytes,
                           Base64FormattingOptions.InsertLineBreaks);
                }
            }
            catch
            {
                return "";
            }
        }

        public StrokeCollection Base64ToStrokes(string sBase64)
        {
            try
            {
                StrokeCollection sc = null;
                // Base64 to byte-array.
                byte[] isfBytes = Convert.FromBase64String(sBase64);
                // Save to a stream.
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(isfBytes, 0, isfBytes.Length);
                    ms.Position = 0;     // Wind back.
                    // Get StrokeCollection from stream.
                    sc = new StrokeCollection(ms);
                }
                return sc;
            }
            catch
            {
                return null;
            }
        }
    }
}
