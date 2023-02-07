using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hide : MonoBehaviour, IInteractable
{
    public Transform hideObject;
    public string explain = "¼û±â";
    public bool isHide = false;
   
    public Transform outPos;
    bool isDelay = false;
    public float angle = 0;

    // Start is called before the first frame update
    void Awake()
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
        Player p = Player.player;
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
            explain = "³ª°¡±â";
            CharacterController cc = p.GetComponent<CharacterController>();
            cc.enabled = false;

            cc.transform.position =
                hideObject.position;
            //p.enabled = true;
            p.transform.eulerAngles = new Vector3(hideObject.rotation.x, angle, hideObject.rotation.z);
            StartCoroutine(delay());
        }
    }

    

    void Out()
    {
        Player p = Player.player;
        if(isHide && p.isHide)
        {
            
            InteractUI.ControlUI(true, explain);
        } 
       
        
        if(Input.GetKeyDown(KeyCode.E) && isHide && p.isHide && isDelay)
        {
            
            isHide = false;
            p.isHide=false;
            p.SetHeadHob(true);
            explain = "¼û±â";
            CharacterController cc = p.GetComponent<CharacterController>();
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
        explain = "³ª°¡±â";
        InteractUI.ControlUI(true, explain);
        yield return new WaitForSeconds(0.5f);
        isDelay = true;
    }
}
