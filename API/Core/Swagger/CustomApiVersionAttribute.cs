namespace API.Core.Swagger
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class CustomApiVersionAttribute : Attribute
    {
        public CustomApiVersionAttribute(string version)
        {
            Version = version;
        }

        public string Version { get; }
    }
}
