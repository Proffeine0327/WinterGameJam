using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 hidePos;
    [SerializeField] private Vector3 outPos;
    [SerializeField] private bool isHide;

    public void Interact()
    {
        if(isHide)
        {
            isHide = !isHide;
            Player.player.transform.position = transform.position + -transform.up * 1;
            Player.player.GetComponent<CharacterController>().enabled = true;
            Player.player.isHide = false;
        }
        else
        {
            isHide = !isHide;
            Player.player.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            Player.player.GetComponent<CharacterController>().enabled = false;
            Player.player.isHide = true;
        }
    }

    public void ShowUI()
    {
        if(isHide)
            InteractUI.ControlUI(true, "나가기");
        else
            InteractUI.ControlUI(true, "숨기");
    }

    private void Update() 
    {
        if(isHide) Player.player.transform.position = transform.position;
    }
}
