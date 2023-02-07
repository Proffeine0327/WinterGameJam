using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    protected GameObject ui;
    public string explain;
    protected bool isDelay = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact()
    {
        
    }

    public virtual void ShowUI()
    {
        
    }


   
}
