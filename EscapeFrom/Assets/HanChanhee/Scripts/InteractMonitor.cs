using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMonitor : MonoBehaviour, IInteractable
{
    public bool isEnable = false;
    public Floor4Ghost floor4Ghost;
    public void Interact()
    {
        if(!isEnable)
        {
            isEnable = true;
            ExplainUI.ControlUI("컴퓨터 전원을 켰습니다.", 2.5f);
            floor4Ghost.MonitorOn(transform);

        }
    }

    public void ShowUI()
    {
        if(!isEnable)
            InteractUI.ControlUI(true, "컴퓨터 켜기");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
