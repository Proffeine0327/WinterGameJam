using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public static List<string> keys = new List<string>();

    [SerializeField] private string keyName;

    public void Interact()
    {
        ExplainUI.ControlUI($"{keyName} 열쇠를 얻었다", 2.5f);

        keys.Add(keyName);
        enabled = false;
        gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        InteractUI.ControlUI(true, "가져가기");
    }
}
