using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hide : MonoBehaviour, IInteractable
{
    public Transform hideObject;
    public string explain = "Hide";
    public bool isHide = false;
    public Transform player;
    public Transform outPos;
    bool isDelay = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDelay)
        {

            Out();
        }
    }
    public void Interact()
    {
        Player p = player.GetComponent<Player>();
        if(isHide)
        {
            
            //isHide = false;
            //explain = "Hide";
            //CharacterController p = player.GetComponent<CharacterController>();
            //p.enabled = false;

            //p.transform.position =
            //outPos.position;
            //p.enabled = true;
           
        } if(!isHide && !p.isHide)
        {
            isHide = true;
           
            p.isHide = true;
            p.isRunning = false;
            p.SetHeadHob(false);
            explain = "Out";
            CharacterController cc = player.GetComponent<CharacterController>();
            cc.enabled = false;

            cc.transform.position =
            hideObject.position;
            //p.enabled = true;
            player.eulerAngles = new Vector3(hideObject.rotation.x, hideObject.rotation.y - 90, hideObject.rotation.z);
            StartCoroutine(delay());
        }
    }

    

    void Out()
    {
        Player p = player.GetComponent<Player>();
        if(isHide && p.isHide)
        {
            InteractUI.ControlUI(true, explain);
        } 
       
        
        if(Input.GetKeyDown(KeyCode.E) && isHide && p.isHide && isDelay)
        {
            
            isHide = false;
            p.isHide=false;
            p.SetHeadHob(true);
            explain = "Hide";
            CharacterController cc = player.GetComponent<CharacterController>();
            cc.enabled = false;

            cc.transform.position =
            outPos.position;
            cc.enabled = true;
            isDelay = false;
        }
    }

    public void ShowUI()
    {
        InteractUI.ControlUI(true, explain);
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        isDelay = true;
    }
}
