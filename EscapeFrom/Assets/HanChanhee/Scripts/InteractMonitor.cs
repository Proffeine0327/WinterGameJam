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
            ExplainUI.ControlUI("��ǻ�� ������ �׽��ϴ�.", 2.5f);
            floor4Ghost.MonitorOn(transform);

        }
    }

    public void ShowUI()
    {
        if(!isEnable)
            InteractUI.ControlUI(true, "��ǻ�� �ѱ�");
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
