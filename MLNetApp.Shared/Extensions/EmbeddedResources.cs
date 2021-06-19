using System.IO;
using System.Reflection;

namespace MLNetApp.Shared.Extensions
{
    public static class EmbeddedResources
    {
        public static Stream GetEmbeddedResourceStream(string resourceName) => 
            Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

        public static string GetEmbeddedResourceString(string resourceName) =>
            GetEmbeddedResourceStream(resourceName).ReadString();
        
        public static byte[] GetEmbeddedResourceBytes(string resourceName) =>
            GetEmbeddedResourceStream(resourceName).ReadBytes();
    }
}