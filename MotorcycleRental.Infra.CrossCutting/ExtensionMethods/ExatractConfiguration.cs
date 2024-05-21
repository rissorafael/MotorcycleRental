using Microsoft.Extensions.Configuration;


namespace MotorcycleRental.Infra.CrossCutting.ExtensionMethods
{
    public static class ExatractConfiguration
    {
        static IConfiguration Config;


        public static void Initialize(IConfiguration configuration)
        {
            Config = configuration;
        }

        public static string GetConnectionString
        {
            get
            {
                return GetConnection();
            }
        }

        public static string GetImagemPath
        {
            get
            {
                return ImagemPath();
            }
        }
        public static string GetConnectionLogger
        {
            get
            {
                return GetConnectionLoggerString();
            }
        }
        public static string GetRabbitmqCompose
        {
            get
            {
                return GetRabbitmqComposeString();
            }
        }

        public static string GetVetorCriptografia
        {
            get
            {
                return GetVetorCriptografiaString();
            }
        }

        public static string GetChaveCriptografia
        {
            get
            {
                return GetChaveCriptografiaString();
            }
        }

        public static string GetChaveToken
        {
            get
            {
                return GetChaveTokenString();
            }
        }

        private static string GetConnection()
        {
            var connectionString = Config.GetConnectionString("MyPostgresConnection");
            return connectionString;
        }

        private static string ImagemPath()
        {
            var imagemPath = Config.GetSection("ImagemPath").Value;
            return imagemPath;
        }

        private static string GetConnectionLoggerString()
        {
            var logFilePath = Config.GetSection("ElasticConfiguration:Uri").Value;
            return logFilePath;
        }

        private static string GetRabbitmqComposeString()
        {
            var rabbitMqPath = Config.GetSection("RabbitmqCompose:Host").Value;
            return rabbitMqPath;
        }

        private static string GetVetorCriptografiaString()
        {
            var vetorCriptografiaPath = Config.GetSection("Criptografia:Vetor").Value;
            return vetorCriptografiaPath;
        }

        private static string GetChaveCriptografiaString()
        {
            var chaveCriptografiaPath = Config.GetSection("Criptografia:Chave").Value;
            return chaveCriptografiaPath;
        }

        private static string GetChaveTokenString()
        {
            var chaveTokenPath = Config.GetSection("Token:Chave").Value;
            return chaveTokenPath;
        }
    }
}
