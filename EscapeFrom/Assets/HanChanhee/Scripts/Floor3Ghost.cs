using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor3Ghost : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> rooms = new List<Transform>();
    public List<Vector3> sizes = new List<Vector3>();
    public int[] orders;
    public int curRoom = 1;

    void Awake()
    {
        orders = new int[rooms.Count];
        for (int i = 0; i < rooms.Count; i++)
        {
            orders[i] = Random.Range(1, rooms.Count + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckRooms();
    }


    void CheckRooms()
    {
       
        if(rooms.Count > curRoom)
        {

            for(int i = 0; i < rooms.Count; i++)
            {
                Collider[] colliders = Physics.OverlapBox(rooms[i].position, sizes[i]);
                foreach(Collider collider in colliders)
                {
                    if(Player.player.transform == collider.gameObject.transform)
                    {
                        if(orders[i] != curRoom)
                        {

                            Debug.Log("Death");
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
