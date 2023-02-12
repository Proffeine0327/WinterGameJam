using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExplainUI : MonoBehaviour
{
    private static ExplainUI ui;

    public static void ControlUI(string explain, float time)
    {
        ui.text.text = explain;
        ui.actionBarTime = time;
    }

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float actionBarTime;

    private void Awake() 
    {
        ui = this;
    }

    private void Update() 
    {
        if(actionBarTime > 0) 
        {
            actionBarTime -= Time.deltaTime;
            text.gameObject.SetActive(true);
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }
}
