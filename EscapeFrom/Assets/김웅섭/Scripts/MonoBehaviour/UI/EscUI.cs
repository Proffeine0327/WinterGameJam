using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EscUI : MonoBehaviour
{
    private static EscUI ui;

    public static void ActiveUI(bool active)
    {
        ui.menuGroup.SetActive(active);
        ui.background.SetActive(active);

        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = active;
        ui.Invoke(() => ui.isShowing = active, 0.01f);
    }

    [Header("boolen")]
    [SerializeField] private bool isShowing;
    [Header("Background")]
    [SerializeField] private GameObject background;
    [Header("Menu")]
    [SerializeField] private GameObject menuGroup;
    [SerializeField] private Button return2game;
    [SerializeField] private Button setting;
    [SerializeField] private Button exit;

    public static bool IsShowing { get { return ui.isShowing; } }

    private void Awake()
    {
        ui = this;

        return2game.onClick.AddListener(() =>
        {
            ActiveUI(false);
        });

        setting.onClick.AddListener(() =>
        {   
            ActiveUI(false);
            SettingUI.ActiveUI(true);
        });

        exit.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        });

        background.SetActive(false);
        menuGroup.SetActive(false);
    }

    private void Update()
    {
        if (isShowing)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) ActiveUI(false);
        }
    }
}
