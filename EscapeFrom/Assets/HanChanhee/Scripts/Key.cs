using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public string keyCode = "<KeyCode>";

    public string explain = "Get <KeyCode>";

    public static List<string> keys = new List<string>();


    public static void AddKey(string key)
    {
        keys.Add(key);
    }

    // Start is called before the first frame update
    void Awake()
    {
        explain = $"Get {keyCode}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        AddKey(keyCode);
        gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        InteractUI.ControlUI(true, explain);
    }
}
