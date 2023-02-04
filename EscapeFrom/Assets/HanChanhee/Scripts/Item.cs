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


    protected void GetActionBar()
    {
        ui = InteractUI.GetActionBar();
    }

    public virtual void Interact()
    {
        
    }

    public virtual void ShowUI()
    {
        
    }


    protected IEnumerator FadeUI()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 20; i++)
        {
            Color color = ui.GetComponent<TextMeshProUGUI>().color;
            ui.GetComponent<TextMeshProUGUI>().color = new Color(color.r, color.g, color.b, 1 - i * 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.2f);
        isDelay = false;
        ui.SetActive(false);
    }
}
