using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Door : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    public int doorState = -1;
    public bool doorEnabled = false;
    public bool isLocked = false;

    bool isDelay = false;
    bool isActiveActionBar = false;

    public Transform targetDoor;
    public Transform openDoor;

    string explain = "[Door State]";

    public string keyName = "";

    private GameObject ui;

    Vector3 originPos;
    



    private void Awake()
    {
        originPos = openDoor.position;
        ui = InteractUI.GetActionBar();
        
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
                {
                    doorEnabled = true;

                    isDelay = false;
                }
                break;
            case -1:
                openDoor.position = Vector3.MoveTowards(openDoor.position, originPos, Time.deltaTime * 2);
                explain = "Open";
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
                ui.GetComponent<TextMeshProUGUI>().text = "The Door is Opened";
            } else
            {
                ui.GetComponent<TextMeshProUGUI>().text = "The Door is Locked";

            }
            ui.SetActive(true);

            if (!isActiveActionBar)
            {
                Color color = ui.GetComponent<TextMeshProUGUI>().color;
                ui.GetComponent<TextMeshProUGUI>().color = new Color(color.r, color.g, color.b, 100);
                isActiveActionBar = true;
                StartCoroutine(delay());

            }

        }
       
    }

    IEnumerator delay()
    {
        
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < 20; i++)
        {
            Color color = ui.GetComponent<TextMeshProUGUI>().color;
            ui.GetComponent<TextMeshProUGUI>().color = new Color(color.r, color.g, color.b, 1 - i * 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.2f);
        isActiveActionBar = false;
        ui.SetActive(false);
    }

    public void ShowUI()
    {
        
        InteractUI.ControlUI(true, explain);
    }
}
