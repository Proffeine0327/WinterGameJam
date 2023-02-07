using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoableKey : MonoBehaviour, IPhotoable
{
    [SerializeField] private string keyName;
    public void Take()
    {
        Key.keys.Add(keyName);
        ExplainUI.ControlUI($"{keyName} 열쇠를 얻었다", 2.5f);
        gameObject.SetActive(false);
    }
}
