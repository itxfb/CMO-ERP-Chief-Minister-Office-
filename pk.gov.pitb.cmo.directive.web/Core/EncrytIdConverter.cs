//using System.Text.Json;
//using HashidsNet;
//using Newtonsoft.Json;
//using JsonSerializer = Newtonsoft.Json.JsonSerializer;

//public class EncryptIdConverter : JsonConverter<int>
//{





//    private static string Encrypt(int value)
//    {
//        Hashids h = new Hashids();
//        return h.Encode(value);


//    }

//    public override void WriteJson(JsonWriter writer, int value, JsonSerializer serializer)
//    {
//        string val = Encrypt(value);
//        writer.WriteValue(val);
//    }

//    public override int ReadJson(JsonReader reader, Type objectType, int existingValue, bool hasExistingValue, JsonSerializer serializer)
//    {
//        throw new NotImplementedException();
//    }
//}