using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private Image staminaBar;

    void Update()
    {
        staminaBar.fillAmount = Mathf.Round(Player.Stamina) / 100;
    }
}
