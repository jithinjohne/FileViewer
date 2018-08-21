using Leadtools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileViewer.LeadToolsHelpers
{
    public class License
    {
        public static void Set()
        {
            string licenseFilePath = "LeadToolsHelpers\\eval-license-files.lic"; //Your .LIC file path here
            string developerKey = System.IO.File.ReadAllText("LeadToolsHelpers\\eval-license-files.lic.key"); //Your .KEY file path here

            RasterSupport.SetLicense(licenseFilePath, developerKey);

            //Tests to see if license supports a particular type
            bool isLocked = RasterSupport.IsLocked(RasterSupportType.Document);

            if (isLocked)
                Trace.WriteLine("Document support is locked");
            else
                Trace.WriteLine("Document support is unlocked");
        }
    }
}
