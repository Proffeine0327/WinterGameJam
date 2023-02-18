using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Floor1Ghost : MonoBehaviour
{
    [SerializeField] private Sprite deathImg;
    [SerializeField] private GameObject ghostSprite;
    [SerializeField] private float doorCastLength;
    [SerializeField] private bool isAppeared;

    private AudioSource audiosource;

    public static Floor1Ghost ghost;

    public static void ActiveBoss(bool active)
    {
        ghost.agent.enabled = active;
        ghost.ghostSprite.SetActive(active);
        ghost.isAppeared = active;
    }

    private NavMeshAgent agent;

    public static NavMeshAgent Agent { get { return ghost.agent; } }

    void Awake()
    {
        ghost = this;

        agent = GetComponent<NavMeshAgent>();

        ActiveBoss(false);
    }

    void Update()
    {
        if(isAppeared)
        {
            if(audiosource == null)
            {
                audiosource = SoundManager.PlaySound("Noise", 0, 0.7f, transform.position, true);
            }
            else audiosource.transform.position = transform.position;

            if(Vector3.Distance(transform.position, Player.player.transform.position) < 1f)
            {
                DeathUI.Death(deathImg);
            }

            if (Player.player.isHide) ActiveBoss(false);
        }
        else
        {
            Destroy(audiosource.gameObject);
        }

        if (agent.enabled)
        {
            agent.SetDestination(Player.player.transform.position);
            var casts = Physics.RaycastAll(transform.position, transform.forward, doorCastLength);

            foreach (var obj in casts)
            {
                SlideDoor door;
                if (obj.collider.TryGetComponent<SlideDoor>(out door))
                {
                    if (!door.isOpen)
                    {
                        door.isOpen = true;
                        agent.enabled = false;
                        this.Invoke(() => { agent.enabled = true; Debug.Log("enable"); }, 0.25f);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * doorCastLength);
    }
}
