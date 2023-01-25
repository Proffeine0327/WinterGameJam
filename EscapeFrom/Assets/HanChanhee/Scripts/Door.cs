using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    public int doorState = -1;
    public bool doorEnabled = false;    
    public Transform targetDoor;
    public Transform openDoor;
    string explain = "[Door State]";
    Vector3 originPos;
    



    private void Awake()
    {
        originPos = openDoor.position;
    }

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        switch(doorState)
        {
            case 0:
                break;
            case 1:
                openDoor.position = Vector3.MoveTowards(openDoor.position, new Vector3(targetDoor.position.x, openDoor.position.y, openDoor.position.z), Time.deltaTime * 2);
                explain = "Close";
                if (Vector3.Distance(openDoor.position, new Vector3(targetDoor.position.x, openDoor.position.y, openDoor.position.z)) < 0.5f)
                    doorEnabled = true;
                break;
            case -1:
                openDoor.position = Vector3.MoveTowards(openDoor.position, originPos, Time.deltaTime * 2);
                explain = "Open";
                if (Vector3.Distance(openDoor.position, originPos) < 0.5f)
                    doorEnabled = false;
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

    public void Interact()
    {
        doorState = doorState * -1;
       
    }

    public void ShowUI()
    {
        
        InteractUI.ControlUI(true, explain);
    }
}
