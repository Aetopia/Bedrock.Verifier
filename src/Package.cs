using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Management.Deployment;

static class Package
{
    static readonly PackageManager Manager = new();

    const string Release = "Microsoft.MinecraftUWP_8wekyb3d8bbwe";

    const string Preview = "Microsoft.MinecraftWindowsBeta_8wekyb3d8bbwe";

    internal static (bool? Release, bool? Preview) Get() => (Get(Release), Get(Preview));

    static bool? Get(string value)
    {
        var package = Manager.FindPackagesForUser(string.Empty, value).FirstOrDefault();
        return package is null ? null : package.SignatureKind is PackageSignatureKind.Store;
    }
}