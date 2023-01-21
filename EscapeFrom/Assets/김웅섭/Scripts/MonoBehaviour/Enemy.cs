using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void Update()
    {
        var pos = Player.player.transform.position;
        pos.y = transform.position.y;
        transform.LookAt(pos);    
        
    }
}
