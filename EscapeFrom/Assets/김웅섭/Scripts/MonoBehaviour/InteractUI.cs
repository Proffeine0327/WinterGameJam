using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractUI : MonoBehaviour
{
    private static InteractUI ui;

    public static void ControlUI(bool active, string explain)
    {
        ui.group.SetActive(active);
        ui.explain.text = $"[E] {explain}";
    }

    [SerializeField] public GameObject group;
    [SerializeField] public TextMeshProUGUI explain;
    
    private void Awake() 
    {
        ui = this;
    }
}
