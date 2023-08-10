using System.Security.Cryptography;
using System.Text;

namespace Shared;

public static class Encryptor
{
    private static readonly string key = "78214125442A462D4A614E645267556B58703273357638792F423F4528482B4B";
    private static readonly string iv = "33743677397A24432646294A404E6351";

    public static async Task<string> Encrypt(string input)
    {
        var keyHash = Convert.FromHexString(key);
        var ivHash = Convert.FromHexString(iv);

        var crypto = Aes.Create();

        crypto.Key = keyHash;
        crypto.IV = ivHash;

        var bytesInput = Encoding.Unicode.GetBytes(input);

        var ms = new MemoryStream();
        var cs = new CryptoStream(ms, crypto.CreateEncryptor(), CryptoStreamMode.Write);
        await cs.WriteAsync(bytesInput, 0, bytesInput.Length);
        await cs.FlushFinalBlockAsync();

        cs.Close();
        ms.Close();

        return Convert.ToBase64String(ms.ToArray());
    }

    public static async Task<string> Decrypt(string input)
    {
        var keyHash = Convert.FromHexString(key);
        var ivHash = Convert.FromHexString(iv);

        var crypto = Aes.Create();

        crypto.Key = keyHash;
        crypto.IV = ivHash;

        var bytesInput = Convert.FromBase64String(input);

        var ms = new MemoryStream();
        var cs = new CryptoStream(ms, crypto.CreateDecryptor(), CryptoStreamMode.Write);
        await cs.WriteAsync(bytesInput, 0, bytesInput.Length);
        await cs.FlushFinalBlockAsync();

        cs.Close();
        ms.Close();

        return Encoding.Unicode.GetString(ms.ToArray());
    }
}