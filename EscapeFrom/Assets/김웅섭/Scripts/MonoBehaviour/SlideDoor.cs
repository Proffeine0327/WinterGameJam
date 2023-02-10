using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform group;
    [SerializeField] private Vector3 openPos;
    [SerializeField] private bool isOpen;

    public void Interact()
    {
        isOpen = !isOpen;
    }

    public void ShowUI()
    {
        if(isOpen)
            InteractUI.ControlUI(true, "닫기");
        else
            InteractUI.ControlUI(true, "열기");
    }

    private void Update() {
        if(isOpen) group.localPosition = openPos;
        else group.localPosition = Vector3.zero;
    }
}
