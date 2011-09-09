using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Ionic.Zip;


namespace TaskDashInstallerClasses
{
    [RunInstaller(true)]
    public partial class MongoDBInstaller : System.Configuration.Install.Installer
    {
        public MongoDBInstaller()
        {
            InitializeComponent();
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);

            ExtractZip();

            InstallService();
        }

        private void InstallService()
        {
            try
            {
                if (!Directory.Exists(LogPath))
                {
                    Directory.CreateDirectory(LogPath);
                }

                Process.Start(ServicePath, "--install --logpath " + LogPath);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " " + ServicePath);
            }
        }

        private void ExtractZip()
        {
            if (Directory.Exists(OutputDirectory))
            {
                MessageBox.Show(
@"An instance of MongoDB has been detected. 
Setup will not overwrite the installation, but the MongoDB service will be initialized.", 
                    "TaskDash MongoDBInstaller");
                return;
            }

            using (ZipFile zip1 = ZipFile.Read(ZipFilePath))
            {
                var selection = (from e in zip1.Entries
                                 where (e.FileName).StartsWith("mongodb-win32")
                                 select e);


                Directory.CreateDirectory(OutputDirectory);

                foreach (var e in selection)
                {
                    e.Extract(OutputDirectory, ExtractExistingFileAction.DoNotOverwrite);
                }
            }
        }

        private string ZipFilePath
        {
            get { return String.Format(ProgramFilesx86 + @"TaskDash\{0}.zip", FileName); }
        }
        static string ProgramFilesx86
        {
            get
            {
                if (8 == IntPtr.Size
                    || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
                {
                    return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
                }

                return Environment.GetEnvironmentVariable("ProgramFiles");
            }
        }

        protected string FileName
        {
            get { return @"mongodb-win32-x86_64-1.8.3"; }
        }

        private string OutputDirectory
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 3)
                + @"mongodb\"; }
        }

        private string VersionPath
        {
            get { return OutputDirectory + FileName + @"\"; }
        }

        private string LogPath
        {
            get{ return VersionPath + @"logs\log"; }
        }

        private string Bin
        {
            get { return VersionPath + @"\bin\"; }
        }

        private string ServicePath
        {
            get { return Bin + "mongod.exe"; }
        }
    }
}

