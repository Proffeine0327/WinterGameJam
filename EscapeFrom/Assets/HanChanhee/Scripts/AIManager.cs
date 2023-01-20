using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    public List<Transform> MovePoint = new List<Transform>(); //순찰지점
    public GameObject player; //플레이어
    NavMeshAgent nav; //NavMeshAgent
    RaycastHit hit; //레이캐스트
    Vector3 moveRot; //플레이어 쪽 방향
    public float curtime, moveCooltime = 7; //현시점, 움직임 대기시간
    public bool isCastPlayer = false; //플레이어 감지 유무
    
    int curPoint = 0;
    
    public State state = State.None;

    public enum State
    {
        None, Catch, Move
    }


    
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        switch(state)
        {
            case State.None:
                curtime += Time.deltaTime;
                if (Vector3.Distance(transform.position, player.transform.position) < 5f)
                {
                    curtime = 0;
                    isCastPlayer = true;
                    state = State.Catch;
                    
                }
                if (curtime >= moveCooltime)
                {
                    curtime = 0;
                    state = State.Move;
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
                    state = State.None;
                }
                if (Vector3.Distance(transform.position, player.transform.position) < 5f)
                {
                    isCastPlayer = true;
                    state = State.Catch;
                    
                }
                break;

            case State.Catch:
                nav.SetDestination(player.transform.position);
                
                if (Vector3.Distance(transform.position, player.transform.position) >= 10f)
                {
                    isCastPlayer = false;
                    state =State.None;
                }
                break;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }



}
