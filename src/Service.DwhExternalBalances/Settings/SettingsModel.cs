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
        
        [YamlProperty("DwhExternalBalances.DwhConnectionString")]
        public string DwhConnectionString { get; set; }

        [YamlProperty("DwhExternalBalances.MyNoSqlReaderHostPort")]
        public string MyNoSqlReaderHostPort { get; set; }
        
        [YamlProperty("DwhExternalBalances.ExternalApiGrpcUrl")]
        public string ExternalApiGrpcUrl { get; set; }

        [YamlProperty("DwhExternalBalances.FireblocksApiUrl")]
        public string FireblocksApiUrl  { get; set; }
        
        [YamlProperty("DwhExternalBalances.AssetDictionaryGrpcServiceUrl")]
        public string AssetDictionaryGrpcServiceUrl { get; set; }
    }
}
