public static class DbConfig
{
    public static string GetConnectionString()
    {
        string GetEnv(string name)
        {
            var value = Environment.GetEnvironmentVariable(name);
            if (string.IsNullOrEmpty(value))
                throw new Exception($"Missing required environment variable: {name}");
            return value;
        }

        var server = GetEnv("DB_SERVER");
        var port = GetEnv("DB_PORT");
        var database = GetEnv("DB_DATABASE");
        var user = GetEnv("DB_USER");
        var password = GetEnv("DB_PASSWORD");

        return $"server={server};port={port};database={database};user={user};password={password};";
    }
}
