using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("UI Info")]
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private string itemName;
    [SerializeField] private string itemExplaine;
    [SerializeField] private GameObject detailShow;

    public Sprite ItemSprite { get { return itemSprite; } }
    public string ItemName { get { return itemName; } }
    public string ItemExplaine { get { return itemExplaine; } }
    public GameObject DetailShow { get { return detailShow; } }
}
