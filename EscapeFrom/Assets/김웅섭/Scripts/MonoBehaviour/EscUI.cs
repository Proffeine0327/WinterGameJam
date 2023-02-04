using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EscUIShowType
{
    disable,
    menu,
    setting,
}

public class EscUI : MonoBehaviour
{
    private static EscUI ui;

    public static void ShowEscMenu()
    {
        ui.showType = EscUIShowType.menu;
        ui.menuGroup.SetActive(true);
        ui.background.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    [Header("boolen")]
    [SerializeField] private EscUIShowType showType;
    [Header("Background")]
    [SerializeField] private GameObject background;
    [Header("Menu")]
    [SerializeField] private GameObject menuGroup;
    [SerializeField] private Button return2game;
    [SerializeField] private Button setting;
    [SerializeField] private Button exit;
    [Header("Setting")]
    [SerializeField] private GameObject settingGroup;
    [SerializeField] private Slider hSensivity;
    [SerializeField] private TMP_InputField hSensivityInputField;
    [SerializeField] private Slider vSensivity;
    [SerializeField] private TMP_InputField vSensivityInputField;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_InputField volumeInputField;
    [SerializeField] private Button return2menu;
    [Header("SaveFile")]
    [SerializeField] private SettingInfo settingInfo;

    public static EscUIShowType ShowType { get { return ui.showType; } }
    public static SettingInfo SettingInfo { get { return ui.settingInfo; } }

    public bool isHSensivityInputFieldSelected;
    public bool isVSensivityInputFieldSelected;
    public bool isVolumeInputFieldSelected;

    private void Awake()
    {
        ui = this;

        return2menu.onClick.AddListener(() =>
        {
            showType = EscUIShowType.menu;
            SaveLoadManager.Save("test/test.json", settingInfo);

            menuGroup.SetActive(true);
            settingGroup.SetActive(false);
        });

        return2game.onClick.AddListener(() =>
        {
            showType = EscUIShowType.disable;
            menuGroup.SetActive(false);
            background.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        });

        setting.onClick.AddListener(() =>
        {
            showType = EscUIShowType.setting;
            menuGroup.SetActive(false);
            settingGroup.SetActive(true);
        });

        exit.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        });

        hSensivityInputField.onSubmit.AddListener((str) =>
        {
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

        background.SetActive(false);
        menuGroup.SetActive(false);
        settingGroup.SetActive(false);
    }

    private void Update()
    {
        EventHandle();
        ApplyValue();
        InputFieldHandle();
    }

    private void EventHandle()
    {
        if (showType == EscUIShowType.menu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.Invoke(() =>
                {
                    showType = EscUIShowType.disable;
                    menuGroup.SetActive(false);
                    background.SetActive(false);

                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }, 0.01f);
            }
        }
        if (showType == EscUIShowType.setting)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                showType = EscUIShowType.menu;
                SaveLoadManager.Save("test/test.json", settingInfo);

                menuGroup.SetActive(true);
                settingGroup.SetActive(false);
            }
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
