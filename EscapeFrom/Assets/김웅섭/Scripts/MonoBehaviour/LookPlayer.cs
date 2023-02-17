using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayer : MonoBehaviour
{
    void Update()
    {
        var relative = Player.player.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg, 0);
    }
}
