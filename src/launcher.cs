using System;
using System.Diagnostics;
using System.IO;

static class Launcher
{
    static void Main()
    {
        string dir = Path.GetDirectoryName(typeof(Launcher).Assembly.Location);
        string bat = Path.Combine(dir, "launcher.bat");
        if (File.Exists(bat))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = bat,
                WorkingDirectory = dir,
                UseShellExecute = false,
                CreateNoWindow = true
            });
        }
    }
}
