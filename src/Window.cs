using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;

sealed class Window : System.Windows.Window
{
    const int MB_ICONERROR = 0x00000010;

    [DllImport("Shell32", EntryPoint = "ShellMessageBoxW", CharSet = CharSet.Unicode, ExactSpelling = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    static extern int ShellMessageBox(nint hAppInst = default, nint hWnd = default, string lpcText = default, string lpcTitle = "Error", int fuStyle = MB_ICONERROR);

    internal Window()
    {
        Title = "Bedrock Verifier";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        ResizeMode = ResizeMode.NoResize;
        SizeToContent = SizeToContent.WidthAndHeight;
        SnapsToDevicePixels = UseLayoutRounding = true;

        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            ShellMessageBox(hWnd: new WindowInteropHelper(this).EnsureHandle(), lpcText: $"{e.ExceptionObject}");
            Environment.Exit(default);
        };

        UniformGrid _ = new()
        {
            Width = 400 / 2,
            Height = 300 / 2,
            Columns = 1,
            IsEnabled = false
        };
        Content = _;


        CheckBox checkBox1 = new()
        {
            IsHitTestVisible = false,
            Content = "License",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        CheckBox checkBox2 = new()
        {
            IsHitTestVisible = false,
            Content = "Release",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            IsThreeState = true
        };

        CheckBox checkBox3 = new()
        {
            IsHitTestVisible = false,
            Content = "Preview",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            IsThreeState = true
        };

        _.Children.Add(checkBox1);
        _.Children.Add(checkBox2);
        _.Children.Add(checkBox3);

        ContentRendered += async (_, _) =>
        {
            checkBox1.IsChecked = await Licenses.GetAsync();
            (checkBox2.IsChecked, checkBox3.IsChecked) = Package.Get();
            _.IsEnabled = true;
        };
    }
}