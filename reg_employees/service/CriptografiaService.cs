using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class CriptografiaService
{
    private readonly byte[] key = Encoding.UTF8.GetBytes("ChaveDeTeste1234"); // Use uma chave segura com 16, 24 ou 32 bytes

    public string Criptografar(string texto)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length); // Salva o IV no início do stream

                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(texto);
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    public string Descriptografar(string textoCriptografado)
    {
        try {
            if (!IsBase64String(textoCriptografado)) { 
                throw new FormatException("A string fornecida não é uma Base-64 válida."); 
            }
             byte[] buffer = Convert.FromBase64String(textoCriptografado); 
             using (MemoryStream msDecrypt = new MemoryStream(buffer)) { 
                using (Aes aesAlg = Aes.Create()) { 
                    byte[] iv = new byte[16]; 
                    msDecrypt.Read(iv, 0, iv.Length); 
                    aesAlg.Key = key; aesAlg.IV = iv; 
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV); 
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) 
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt)) { 
                        var decryptedText = srDecrypt.ReadToEnd(); 
                        Console.WriteLine($"Texto Descriptografado: {decryptedText}"); 
                        return decryptedText; 
                    } 
                } 
            } 
        } catch (FormatException ex) { 
            Console.WriteLine($"Erro de Formato: {ex.Message}"); 
            throw; 
        } 
    }

    public bool IsBase64String(string base64) { 
        Span<byte> buffer = new Span<byte>(new byte[base64.Length]); 
        return Convert.TryFromBase64String(base64, buffer, out _); 
    }
}
