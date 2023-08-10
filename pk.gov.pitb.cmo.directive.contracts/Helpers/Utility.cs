
using HashidsNet;

namespace pk.gov.pitb.cmo.contracts
{
    public class Utility
    {
        public static string Encrypt(int id)
        {
            var h = new Hashids("pk.pitb.cmo");
            return h.Encode(id);
        }
        public static int Decrypt(string id)
        {
            var h = new Hashids("pk.pitb.cmo");
            return h.Decode(id)[0];
        }
    }
}
