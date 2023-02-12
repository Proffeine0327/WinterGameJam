using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : Item, IPhotoable
{
    public void Take()
    {
        Player.Items.Add(this);
        InventoryUI.UpdateSlots();
        ExplainUI.ControlUI("파일을 얻었다", 2.5f);
        gameObject.SetActive(false);
    }
}
