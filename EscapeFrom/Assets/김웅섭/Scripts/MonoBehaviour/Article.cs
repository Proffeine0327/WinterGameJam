using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Article : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        InteractUI.ControlUI(true, "Get");
    }
}
