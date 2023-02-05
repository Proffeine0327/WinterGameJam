using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isLocked;
    [SerializeField] private string keyName;

    public void Interact()
    {
        foreach(var key in Key.keys)
        {
            if(key == keyName) isLocked = false;
        }

        if(isLocked)
        {
            ExplainUI.ControlUI("열쇠가 필요하다", 1.5f);
            SoundManager.PlaySound(AudioClipName.metalDoorLocked, 0.6f, Player.player.transform.position);
        }
        else
        {
            ExplainUI.ControlUI("문이 열렸다", 1.5f);
            SoundManager.PlaySound(AudioClipName.metalDoorOpen, 0.6f, Player.player.transform.position);
            enabled = false;
            gameObject.SetActive(false);
        }
    }

    public void ShowUI()
    {
        InteractUI.ControlUI(true, "열기");
    }
}
