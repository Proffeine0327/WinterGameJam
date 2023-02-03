using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingInfo
{
    public SettingInfo(Vector2 mouseSen, float volume)
    {
        this.mouseSensivity = mouseSen;
        this.volume = volume;
    }

    public Vector2 mouseSensivity;
    public float volume;
}
