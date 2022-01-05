using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.DwhExternalBalances.Settings
{
    public class SettingsModel
    {
        [YamlProperty("DwhExternalBalances.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("DwhExternalBalances.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("DwhExternalBalances.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }
    }
}
