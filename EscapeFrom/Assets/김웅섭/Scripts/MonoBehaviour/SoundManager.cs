using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioClipName
{
    Concrete1,
    Concrete2,
    Concrete3,
    Concrete4,
    Concrete5,
    Concrete6,
    Concrete7,
    Concrete8,
    Concrete9,
    Concrete10,
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager manager;

    public static void PlaySound(AudioClipName clip, float volume, Vector3 pos)
    {
        var obj = new GameObject(manager.audios[(int)clip].name);
        obj.transform.position = pos;
        obj.transform.SetParent(manager.gameObject.transform);

        var audiosource = obj.AddComponent<AudioSource>();
        audiosource.clip = manager.audios[(int)clip];
        audiosource.volume = volume * manager.masterVolume;
        audiosource.Play();

        Destroy(obj, manager.audios[(int)clip].length + 1f);
    }
    
    [SerializeField] private List<AudioClip> audios = new List<AudioClip>();
    [SerializeField] private float masterVolume;

    private void Awake() 
    {
        manager = this;
    }

    private void Update() 
    {
        masterVolume = EscUI.SettingInfo.volume / 100;
    }
}
