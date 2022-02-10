using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
// Add System.IO to work with files!
using System.IO;
// Add System.Security.Crytography to use Encryption!
using System.Security.Cryptography;

public class DataEncrpytion : MonoBehaviour
{
    Dictionary<string, string> userPass = new Dictionary<string, string>();

    string privateKey = "abc123";

    public GameObject manager;

    public Text passwordText;
    public Text accountText;

    public InputField account;
    public InputField password;

    private void Start()
    {
       
    }

    public void Login(GameObject key)
    {
        if (key.GetComponent<InputField>().text == privateKey)
        {
            manager.SetActive(true);
            key.SetActive(false);
        }
    }

    public void LookUpAccount(GameObject account)
    {
        foreach (KeyValuePair<string, string> usernameAndPass in userPass)
        {
            if (account.GetComponent<InputField>().text == usernameAndPass.Key)
            {
                accountText.text = usernameAndPass.Key;
                passwordText.text = usernameAndPass.Value;
            }
        }
    }

    public void AddAccount()
    {
        if (account.text.Length > 0 && password.text.Length > 0)
        {
            userPass.Add(account.text, password.text);
        }

        writeFile();
    }

    // Create a field for the save file.
    string saveFile;

    // Create a GameData field.
    public GameData gameData = new GameData();

    // FileStream used for reading and writing files.
    FileStream dataStream;

    // Key for reading and writing encrypted data.
    // (This is a "hardcoded" secret key. )
    byte[] savedKey = { 0x16, 0x15, 0x16, 0x15, 0x16, 0x15, 0x16, 0x15, 0x16, 0x15, 0x16, 0x15, 0x16, 0x15, 0x16, 0x15 };

    void Awake()
    {
        // Update the path once the persistent path exists.
        saveFile = Application.persistentDataPath + "/gamedata.json";
        readFile();
    }

    public void readFile()
    {
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            // Create FileStream for opening files.
            dataStream = new FileStream(saveFile, FileMode.Open);

            // Create new AES instance.
            Aes oAes = Aes.Create();

            // Create an array of correct size based on AES IV.
            byte[] outputIV = new byte[oAes.IV.Length];

            // Read the IV from the file.
            dataStream.Read(outputIV, 0, outputIV.Length);

            // Create CryptoStream, wrapping FileStream
            CryptoStream oStream = new CryptoStream(
                   dataStream,
                   oAes.CreateDecryptor(savedKey, outputIV),
                   CryptoStreamMode.Read);

            // Create a StreamReader, wrapping CryptoStream
            StreamReader reader = new StreamReader(oStream);

            // Read the entire file into a String value.
            string text = reader.ReadToEnd();

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            gameData = JsonUtility.FromJson<GameData>(text);
            int i = 0;
            foreach(string key in gameData.usernames.Split(','))
            {

                userPass.Add(key, gameData.passwords.Split(',')[i]);
                Debug.Log(userPass[key]);
                i++;
            }

            // Close FileStream.
            dataStream.Close();
        }
    }

    public void writeFile()
    {
        // Create new AES instance.
        Aes iAes = Aes.Create();

        // Create a FileStream for creating files.
        dataStream = new FileStream(saveFile, FileMode.Open);

        // Save the new generated IV.
        byte[] inputIV = iAes.IV;

        // Write the IV to the FileStream unencrypted.
        dataStream.Write(inputIV, 0, inputIV.Length);

        // Create CryptoStream, wrapping FileStream.
        CryptoStream iStream = new CryptoStream(
                dataStream,
                iAes.CreateEncryptor(savedKey, iAes.IV),
                CryptoStreamMode.Write);

        // Create StreamWriter, wrapping CryptoStream.
        StreamWriter sWriter = new StreamWriter(iStream);

        foreach (KeyValuePair<string, string> value in userPass) {
            gameData.usernames = gameData.usernames + "" + value.Key;
            gameData.passwords = gameData.passwords + "" + value.Value;
        }
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(gameData);
        Debug.Log(jsonString);

        // Write to the innermost stream (which will encrypt).
        sWriter.Write(jsonString);

        // Close StreamWriter.
        sWriter.Close();

        // Close CryptoStream.
        iStream.Close();

        // Close FileStream.
        dataStream.Close();
    }
}
[System.Serializable]
public class GameData
{
    public string usernames;
    public string passwords;
}