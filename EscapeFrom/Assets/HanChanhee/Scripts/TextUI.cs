using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUI : MonoBehaviour
{
    private static TextUI ui;

    public static GameObject GetLetterUI()
    {
        return ui.letter;
    }
    
    [SerializeField] private GameObject letter;
    // Start is called before the first frame update
    void Awake()
    {
        ui = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
