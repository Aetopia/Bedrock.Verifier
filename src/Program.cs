using System;
using System.Globalization;
using System.Threading;
using System.Windows;

static class Program
{
    [STAThread]
    static void Main()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        using (new Mutex(false, "C997F923-EB2E-4CEF-AB11-DA7BF19CD107", out var value)) if (value) new Application().Run(new Window());
    }
}