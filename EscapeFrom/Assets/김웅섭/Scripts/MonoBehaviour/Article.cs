using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Article : MonoBehaviour, IPhotoable
{
    public void Take()
    {
        gameObject.SetActive(false);
    }
}
