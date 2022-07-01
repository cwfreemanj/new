using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleGenerator : MonoBehaviour
{

    [SerializeField] List<string> playerNames;
    [SerializeField] List<string> playerDescriptors;
    [SerializeField] List<string> swordNames;
    [SerializeField] List<string> swordDescriptors;
    [SerializeField] List<string> identifiers;

    [SerializeField] Text title;

    public void Start()
    {
        string newTitle = "" + identifiers[Random.Range(0, identifiers.Count - 1)] + " "+ swordNames[Random.Range(0, swordNames.Count - 1)] + " " + swordDescriptors[Random.Range(0, swordDescriptors.Count - 1)];

        title.text = newTitle;
        
    }
    public void GenerateTitle()
    {

    }
}


