using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;
//using System;




public class Floor3Ghost : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> rooms = new List<Transform>();
    public List<Vector3> sizes = new List<Vector3>();
    public int[] orders;
    public int curRoom = 1;

    private List<int> arraySize = new List<int>();

    public Door[] doors;

    public static Floor3Ghost GetFloor3Ghost()
    {
        
        return floor3Ghost;
    }

    static Floor3Ghost floor3Ghost;
    

    public void OpenDoor(Door door)
    {
        for(int i = 0; i < doors.Length; i++)
        {
            if(door == doors[i])
            {
                if(orders[i] == curRoom)
                {

                } else if(orders[i] < curRoom)
                {

                } else if(orders[i] > curRoom)
                {

                }
            }
        }
    }

    void Awake()
    {
        floor3Ghost = this;

        //System.Random random = new System.Random();
        //for(int i = 0; i < rooms.Count; i++)
        //{
        //    arraySize.Add(i + 1);
        //}
        //orders = arraySize.ToArray();
        //orders = orders.OrderBy(x => random.Next()).ToArray();
        
        //orders = new int[rooms.Count];
        
    }

    // Update is called once per frame
    void Update()
    {
       // CheckRooms();
    }


    void CheckRooms()
    {
       
        if(rooms.Count >= curRoom)
        {

            for(int i = 0; i < rooms.Count; i++)
            {
                Collider[] colliders = Physics.OverlapBox(rooms[i].position, sizes[i] /2);
                foreach(Collider collider in colliders)
                {
                    if(Player.player.transform == collider.gameObject.transform)
                    {
                        
                        if(orders[i] != curRoom)
                        {
                            if(curRoom < orders[i])
                            {

                                Debug.Log("Death");
                            }
                            break;
                        }
                        else if(orders[i] == curRoom)
                        {
                            curRoom++;
                            Debug.Log("Pass");
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < rooms.Count; i++)
        {
            if(curRoom == orders[i])
            {
                Gizmos.color = Color.green;
            } else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawWireCube(rooms[i].position, sizes[i]);
        }
    }
}
