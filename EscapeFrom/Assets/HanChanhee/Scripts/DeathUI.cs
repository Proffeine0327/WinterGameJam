using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{
    public static GameObject ui;
    
    // Start is called before the first frame update
    public static void Death(Sprite sprite, string deathSound)
    {
        ui.SetActive(true);
        ui.GetComponent<Image>().sprite = sprite;
        SoundManager.PlaySound(deathSound, 0, 1f, Player.player.transform.position);
        float time = 0f;
        
        while(time < 2f)
        {
            time += Time.deltaTime;
        }

        Application.Quit();
        

    }
    void Awake()
    {
        ui = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
        
    
}
