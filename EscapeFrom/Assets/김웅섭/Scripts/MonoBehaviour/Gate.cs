using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isLocked;
    [SerializeField] private string keyName;

    public void Interact()
    {
        foreach(var item in Player.Items)
        {
            PhotoableKey key;
            if(item.TryGetComponent<PhotoableKey>(out key))
            {
                if(key.KeyName == keyName)
                {
                    isLocked = false;
                    Player.Items.Remove(item);
                    InventoryUI.UpdateSlots();

                    break;
                }
            }
        }

        if(isLocked)
        {
            ExplainUI.ControlUI("열쇠가 필요하다", 1.5f);
            SoundManager.PlaySound("metalDoor", 0, 0.6f, Player.player.transform.position);
        }
        else
        {
            ExplainUI.ControlUI("문이 열렸다", 1.5f);
            SoundManager.PlaySound("metalDoor", 1, 0.6f, Player.player.transform.position);
            enabled = false;
            gameObject.SetActive(false);
        }
    }

    public void ShowUI()
    {
        InteractUI.ControlUI(true, "열기");
    }
}
