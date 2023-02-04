using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractUI : MonoBehaviour
{
    private static InteractUI ui;

    public static void ControlUI(bool active, string show)
    {
        ui.text.gameObject.SetActive(active);
        
        ui.text.text = $"[E] {show}";
    }

    [SerializeField] private TextMeshProUGUI text;

    private void Awake() 
    {
        ui = this;
    }
}
