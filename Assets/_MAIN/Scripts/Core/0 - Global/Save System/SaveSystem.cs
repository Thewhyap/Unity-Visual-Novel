using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static readonly string encryptionKey = "YourEncryptionKey123"; //TODO
    private static string SavePath(string saveName) => Application.persistentDataPath + "/saves/" + saveName + ".sav";
    public static bool IsSaveExisting(string saveSlot) => File.Exists(SavePath(saveSlot));

    public static void Save(SaveData data, string saveSlot)
    {
        string json = JsonUtility.ToJson(data);
        string encryptedJson = Encrypt(json, encryptionKey);
        File.WriteAllText(SavePath(saveSlot), encryptedJson);
    }

    public static SaveData Load(string saveSlot)
    {
        string filePath = SavePath(saveSlot);
        if (File.Exists(filePath))
        {
            string encryptedJson = File.ReadAllText(filePath);
            string json = Decrypt(encryptedJson, encryptionKey);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return null;
    }

    public static void Delete(string saveSlot)
    {
        string path = SavePath(saveSlot);
        if (File.Exists(path)) File.Delete(path);
    }

    public static SaveData LoadLatest()
    {
        string[] saveFiles = Directory.GetFiles(Application.persistentDataPath, "*.sav"); //TODO change this

        string latestFile = null;
        DateTime latestTime = DateTime.MinValue;

        foreach (string file in saveFiles)
        {
            DateTime lastModified = File.GetLastWriteTime(file);
            if (lastModified > latestTime)
            {
                latestTime = lastModified;
                latestFile = file;
            }
        }

        if (latestFile != null)
        {
            return Load(latestFile);
        }

        return null;
    }

    private static string Encrypt(string plainText, string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        using Aes aes = Aes.Create();
        aes.Key = keyBytes;
        aes.GenerateIV();
        byte[] iv = aes.IV;
        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using MemoryStream ms = new();
        ms.Write(iv, 0, iv.Length);
        using (CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write))
        {
            using StreamWriter writer = new(cs);
            writer.Write(plainText);
        }
        return Convert.ToBase64String(ms.ToArray());
    }

    private static string Decrypt(string cipherText, string key)
    {
        byte[] fullCipher = System.Convert.FromBase64String(cipherText);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        using Aes aes = Aes.Create();
        aes.Key = keyBytes;
        byte[] iv = new byte[aes.BlockSize / 8];
        byte[] cipherBytes = new byte[fullCipher.Length - iv.Length];

        Array.Copy(fullCipher, iv, iv.Length);
        Array.Copy(fullCipher, iv.Length, cipherBytes, 0, cipherBytes.Length);

        aes.IV = iv;
        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using MemoryStream ms = new(cipherBytes);
        using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
        using StreamReader reader = new(cs);
        return reader.ReadToEnd(); // Decrypted plaintext
    }
}
