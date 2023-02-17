using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;
//using System;




public class Floor3Ghost : MonoBehaviour
{
    // Start is called before the first frame update
    //public List<Transform> rooms = new List<Transform>();
    //public List<Vector3> sizes = new List<Vector3>();
    public int[] orders;
    public int curRoom = 1;

    public float size = 1;

    private List<int> arraySize = new List<int>();

    public SlideDoor[] doors;

    public Transform[] balls;

    public Sprite deathImage;
   

    public static Floor3Ghost GetFloor3Ghost()
    {
        
        return floor3Ghost;
    }

    static Floor3Ghost floor3Ghost;
    

    public void OpenDoor(SlideDoor door)
    {
        for(int i = 0; i < doors.Length; i++)
        {
            if(door == doors[i])
            {
                if(orders[i] == curRoom)
                {
                    Debug.Log("Pass");
                    curRoom++;
                } else if(orders[i] < curRoom)
                {
                    Debug.Log("Nothing");
                } else if(orders[i] > curRoom)
                {
                    Debug.Log("Death");
                    DeathUI.Death(deathImage);
                    this.enabled = false;

                }
            }
        }
    }

    void Awake()
    {
        floor3Ghost = this;

        
        
    }

    // Update is called once per frame
    void Update()
    {
       CheckBalls();
    }

    void CheckBalls()
    {
        for(int i = 0; i < balls.Length; i++)
        {
            Collider[] hits = Physics.OverlapSphere(balls[i].position, size / 2);
            foreach(Collider hit in hits)
            {
                if(hit.gameObject == Player.player.gameObject)
                {
                    DeathUI.Death(deathImage);
                    gameObject.SetActive(false);
                }
            }

        }
    }


    

    private void OnDrawGizmos()
    {
        for(int i = 0; i < balls.Length; i++)
        {
            
            
            
                Gizmos.color = Color.red;
            
            Gizmos.DrawWireSphere(balls[i].position, size);
        }
    }
}
