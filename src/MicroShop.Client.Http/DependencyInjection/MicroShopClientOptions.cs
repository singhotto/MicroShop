namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Configurazione client HTTP.
/// </summary>
public class MicroShopClientOptions {

    /// <summary>
    /// Nome sezione nel file di configurazione "appsettings.json".
    /// </summary>
    public const string SectionName = "MicroShopClient";

    /// <summary>
    /// Base URL di destinazione.
    /// </summary>
    public string BaseAddress { get; set; } = "";

}