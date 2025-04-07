using System;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.ApplicationModel.Store.LicenseManagement;
using Windows.Globalization;

static class Licenses
{
    static readonly string Address = $"https://storesdk.dsx.mp.microsoft.com/v8.0/Sdk/products/contentId?market={new GeographicRegion().CodeTwoLetter}&locale=iv&deviceFamily=Windows.Desktop&productIds=9P5X4QVLC2XR";

    static readonly WebClient Client = new();

    internal static async Task<bool> GetAsync()
    {
        await LicenseManager.RefreshLicensesAsync(LicenseRefreshOption.AllLicenses);

        using var stream = await Client.OpenReadTaskAsync(Address);
        using var reader = JsonReaderWriterFactory.CreateJsonReader(stream, XmlDictionaryReaderQuotas.Max);
        var element = XElement.Load(reader);

        var contentIds = element.Descendants("ContentIds").Select(_ => _.Value);
        var keyIds = element.Descendants("KeyIds").Select(_ => _.Value);
        var result = await LicenseManager.GetSatisfactionInfosAsync(contentIds, keyIds);

        if (result.ExtendedError != default) throw result.ExtendedError;
        return result.LicenseSatisfactionInfos.Any(_ => _.Value.IsSatisfied);
    }
}