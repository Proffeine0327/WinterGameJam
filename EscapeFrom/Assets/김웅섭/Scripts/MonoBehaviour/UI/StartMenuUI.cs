using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenuUI : MonoBehaviour
{
    private static StartMenuUI ui;

    public static void ActiveUI(bool active)
    {
        ui.title.gameObject.SetActive(active);
        ui.start.gameObject.SetActive(active);
        ui.setting.gameObject.SetActive(active);
        ui.quit.gameObject.SetActive(active);
    }

    [Header("setting")]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Button start;
    [SerializeField] private Button setting;
    [SerializeField] private Button quit;
    [Header("Info")]
    [SerializeField] private bool isStart;
    [SerializeField] private float animationPlaytime;
    [SerializeField] private bool isAnimating;

    public static bool IsStart { get { return ui.isStart; } }

    private void Awake()
    {
        ui = this;
        
        start.onClick.AddListener(() => {
            GameStart();
        });

        setting.onClick.AddListener(() => {
            ActiveUI(false);
            SettingUI.ActiveUI(true);
        });

        quit.onClick.AddListener(() => {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        });
    }

    public void GameStart()
    {
        if (!isAnimating && !isStart)
        {
            isAnimating = true;
            StartCoroutine(StartAnimation());
        }
    }

    IEnumerator StartAnimation()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        RenderSettings.fog = true;
        var startDensity = RenderSettings.fogDensity;

        var startTmp = start.GetComponentInChildren<TextMeshProUGUI>();
        var settingTmp = setting.GetComponentInChildren<TextMeshProUGUI>();
        var quitTmp = quit.GetComponentInChildren<TextMeshProUGUI>();

        for (float i = animationPlaytime; i > 0; i -= Time.deltaTime)
        {
            yield return null;

            RenderSettings.fogDensity = startDensity + (1 - startDensity) * ((animationPlaytime - i) / animationPlaytime);
            title.color = new Color(title.color.r, title.color.g, title.color.b, i / animationPlaytime);
            startTmp.color = new Color(startTmp.color.r, startTmp.color.g, startTmp.color.b, i / animationPlaytime);
            settingTmp.color = new Color(settingTmp.color.r, settingTmp.color.g, settingTmp.color.b, i / animationPlaytime);
            quitTmp.color = new Color(quitTmp.color.r, quitTmp.color.g, quitTmp.color.b, i / animationPlaytime);
        }

        ActiveUI(false);
        Player.player.hasHandCam = true;

        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            yield return null;
            RenderSettings.fogDensity = 1 - 0.73f * i;
        }

        isAnimating = false;
        isStart = true;
    }
}
