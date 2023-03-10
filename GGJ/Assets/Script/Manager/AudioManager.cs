using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    List<AudioHandler> audioList;

    Dictionary<string, AudioSource> audioDict = new Dictionary<string, AudioSource>();

    // Start is called before the first frame update

    public static AudioManager instance;
    void Awake()
    {
        AudioManager[] objs = FindObjectsOfType<AudioManager>();

        if(objs.Length > 1)
            Destroy(this.gameObject);

        instance = this;

        AddAudioClipToDict();
    }

    void AddAudioClipToDict()
    {
        for (int i = 0; i < audioList.Count; ++i)
        {
            string audioName = audioList[i].audioName;
            audioDict[audioName] = audioList[i].audio;
        }
    }

    public void PlaySound(string soundName, Transform soundPos)
    {
        AudioSource sound = Instantiate(audioDict[soundName], soundPos);
        sound.Play();
        Destroy(sound, sound.clip.length);
    }
}

[Serializable]
public class AudioHandler
{
    public string audioName;
    public AudioSource audio;
}
