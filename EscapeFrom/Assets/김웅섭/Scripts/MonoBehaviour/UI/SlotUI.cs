using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image img;
    [SerializeField] private TextMeshProUGUI tm;
    [Header("info")]
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private string itemName;
    [SerializeField] private string itemExplaine;
    private Action clickAction;

    /// <summary>
    /// Init this slot.
    /// </summary>
    /// <param name="sprite">sprite to display</param>
    /// <param name="name">name of this item</param>
    /// <param name="explain">item explain</param>
    /// <param name="detail">instantiate this when slot is clicked.</param>
    public void Init(Sprite sprite, string name, string explain, Action clickAction)
    {
        this.itemSprite = sprite;
        this.img.sprite = sprite;

        this.itemName = name;
        this.tm.text = name;
        
        this.itemExplaine = explain;
        this.clickAction += clickAction;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickAction?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryUI.ControlExplaineUI(true, itemExplaine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryUI.ControlExplaineUI(false, null);
    }
}
