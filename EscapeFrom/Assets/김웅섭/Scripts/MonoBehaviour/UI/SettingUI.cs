using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingUI : MonoBehaviour
{
    private static SettingUI ui;

    public static void ActiveUI(bool active)
    {
        ui.settingGroup.SetActive(active);
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = active;

        ui.Invoke(() => ui.isShowing = active, 0.01f);
    }

    [Header("Setting")]
    [SerializeField] private GameObject settingGroup;
    [SerializeField] private Slider hSensivity;
    [SerializeField] private TMP_InputField hSensivityInputField;
    [SerializeField] private Slider vSensivity;
    [SerializeField] private TMP_InputField vSensivityInputField;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_InputField volumeInputField;
    [SerializeField] private Button return2menu;
    [Header("Info")]
    [SerializeField] private bool isShowing;
    [Header("SaveFile")]
    [SerializeField] private SettingInfo settingInfo;

    public static SettingInfo SettingInfo { get { return ui.settingInfo; } }
    public static bool IsShowing { get { return ui.isShowing; } }

    private bool isHSensivityInputFieldSelected;
    private bool isVSensivityInputFieldSelected;
    private bool isVolumeInputFieldSelected;

    private void Awake()
    {
        ui = this;

        return2menu.onClick.AddListener(() =>{
            SaveLoadManager.Save("test/test.json", settingInfo);
            ActiveUI(false);

            if(!StartMenuUI.IsStart)
            {
                StartMenuUI.ActiveUI(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else EscUI.ActiveUI(true);
        });

        hSensivityInputField.onSubmit.AddListener((str) =>{
            float value;
            if (float.TryParse(str, out value))
            {
                hSensivity.value = value;
            }
            isHSensivityInputFieldSelected = false;
        });

        vSensivityInputField.onSubmit.AddListener((str) =>
        {
            float value;
            if (float.TryParse(str, out value))
            {
                vSensivity.value = value;
            }
            isVSensivityInputFieldSelected = false;
        });

        volumeInputField.onSubmit.AddListener((str) =>
        {
            int value;
            if (int.TryParse(str, out value))
            {
                volumeSlider.value = value;
            }
            isVolumeInputFieldSelected = false;
        });

        if (!SaveLoadManager.Load<SettingInfo>("test/test.json", ref settingInfo))
        {
            var newSetting = new SettingInfo(Vector2.one * 1.5f, 50);
            settingInfo = newSetting;
            SaveLoadManager.Save("test/test.json", newSetting);
        }

        hSensivity.value = settingInfo.mouseSensivity.x;
        vSensivity.value = settingInfo.mouseSensivity.y;
        volumeSlider.value = settingInfo.volume;

        settingGroup.SetActive(false);
    }

    private void Update() 
    {
        EventHandle();
        ApplyValue();
        InputFieldHandle();
    }

    public void EventHandle()
    {
        if (isShowing && Input.GetKeyDown(KeyCode.Escape))
        {
            SaveLoadManager.Save("test/test.json", settingInfo);
            ActiveUI(false);

            if(!StartMenuUI.IsStart)
            {
                StartMenuUI.ActiveUI(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else EscUI.ActiveUI(true);
        }
    }

    private void ApplyValue()
    {
        settingInfo.mouseSensivity.x = hSensivity.value;
        settingInfo.mouseSensivity.y = vSensivity.value;

        settingInfo.volume = volumeSlider.value;
    }

    private void InputFieldHandle()
    {
        isHSensivityInputFieldSelected = hSensivityInputField.isFocused;
        isVSensivityInputFieldSelected = vSensivityInputField.isFocused;
        isVolumeInputFieldSelected = volumeInputField.isFocused;

        if (!isHSensivityInputFieldSelected)
            hSensivityInputField.text = Math.Round(hSensivity.value, 2).ToString();

        if (!isVSensivityInputFieldSelected)
            vSensivityInputField.text = Math.Round(vSensivity.value, 2).ToString();

        if (!isVolumeInputFieldSelected)
            volumeInputField.text = volumeSlider.value.ToString();
    }
}
