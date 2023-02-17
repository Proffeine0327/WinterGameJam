using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMonitor : MonoBehaviour, IInteractable
{
    public bool isEnable = false;
    public bool one = false;
    public Floor4Ghost floor4Ghost;

    public GameObject hand;

    public GameObject[] hands;


    public void Interact()
    {
        if(!isEnable && !one)
        {
            one = true;
            isEnable = true;
            ExplainUI.ControlUI("컴퓨터 전원을 켰습니다.", 2.5f);
            floor4Ghost.MonitorOn(transform);

        } else if(isEnable)
        {
            isEnable= false;
            floor4Ghost.MonitorOff(transform);
            //hand.SetActive(false);

        }
    }

    public void ShowUI()
    {
        if (!isEnable && !one)
            InteractUI.ControlUI(true, "컴퓨터 켜기");
        else if(isEnable)
            InteractUI.ControlUI(true, "컴퓨터 끄기");
    }

    // Start is called before the first frame update
    void Start()
    {
        //hand = Instantiate(hands[Random.Range(0, hands.Length)], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
