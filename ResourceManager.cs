namespace BattleShips
{
    internal class ResourceManager
    {
        private IResourceManagerFactory factory;
        private System.Resources.ResourceManager resourceManager;

        public ResourceManager(string language)
        {
            if (language == "en-EN")
            {
                factory = new EnglishResourceManagerFactory();
            }
            else if (language == "es-ES")
            {
                factory = new SpanishResourceManagerFactory();
            }
            else
            {
                throw new ArgumentException($"Language {language} not supported");
            }

            resourceManager = factory.CreateResourceManager();
        }

        public string GetString(string name)
        {
            return resourceManager.GetString(name);
        }
    }
}
