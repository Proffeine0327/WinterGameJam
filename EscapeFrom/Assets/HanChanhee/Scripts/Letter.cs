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
        textUI = TextUI.GetLetterUI();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.E)) && isActive) {
            textUI.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    public override void Interact()
    {
        
        if(!isActive)
        {
           
            textUI.SetActive(true);
            
            for(int i = 0; i < textUI.transform.childCount; i++)
            {
                TextMeshProUGUI text = textUI.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                text.text = "";
                if(i < list.Count)
                {
                    if(list[i] != null)
                    {
                    
                        text.text = list[i];
                    }

                }
            }
            StartCoroutine(delay());

        }
        
    }

    IEnumerator delay()
    {
        yield return null;
        isActive = true;
    }

    public override void ShowUI()
    {
        
        InteractUI.ControlUI(true, explain + " ¾ò±â");
        
    }

    


}
