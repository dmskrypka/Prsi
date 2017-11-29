using System;
using System.Windows.Forms;
using Prsi.Mapping;

namespace Prsi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Logger.InitLogger();
            MappingProfilesConfig.RegisterMapping();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Enter_name());
        }


    }

    
}
