using Newtonsoft.Json.Schema;
using System.IO;

namespace RestAPISampleAutoTests.Utils
{
    public static class JsonSchemas
    {
        private static readonly string BaseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Schemas");

        public static JSchema UsersJsonSchema()
        {
            return LoadSchemaFromFile("users-schema.json");
        }

        public static JSchema ResourcesJsonSchema()
        {
            return LoadSchemaFromFile("resources-schema.json");
        }

        private static JSchema LoadSchemaFromFile(string fileName)
        {
            string filePath = Path.Combine(BaseDirectory, fileName);
            string jsonSchema = File.ReadAllText(filePath);
            JSchema schema = JSchema.Parse(jsonSchema);
            return schema;
        }
    }
}