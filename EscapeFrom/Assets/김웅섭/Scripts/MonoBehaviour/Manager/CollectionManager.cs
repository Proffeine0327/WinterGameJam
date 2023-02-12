using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager manager;

    public List<GameObject> collections = new List<GameObject>();

    private void Awake() 
    {
        manager = this;    
    }
}
