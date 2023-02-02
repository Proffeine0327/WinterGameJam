using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EscUI : MonoBehaviour
{
    private static EscUI ui;

    public static void ShowEscMenu()
    {
        ui.isShowingEscMenu = true;
        ui.menuGroup.SetActive(true);
        ui.background.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    [Header("boolen")]
    [SerializeField] private bool isShowingEscMenu;
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
    [SerializeField] private TMP_InputField hSensivityText;
    [SerializeField] private Slider vSensivity;
    [SerializeField] private TMP_InputField vSensivityText;
    [SerializeField] private Button return2menu;
    [Header("SaveFile")]
    [SerializeField] private SettingInfo settingInfo;

    public static bool IsShowingEscMenu { get { return ui.isShowingEscMenu; } }

    public bool isHSensivityTextSelected;
    public bool isVSensivityTextSelected;

    private void Awake()
    {
        ui = this;
    }

    void Start()
    {
        return2menu.onClick.AddListener(() =>
        {
            settingInfo.mouseSensivity.x = hSensivity.value;
            settingInfo.mouseSensivity.y = vSensivity.value;
            SaveLoadManager.Save("test/test.json", settingInfo);

            menuGroup.SetActive(true);
            settingGroup.SetActive(false);
        });

        return2game.onClick.AddListener(() =>
        {
            isShowingEscMenu = false;
            menuGroup.SetActive(false);
            background.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        });

        setting.onClick.AddListener(() =>
        {
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
        
        hSensivityText.onSubmit.AddListener((str) => {
            float value;
            if(float.TryParse(str, out value))
            {
               hSensivity.value = value; 
            }
            isHSensivityTextSelected = false;
        });

        vSensivityText.onSubmit.AddListener((str) => {
            float value;
            if(float.TryParse(str, out value))
            {
               hSensivity.value = value; 
            }
            isVSensivityTextSelected = false;
        });

        SaveLoadManager.Load<SettingInfo>("test/test.json", ref settingInfo);
        hSensivity.value = settingInfo.mouseSensivity.x;
        vSensivity.value = settingInfo.mouseSensivity.y;

        background.SetActive(false);
        menuGroup.SetActive(false);
        settingGroup.SetActive(false);
    }

    private void Update()
    {
        isHSensivityTextSelected = hSensivityText.isFocused;
        isVSensivityTextSelected = vSensivityText.isFocused;

        if (!isHSensivityTextSelected)
            hSensivityText.text = Math.Round(hSensivity.value, 2).ToString();

        if (!isVSensivityTextSelected)
            vSensivityText.text = Math.Round(vSensivity.value, 2).ToString();
    }
}
