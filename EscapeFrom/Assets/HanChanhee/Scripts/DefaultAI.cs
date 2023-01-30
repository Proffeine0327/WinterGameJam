using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DefaultAI : MonoBehaviour
{
    public List<Transform> MovePoint = new List<Transform>(); //��������
    public GameObject player; //�÷��̾�
    
    NavMeshAgent nav; //NavMeshAgent
   // Vector3 moveRot; //�÷��̾� �� ����
    public float curtime, moveCooltime = 7; //������, ������ ���ð�
    public bool isCastPlayer = false; //�÷��̾� ���� ����
    
    int curPoint = 0;

    public float normalSpeed = 1.5f;
    public float followSpeed = 2;

    public float doorDelayTime = 1f;

    public State state = State.None;
    State prevState;

    public enum State
    {
        None, Catch, Move, Delay
    }

    public float catchDistance;
    public float followDistance;
    
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckDoor();
    }

    void CheckDoor()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, Vector3.one * 2);
        foreach(Collider collider in colliders)
        {
            if(collider.gameObject.tag == "Door")
            {
                Door door = collider.gameObject.GetComponent<Door>();
                if(!door.doorEnabled && door.doorState == -1)
                {

                    curtime = 0;
                    ChangeState(State.Delay);
                    door.Interact();
                } 

            }
        }
        
    }

    void ChangeState(State s)
    {
        prevState = state;
        state = s;
    }

    private void Move()
    {
        Player p = player.GetComponent<Player>();
        switch(state)
        {
            case State.None:
                nav.SetDestination(transform.position);
                curtime += Time.deltaTime;
                if (Vector3.Distance(transform.position, player.transform.position) < catchDistance && !p.isHide)
                {
                    curtime = 0;
                    isCastPlayer = true;
                    
                    ChangeState(State.Catch);
                    
                }
                if (curtime >= moveCooltime)
                {
                    curtime = 0;
                    ChangeState(State.Move);
                }

                break;
            case State.Move:
                nav.SetDestination(MovePoint[curPoint].position);
                if (Vector3.Distance(transform.position, MovePoint[curPoint].position) < 0.6f)
                {
                    if (curPoint == MovePoint.Count - 1)
                    {
                        curPoint = 0;
                    }
                    else
                    {
                        curPoint++;
                    }
                    ChangeState(State.None);
                }
                if (Vector3.Distance(transform.position, player.transform.position) < catchDistance && !p.isHide)
                {
                    isCastPlayer = true;
                    ChangeState(State.Catch);

                }
                break;

            case State.Catch:
                nav.speed = followSpeed;
                nav.SetDestination(player.transform.position);
                
                if (Vector3.Distance(transform.position, player.transform.position) >= followDistance || p.isHide)
                {
                    isCastPlayer = false;
                    for(int i = 0; i < MovePoint.Count; i++)
                    {
                        
                        if(Vector3.Distance(transform.position, MovePoint[i].position) < Vector3.Distance(transform.position, MovePoint[curPoint].position))
                        {
                            curPoint = i;
                        }
                    }
                    ChangeState(State.None);
                }
                break;
            case State.Delay:
               
                nav.speed = 0;
                curtime += Time.deltaTime;
                if(curtime >= doorDelayTime)
                {
                    nav.speed = normalSpeed;
                    curtime = 0;
                    ChangeState(prevState);
                }
                break;

        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, catchDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, followDistance);
        
        Gizmos.DrawWireCube(transform.position, Vector3.one * 2);
    }
}
