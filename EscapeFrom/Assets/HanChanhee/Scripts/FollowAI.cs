using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{

    NavMeshAgent nav;
    public GameObject player;   //플레이어
    public float speed;

    public Sprite deathImage;
   
    // Start is called before the first frame update
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        player = Player.player.gameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.transform.position);
        nav.speed = speed;
        if (Vector3.Distance(transform.position, player.transform.position) < 1f && !Player.player.isHide)
        {
            Debug.Log("Death");
            DeathUI.Death(deathImage);
            gameObject.SetActive(false);
        }
    }
}
