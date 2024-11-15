namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Configurazione client HTTP.
/// </summary>
public class SupplyClientOptions {

    /// <summary>
    /// Nome sezione nel file di configurazione "appsettings.json".
    /// </summary>
    public const string SectionName = "SupplyClient";

    /// <summary>
    /// Base URL di destinazione.
    /// </summary>
    public string BaseAddress { get; set; } = "";

}