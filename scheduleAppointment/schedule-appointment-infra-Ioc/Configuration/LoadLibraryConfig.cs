using System.Runtime.InteropServices;

namespace schedule_appointment_infra_Ioc.Configuration
{
    public static class LoadLibraryConfig
    {
        public static void Load(string contentRootPath)
        {
            var architectureFolder = IntPtr.Size == 8 ? "64 bit" : "32 bit";

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var wkHtmlToPdfPath = Path.Combine(contentRootPath, $"wkhtmltox/v0.12.4/{architectureFolder}/libwkhtmltox");
                var context = new CustomAssemblyLoadContext();
                context.LoadUnmanagedLibrary(wkHtmlToPdfPath);
            }
        }
    }
}
