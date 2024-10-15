using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static readonly string ENCRYPTION_KEY = "YourEncryptionKey123"; //TODO
    private static readonly string SAVE_NAME = "Save_";
    private static readonly string HEADERS_PATH = Path.Combine(Application.persistentDataPath, $"{SAVE_NAME}headers.json");
    private static string SavePath(int saveSlot) => Application.persistentDataPath + "/saves/" + SAVE_NAME + saveSlot + ".sav";
    public static bool IsSaveExisting(int saveSlot) => File.Exists(SavePath(saveSlot));

    public static List<HeaderData> headers = LoadAllHeaders();

    public static void Save(GameData data, int saveSlot)
    {
        string json = JsonUtility.ToJson(data);
        string encryptedJson = Encrypt(json, ENCRYPTION_KEY);
        File.WriteAllText(SavePath(saveSlot), encryptedJson);

        SetHeader(new HeaderData(saveSlot, data));
    }

    public static GameData Load(int saveSlot)
    {
        string filePath = SavePath(saveSlot);
        if (File.Exists(filePath))
        {
            string encryptedJson = File.ReadAllText(filePath);
            string json = Decrypt(encryptedJson, ENCRYPTION_KEY);
            return JsonUtility.FromJson<GameData>(json);
        }
        return null;
    }

    public static GameData LoadLatest()
    {
        List<HeaderData> headers = LoadAllHeaders();

        int latestSaveSlot = -1;
        DateTime latestTime = DateTime.MinValue;

        foreach (HeaderData header in headers)
        {
            DateTime lastModified = header.lastUpdate;
            if (lastModified > latestTime)
            {
                latestTime = lastModified;
                latestSaveSlot = header.slot;
            }
        }

        return latestSaveSlot == -1 ? null : Load(latestSaveSlot);
    }

    public static void Delete(int saveSlot)
    {
        string path = SavePath(saveSlot);
        if (File.Exists(path)) File.Delete(path);
        headers.Remove(GetHeader(saveSlot));
    }

    public static HeaderData GetHeader(int saveSlot)
    {
        return headers.Find(header => header.slot == saveSlot);
    }

    public static void SetHeader(HeaderData header)
    {
        headers.RemoveAll(headerData => headerData.slot == header.slot);
        headers.Add(header);

        SaveHeaders();
    }

    private static void SaveHeaders()
    {
        string json = JsonUtility.ToJson(headers);
        File.WriteAllText(HEADERS_PATH, json);
    }

    private static List<HeaderData> LoadAllHeaders()
    {
        string json = File.ReadAllText(HEADERS_PATH);
        return JsonUtility.FromJson<List<HeaderData>>(json);
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
        return reader.ReadToEnd();
    }
}
