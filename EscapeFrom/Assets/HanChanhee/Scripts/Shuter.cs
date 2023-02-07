using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shuter : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    public float upHeight = 1f;
    public string keyCode = "";

    string explain = "열기";

    public bool isLocked = true;

    public float originY = 0;

    public Vector3 origin;

    public float openSpeed = 2f;
    private GameObject ui;
    bool isActiveActionBar = false;

    

    public void Interact()
    {
        if(Key.keys.Contains(keyCode))
        {
            if(isLocked)
            {
                isLocked = false;
                if (!isActiveActionBar)
                {
                    isActiveActionBar = true;
                    ExplainUI.ControlUI("셔텨를 열었다", 1.5f);
                }

            }
        } else
        {
            if (isLocked)
            {
                if(!isActiveActionBar)
                {
                    isActiveActionBar = true;
                    ExplainUI.ControlUI("이 셔터는 잠겨 있다", 1.5f);
                    
                }

            }
        }
    }

    public void ShowUI()
    {
        if(isLocked)
            InteractUI.ControlUI(true, explain);
    }

    void Start()
    {
        originY = transform.position.y;
        origin = transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocked)
        {
            transform.position = 
                Vector3.MoveTowards(transform.position, new Vector3(origin.x, originY + upHeight, origin.z), Time.deltaTime * openSpeed);
            if((originY + upHeight) - transform.position.y < 0.3f)
            {
                this.gameObject.SetActive(false);
                //ui.SetActive(false);
            } 
        }
    }
}
