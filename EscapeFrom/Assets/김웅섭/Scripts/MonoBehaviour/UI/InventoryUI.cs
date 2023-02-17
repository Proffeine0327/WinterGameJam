using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum InventoryUIShowType
{
    disable,
    menu,
    detail
}

public class InventoryUI : MonoBehaviour
{
    private static InventoryUI ui;

    public static void ShowUI()
    {
        ui.Invoke(() =>
        {
            ui.background.SetActive(true);
            ui.menuGroup.SetActive(true);
            ui.showType = InventoryUIShowType.menu;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }, 0.01f);
    }

    public static void ControlExplaineUI(bool active, string explaine)
    {
        ui.explain.enabled = active;
        ui.explain.text = explaine;
    }

    public static void UpdateSlots()
    {
        int slotCount = ui.slots.Count;
        int itemCount = Player.Items.Count;

        while (slotCount > itemCount)
        {
            Destroy(ui.slots[slotCount - 1].gameObject);
            ui.slots.RemoveAt(slotCount - 1);
            slotCount--;
        }

        for (int i = 0; i < slotCount; i++)
        {
            var item = Player.Items[i];
            ui.slots[i].Init(item.ItemSprite, item.ItemName, item.ItemExplaine, () =>
            {
                if (ui.showType != InventoryUIShowType.detail)
                    if (item.DetailShow != null)
                    {
                        ui.currentDetail = Instantiate(item.DetailShow, ui.background.transform);
                        ui.menuGroup.SetActive(false);
                        ui.showType = InventoryUIShowType.detail;
                        InventoryUI.ControlExplaineUI(false, null);
                    }
            });
        }

        for (int i = slotCount; i < itemCount; i++) //x += 240
        {
            var item = Player.Items[i];
            var slot = Instantiate(ui.slotPrefeb, ui.slotGroup);

            slot.GetComponent<SlotUI>().Init(item.ItemSprite, item.ItemName, item.ItemExplaine, () =>
            {
                if (ui.showType != InventoryUIShowType.detail)
                    if (item.DetailShow != null)
                    {
                        ui.currentDetail = Instantiate(item.DetailShow, ui.background.transform);
                        ui.menuGroup.SetActive(false);
                        ui.showType = InventoryUIShowType.detail;
                        InventoryUI.ControlExplaineUI(false, null);
                    }
            });

            ui.slots.Add(slot.GetComponent<SlotUI>());
            slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(1920 * (i / 10) + 320 + 320 * (i % 5), 222 + -444 * ((i % 10) / 5));
        }
    }

    [Header("setting")]
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject menuGroup;
    [SerializeField] private RectTransform slotGroup;
    [SerializeField] private GameObject slotPrefeb;
    [SerializeField] private TextMeshProUGUI explain;
    [SerializeField] private Button left;
    [SerializeField] private Button right;
    [Header("Info")]
    [SerializeField] private InventoryUIShowType showType;
    [SerializeField] private int page;
    [SerializeField] private List<SlotUI> slots;
    [SerializeField] private GameObject currentDetail;

    public static InventoryUIShowType ShowType { get { return ui.showType; } }

    private void Awake()
    {
        ui = this;
    }

    private void Start()
    {
        explain.enabled = false;

        right.onClick.AddListener(() =>
        {
            if (showType == InventoryUIShowType.menu)
            {
                page++;
            }
        });

        left.onClick.AddListener(() =>
        {
            if (showType == InventoryUIShowType.menu)
            {
                page--;
            }
        });

    }

    private void Update()
    {
        if (showType == InventoryUIShowType.menu)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                this.Invoke(() =>
                {
                    background.SetActive(false);
                    menuGroup.SetActive(false);
                    showType = InventoryUIShowType.disable;

                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }, 0.01f);
            }
        }
        if (showType == InventoryUIShowType.detail)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Destroy(currentDetail);
                currentDetail = null;

                menuGroup.SetActive(true);
                showType = InventoryUIShowType.menu;
            }
        }

        if (page <= 0) left.gameObject.SetActive(false);
        else left.gameObject.SetActive(true);

        if (page >= (slots.Count - 1) / 10) right.gameObject.SetActive(false);
        else right.gameObject.SetActive(true);

        slotGroup.anchoredPosition = new Vector2(-1920 * page, 0);

        page = Mathf.Clamp(page, 0, (slots.Count - 1) / 10);
    }
}
