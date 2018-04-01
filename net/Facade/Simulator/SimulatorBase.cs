using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmartHome.Facade.Simulator
{
    public abstract class SimulatorBase
    {
        public T Deserialize<T>(string fileName, string username)
        {
            string path = GetFullJsonPath(fileName, username);
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                JsonSerializer serializer = new JsonSerializer();
                using (var sw = new StreamReader((stream)))
                {
                    JsonReader reader = new JsonTextReader(sw);

                    return serializer.Deserialize<T>(reader);

                }
            }
        }

        private string GetFullJsonPath(string fileName, string userName)
        {
            var attribute = GetType().GetCustomAttribute<GiveMeNameAttribute>();
            if (attribute == null)
            {
                throw new ArgumentNullException($"{nameof(GiveMeNameAttribute)} not applied on {GetType().Name} class!");
            }

            string applicationFullPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            string applicationDirectory = Path.GetDirectoryName(Path.GetFullPath(applicationFullPath));
            string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(applicationDirectory, "../../../../TestData/Simulator"));

            path = Path.Combine(path, attribute.Directory, userName, fileName);

            return path;
        }
    }
}
