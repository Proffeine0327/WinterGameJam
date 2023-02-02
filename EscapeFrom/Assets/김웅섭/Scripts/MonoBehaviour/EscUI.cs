using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscUI : MonoBehaviour
{
    [Header("Test")]
    [SerializeField] private Button btn;
    [Header("Setting")]
    [SerializeField] private Slider horizontal_sensivity;
    [SerializeField] private Slider vertical_sensivity;
    [Header("SaveFile")]
    [SerializeField] private SettingInfo settings;

    void Start()
    {
        btn.onClick.AddListener(() => 
        {
            settings.mouseSensivity.x = horizontal_sensivity.value;
            settings.mouseSensivity.y = vertical_sensivity.value;
            SaveLoadManager.Save("test/test.json", settings);
        });

        SaveLoadManager.Load<SettingInfo>("test/test.json", ref settings);
        horizontal_sensivity.value = settings.mouseSensivity.x;
        vertical_sensivity.value = settings.mouseSensivity.y;
    }
}
