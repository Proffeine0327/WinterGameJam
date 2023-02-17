using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{
    private static DeathUI ui;

    public static void Death(Sprite sprite)
    {
        ui.transform.GetChild(0).gameObject.SetActive(true);
        ui.transform.GetChild(1).gameObject.SetActive(true);

        ui.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
        SoundManager.PlaySound("DeathSound", 0, 1f, Player.player.transform.position);

        ui.Invoke(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }, 1f);
    }
    
    void Awake()
    {
        ui = this;
    }
}
