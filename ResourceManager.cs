namespace BattleShips
{
    internal class ResourceManager
    {
        private readonly IResourceManagerFactory factory;
        private readonly System.Resources.ResourceManager resourceManager;

        public ResourceManager(string? language)
        {
            factory = language switch
            {
                "en-EN" => new EnglishResourceManagerFactory(),
                "es-ES" => new SpanishResourceManagerFactory(),
                _ => throw new ArgumentException($"Language {language} not supported")
            };

            resourceManager = factory.CreateResourceManager();
        }

        public string? GetString(string name)
        {
            return resourceManager.GetString(name);
        }
    }
}
