using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoableKey : Item, IPhotoable
{
    [Header("key")]
    [SerializeField] private string keyName;

    public string KeyName { get { return keyName; } }
    
    public void Take()
    {
        Player.Items.Add(this);
        InventoryUI.UpdateSlots();
        ExplainUI.ControlUI($"{keyName} 열쇠를 얻었다", 2.5f);
        gameObject.SetActive(false);
    }
}
