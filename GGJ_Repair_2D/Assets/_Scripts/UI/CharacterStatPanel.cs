using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatPanel : MonoBehaviour
{
    public int characterID;
    public Text statOutput;
    public Text nameLabel;
    public Image portraitHolder;

    // Start is called before the first frame update
    void Start()
    {
        BuildStatOutput();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuildStatOutput()
    {
        string output = "";


        // TODO: Stats!!!!!


        statOutput.text = output;
    }
}
