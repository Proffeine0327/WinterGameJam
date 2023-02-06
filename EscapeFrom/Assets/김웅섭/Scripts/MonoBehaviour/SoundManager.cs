using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioClipGroup
{
    [SerializeField] private List<AudioClip> clips = new List<AudioClip>();
    public List<AudioClip> Clips { get { return clips; } }
}

public class SoundManager : MonoBehaviour, ISerializationCallbackReceiver
{
    private static SoundManager manager;

    public static int GetArraySize(string key)
    {
        return manager.clipDictionary[key].Clips.Count;
    }

    public static void PlaySound(string key, int index, float volume, Vector3 pos)
    {
        var obj = new GameObject(manager.clipDictionary[key].Clips[index].name);
        obj.transform.position = pos;
        obj.transform.SetParent(manager.gameObject.transform);

        var audiosource = obj.AddComponent<AudioSource>();
        audiosource.clip = manager.clipDictionary[key].Clips[index];
        audiosource.volume = volume * manager.masterVolume;
        audiosource.Play();

        Destroy(obj, manager.clipDictionary[key].Clips[index].length + 1f);
    }

    [SerializeField] private List<string> keys = new List<string>();
    [SerializeField] private List<AudioClipGroup> values = new List<AudioClipGroup>();
    [SerializeField] private float masterVolume;
    public Dictionary<string, AudioClipGroup> clipDictionary = new Dictionary<string, AudioClipGroup>();

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (var clip in clipDictionary)
        {
            keys.Add(clip.Key);
            values.Add(clip.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        clipDictionary = new Dictionary<string, AudioClipGroup>();
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++) clipDictionary.Add(keys[i], values[i]);
    }

    private void Awake()
    {
        manager = this;
    }

    private void Update()
    {
        masterVolume = EscUI.SettingInfo.volume / 100;
    }
}