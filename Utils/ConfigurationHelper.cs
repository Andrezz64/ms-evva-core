namespace ms_evva_core.Utils;

public static class ConfigurationHelper
{
    public static IConfiguration Configuration { get; private set; }

    public static void Initialize(IConfiguration configuration)
    {
        Configuration = configuration;
    }
}