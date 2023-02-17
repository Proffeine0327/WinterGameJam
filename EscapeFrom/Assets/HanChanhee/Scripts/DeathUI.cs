using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{
    private static DeathUI ui;
    
    // Start is called before the first frame update
    public static void Death(Sprite sprite)
    {
        ui.transform.GetChild(0).gameObject.SetActive(true);
        ui.transform.GetChild(1).gameObject.SetActive(true);
        
        ui.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
        SoundManager.PlaySound("DeathSound", 0, 1f, Player.player.transform.position);
        float time = 0f;
        
        while(time < 3f)
        {
            time += Time.deltaTime;
        }
        
        Application.Quit();
        
        

    }
    void Start()
    {
        ui = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
        
    
}
