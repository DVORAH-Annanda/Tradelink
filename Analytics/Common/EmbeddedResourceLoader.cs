using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Analytics.Common
{
    public static class EmbeddedResourceLoader
    {
        public static string ReadText(string resourceFileName)
        {
            Assembly assembly = typeof(EmbeddedResourceLoader).Assembly;

            string resourceName = assembly
                .GetManifestResourceNames()
                .FirstOrDefault(x =>
                    x.EndsWith(
                        resourceFileName,
                        StringComparison.OrdinalIgnoreCase));

            if (resourceName == null)
            {
                throw new FileNotFoundException(
                    "Embedded resource could not be found: " +
                    resourceFileName);
            }

            using (Stream stream =
                   assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException(
                        "Embedded resource stream could not be opened: " +
                        resourceFileName);
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
