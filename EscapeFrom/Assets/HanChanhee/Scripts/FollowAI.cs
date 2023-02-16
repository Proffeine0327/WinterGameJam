using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{

    public GameObject player = Player.player.gameObject; //플레이어
    NavMeshAgent nav;
    public float speed;

    public Sprite deathImage;
    public string deathSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.transform.position);
        nav.speed = speed;
        if (Vector3.Distance(transform.position, player.transform.position) < 0.5f || !Player.player.isHide)
        {
            DeathUI.Death(deathImage, deathSound);
        }
    }
}
