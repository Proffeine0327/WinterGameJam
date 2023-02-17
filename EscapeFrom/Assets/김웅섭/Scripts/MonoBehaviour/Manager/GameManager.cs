using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager manager;

    [Header("1f")]
    [SerializeField] private int currentStage;
    [SerializeField] private string active_1fghost_item_name;
    [SerializeField] private Vector3 trigger_1fghost_point;
    [SerializeField] private float trigget_1fghost_range;
    [SerializeField] private GameObject school_front_door_shutter;

    private bool is_player_open_detail = false;
    private bool is_1fboss_displayed = false;
    private bool is_player_has_trigger_item = false;

    private void Awake() 
    {
        manager = this;

        school_front_door_shutter.SetActive(false);
    }

    private void Update()
    {
        if(currentStage == 0)
        {
            if(!is_player_has_trigger_item)
                is_player_has_trigger_item = Player.Items.Select((Item item) => item.gameObject.name ).Contains(active_1fghost_item_name);

            if(
                !is_player_open_detail &&
                is_player_has_trigger_item && 
                InventoryUI.ShowType == InventoryUIShowType.detail
            )
            {
                Debug.Log("player open detail");
                is_player_open_detail = true;
            }

            if(!is_1fboss_displayed && is_player_open_detail && InventoryUI.ShowType == InventoryUIShowType.disable)
            {
                Debug.Log("1fboss");
                is_1fboss_displayed = true;
                school_front_door_shutter.SetActive(true);
                this.Invoke(() => {
                    Floor1Ghost.ActiveBoss(true);
                }, 2f);
            }
            
            if(!is_1fboss_displayed && is_player_has_trigger_item && Physics.CheckSphere(trigger_1fghost_point, trigget_1fghost_range, LayerMask.GetMask("Player")))
            {
                Debug.Log("1fboss2");
                is_1fboss_displayed = true;
                school_front_door_shutter.SetActive(true);
                Floor1Ghost.Agent.enabled = false;
                Floor1Ghost.ghost.transform.position = new Vector3(-13.61f,-3.566f,-1.36f);
                Floor1Ghost.Agent.enabled = true;
                Floor1Ghost.ActiveBoss(true);
            }       
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(trigger_1fghost_point, trigget_1fghost_range);
    }
}
