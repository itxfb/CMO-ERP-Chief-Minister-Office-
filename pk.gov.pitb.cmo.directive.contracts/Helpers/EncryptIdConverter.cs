using System.Formats.Asn1;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace pk.gov.pitb.cmo.contracts
{

    public class EncryptIdConverter : JsonConverter<int>
    {
        private static string Encrypt(int value)
        {
            return Utility.Encrypt(value);
        }



        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (string.IsNullOrEmpty(reader.GetString())) return -1;
            return Utility.Decrypt(reader.GetString());

        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            string val = Encrypt(value);
            writer.WriteStringValue(val);
        }
    }




    

}