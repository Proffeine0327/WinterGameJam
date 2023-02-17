using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoor : MonoBehaviour, IInteractable
{
    public bool isLocked;
    [SerializeField] private string keyName;
    [SerializeField] private Transform group;
    [SerializeField] private Vector3 openPos;
    public bool isOpen;
    [Header("group door")]
    [SerializeField] private List<SlideDoor> groupDoors = new List<SlideDoor>();

    public void Interact()
    {
        if(isLocked)
        {
            foreach(var item in Player.Items)
            {
                if(item.GetComponent<PhotoableKey>()?.KeyName == keyName)
                {
                    isLocked = false;
                    ExplainUI.ControlUI("문이 열렸다", 1.5f);
                    Player.Items.Remove(item);

                    foreach (var door in groupDoors) door.isLocked = false;
                    return;
                }
            }

            ExplainUI.ControlUI($"{keyName} 열쇠가 필요하다", 2.5f);
            SoundManager.PlaySound("woodDoor", 1, 1f, Player.player.transform.position);
            return;
        }

        isOpen = !isOpen;
        SoundManager.PlaySound("woodDoor", 0, 1f, Player.player.transform.position);
        CheckOpen();
    }

    public void Open()
    {
        isOpen = !isOpen;
    }

    public void CheckOpen()
    {
        Floor3Ghost floor3Ghost = Floor3Ghost.GetFloor3Ghost();
        if (floor3Ghost != null)
        {
            floor3Ghost.OpenDoor(this);
        }
    }

    public void ShowUI()
    {
        if(isOpen)
            InteractUI.ControlUI(true, "닫기");
        else
            InteractUI.ControlUI(true, "열기");
    }

    private void Update() {
        if(isOpen) group.localPosition = openPos;
        else group.localPosition = Vector3.zero;
    }
}
