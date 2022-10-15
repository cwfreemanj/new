using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
// Add System.IO to work with files!
using System.IO;
// Add System.Security.Crytography to use Encryption!
using System.Security.Cryptography;

public class DataEcryption : MonoBehaviour
{
    Dictionary<string, string> userPass = new Dictionary<string, string>();
    Dictionary<string, string> QA = new Dictionary<string, string>();

    List<Dictionary<string, string>> allQA = new List<Dictionary<string, string>>();

    string newQA = "";

    string privateKey = "abc123";

    public GameObject manager;

    public Text passwordText;
    public Text accountText;
    public Text questionText;
    public Text answerText;

    public InputField account;
    public InputField password;
    public InputField question;
    public InputField answer;

    public void UpdateQuestions()
    {
        userPass.Add(account.text, password.text);
        // Add account information to dictionary

        bool hasAccount = false;

        // Does allQA contain a QA with the current account name?

        if (allQA.Count > 0)
        {

            foreach (Dictionary<string, string> qa in allQA)
            {
                foreach (KeyValuePair<string, string> value in qa)
                {
                    if (value.Key == account.text)
                    {
                        hasAccount = true;
                        // update information inside

                    }

                }
            }
        }

        if (!hasAccount)
        {
            QA.Add(account.text, password.text);

            string key = question.GetComponent<InputField>().text;
            //Debug.Log(QA[key]);
            if (QA.ContainsKey(key))
            {
                QA.Remove(key);
                QA.Add(key, answer.text);
                //  Debug.Log(QA[key]);

            }
            else
            {
                QA.Add(key, answer.text);
                //Debug.Log(QA[key]);
            }

            Dictionary<string, string> tempQA = QA;
            allQA.Add(tempQA);
            Debug.Log(allQA.Count);

            //QA.Clear();

        }


        newQA += "," + account.text;
        newQA += "," + question.text;
        newQA += "," + answer.text;
        

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
        readFile();

        //Debug.Log(gameData.myQAlist);

        int i = 0;
        foreach (string key in gameData.usernames.Split(' '))
        {
            if (!userPass.ContainsKey(key))
                userPass.Add(key, gameData.passwords.Split(' ')[i]);

            i++;
        }

        i = 0;

        

       // Debug.Log(questions[0]);

       /* foreach (string key in gameData.question.Split(' '))
        {
            Debug.Log(key);
            if (key == account.GetComponent<InputField>().text)
            {
                if (!QA.ContainsKey(key))
                    QA.Add(key, gameData.answer.Split(' ')[i]);

            }
            else
            {
                if (!QA.ContainsKey(key))
                    QA.Add(key, gameData.answer.Split(' ')[i]);

                // Display question and answers
                foreach (KeyValuePair<string, string> qa in QA)
                {

                    // If first key in QA is equal to account lookup text
                    if (qa.Key == account.GetComponent<InputField>().text)
                    {
                        continue;
                    }
                    {
                        //  break
                    }


                    // Setting question text
                    questionText.text = qa.Key;
                    Debug.Log(qa.Key);
                    // Setting answer text
                    answerText.text = qa.Value;
                    Debug.Log(qa.Value);
                    break;

                }


                i++;
            }

           


        }*/
        foreach (KeyValuePair<string, string> usernameAndPass in userPass)
        {
            if (account.GetComponent<InputField>().text == usernameAndPass.Key)
            {
                accountText.text = usernameAndPass.Key;
                passwordText.text = usernameAndPass.Value;
            }

        }

        string[] questions = gameData.question.Split(',');
        int index = 0;
        foreach (string q in questions)
        {
            if (index > 0)
            {
                if (index == 2)
                {
                    answerText.text = q;
                    break;
                }
                if (index == 1)
                {
                    questionText.text = q;
                    index++;
                }
                
            }

            if (q == account.GetComponent<InputField>().text)
            {
                index++;
            }
        }
    }

    public void AddAccount()
    {
        if (account.text.Length > 0 && password.text.Length > 0)
        {
            UpdateQuestions();
            Debug.Log("Hello, finished saving information!");
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
        saveFile = Application.persistentDataPath + "/gamedata1.json";
        //readFile();
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

            // Close FileStream.
            dataStream.Close();
        }
    }

    public void writeFile()
    {
        // Create new AES instance.
        Aes iAes = Aes.Create();

        // Create a FileStream for creating files.
        dataStream = new FileStream(saveFile, FileMode.Create);

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

        foreach (KeyValuePair<string, string> value in userPass)
        {
            gameData.usernames = gameData.usernames + " " + value.Key;
            gameData.passwords = gameData.passwords + " " + value.Value;
        }

        gameData.question += newQA;
        Debug.Log(gameData.question);
        /*foreach (KeyValuePair<string, string> value in QA)
        {
            //TO DO
            // We have list of QA dictionaries
            // Need to separate each dictionary
            // Then, separate each item in the dictionary to it's own string

            gameData.question = gameData.question + " " + value.Key;
            gameData.answer = gameData.answer + " " + value.Value;
        }

        gameData.myQAlist = allQA;
        Debug.Log(gameData.myQAlist);*/
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
    public string question;
    public string answer;

    public List<Dictionary<string, string>> myQAlist;
}
