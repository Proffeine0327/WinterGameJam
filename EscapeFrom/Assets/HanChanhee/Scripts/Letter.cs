using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : Item
{
    // Start is called before the first frame update
    public GameObject textUI;
    bool isActive = false;

    public List<string> list = new List<string>();

    
    
    void Start()
    {
        GetActionBar();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)) && isActive) {
            textUI.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    public override void Interact()
    {
        base.Interact();
        if(!isActive)
        {
            textUI.SetActive(true);
            isActive = true;
            for(int i = 0; i < textUI.transform.childCount; i++)
            {
                TextMeshProUGUI text = textUI.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                text.text = "";
                if(list[i] != null)
                {
                   
                    text.text = list[i];
                }
            }

        }
        
    }

    public override void ShowUI()
    {
        base.ShowUI();
        InteractUI.ControlUI(true, "Get " + explain);
    }

    


}
