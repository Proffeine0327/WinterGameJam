using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shuter : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    public float upHeight = 1f;
    public string keyCode = "";

    string explain = "Open";

    public bool isLocked = true;

    public float originY = 0;

    public Vector3 origin;

    public float openSpeed = 2f;
    private GameObject ui;
    bool isActiveActionBar = false;

    IEnumerator delay()
    {

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 20; i++)
        {
            Color color = ui.GetComponent<TextMeshProUGUI>().color;
            ui.GetComponent<TextMeshProUGUI>().color = new Color(color.r, color.g, color.b, 1 - i * 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.2f);
        isActiveActionBar = false;
        ui.SetActive(false);
    }

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
                    Color color = ui.GetComponent<TextMeshProUGUI>().color;
                    ui.GetComponent<TextMeshProUGUI>().color = new Color(color.r, color.g, color.b, 1);
                    ui.GetComponent<TextMeshProUGUI>().text = "The Shuter is Opened";
                    ui.gameObject.SetActive(true);
                    StartCoroutine(delay());
                }

            }
        } else
        {
            if (isLocked)
            {
                if(!isActiveActionBar)
                {
                    isActiveActionBar = true;
                    Color color = ui.GetComponent<TextMeshProUGUI>().color;
                    ui.GetComponent<TextMeshProUGUI>().color = new Color(color.r, color.g, color.b, 1);
                    ui.GetComponent<TextMeshProUGUI>().text = "The Shuter is Locked";
                    ui.gameObject.SetActive(true);
                    StartCoroutine(delay());
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
        ui = InteractUI.GetActionBar();
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
                ui.SetActive(false);
            } 
        }
    }
}
