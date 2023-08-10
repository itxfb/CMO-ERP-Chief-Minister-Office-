using System.Reflection;
using HashidsNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public class EncryptIdContractResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        JsonProperty property = base.CreateProperty(member, memberSerialization);

        if (property.PropertyName == "Id")
        {
            property.ValueProvider = new EncryptedValueProvider(property.ValueProvider);
        }

        return property;
    }

    private class EncryptedValueProvider : IValueProvider
    {
        private readonly IValueProvider _innerProvider;

        public EncryptedValueProvider(IValueProvider innerProvider)
        {
            _innerProvider = innerProvider;
        }

        public void SetValue(object target, object value)
        {
            _innerProvider.SetValue(target, value);
        }

        public object GetValue(object target)
        {
            string unencryptedValue = (string)_innerProvider.GetValue(target);
            // Implement your encryption logic here
            string encryptedValue = Encrypt(Convert.ToInt32(unencryptedValue));
            return encryptedValue;
        }
    }

    private static string Encrypt(int value)
    {
       Hashids h = new Hashids();
       return h.Encode(value);

        
    }
}