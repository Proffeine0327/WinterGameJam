using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Door : MonoBehaviour, IInteractable
{
    public int doorState = -1;
    public bool doorEnabled = false;
    public bool isLocked = false;
    bool isDelay = false;
    bool isActiveActionBar = false;
    public Transform targetDoor;
    public Transform openDoor;
    string explain = "[Door State]";
    public string keyName = "";
    Vector3 originPos;

    private void Awake()
    {
        originPos = openDoor.position;
    }

    void Update()
    {
        switch(doorState)
        {
            case 0:
                break;
            case 1:
                openDoor.position = Vector3.MoveTowards(openDoor.position, new Vector3(targetDoor.position.x, openDoor.position.y, openDoor.position.z), Time.deltaTime * 2);
                explain = "닫기";
                if (Vector3.Distance(openDoor.position, new Vector3(targetDoor.position.x, openDoor.position.y, openDoor.position.z)) < 0.5f)
                {
                    doorEnabled = true;

                    isDelay = false;
                }
                break;
            case -1:
                openDoor.position = Vector3.MoveTowards(openDoor.position, originPos, Time.deltaTime * 2);
                explain = "열기";
                if (Vector3.Distance(openDoor.position, originPos) < 0.5f)
                {
                    doorEnabled = false;
                    isDelay = false;
                }
                break;
        }
    }

    public void OpenDoor(int value)
    {
        if(value > 1 || value < -1)
        {
            return;
        }
        doorState = value;
    }

    public void Open()
    {
        if (!isDelay && !isLocked)
        {
            doorState = doorState * -1;
            isDelay = true;
        }
    }

    public void Interact()
    {
        if(!isDelay && !isLocked)
        {
            doorState = doorState * -1;
            isDelay = true;
        } else if(isLocked)
        {
            if(Key.keys.Contains(keyName))
            {
                isLocked = false;
                ExplainUI.ControlUI("문을 열었다", 2);
            } 
            else
            {
                ExplainUI.ControlUI("이 문은 잠겨있다", 2);
            }
        }
    }

    public void ShowUI()
    {
        InteractUI.ControlUI(true, explain);
    }
}
