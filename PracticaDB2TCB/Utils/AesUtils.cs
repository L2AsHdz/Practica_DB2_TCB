using System.Security.Cryptography;
using System.Text;

namespace PracticaDB2TCB.Utils;

public class AesUtils
{
    private static readonly byte[] Clave = new byte[32];
    private static readonly byte[] Iv = new byte[16];

    public static byte[] CifrarTexto(string texto)
    {
        var textoBytes = Encoding.UTF8.GetBytes(texto);

        using var aes = Aes.Create();
        aes.Key = Clave;
        aes.IV = Iv;

        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(textoBytes, 0, textoBytes.Length);
        cs.Close();
        var textoCifrado = ms.ToArray();
        return textoCifrado;
    }

    public static string DescifrarTexto(byte[] textoCifrado)
    {
        using var aes = Aes.Create();
        aes.Key = Clave;
        aes.IV = Iv;

        using var ms = new MemoryStream(textoCifrado);
        using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        var textoDescifrado = sr.ReadToEnd();
        return textoDescifrado;
    }
}