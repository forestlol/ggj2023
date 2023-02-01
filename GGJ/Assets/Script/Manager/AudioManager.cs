using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    List<AudioHandler> audioList;

    Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();

    AudioSource source;

    // Start is called before the first frame update

    public static AudioManager instance;
    void Awake()
    {
        AudioManager[] objs = FindObjectsOfType<AudioManager>();

        if(objs.Length > 1)
            Destroy(this.gameObject);

        instance = this;
    }

    private void Start()
    {
        if(!source)
            source = GetComponent<AudioSource>();
        AddAudioClipToDict();
    }

    void AddAudioClipToDict()
    {
        for (int i = 0; i < audioList.Count; ++i)
        {
            string audioName = audioList[i].audioName;
            AudioClip clip = audioList[i].clip;

            audioDict[audioName] = clip;
        }
    }

    public void PlaySound(string soundName)
    {
        source.clip = audioDict[soundName];
        source.Play();
    }
}

[Serializable]
public class AudioHandler
{
    public string audioName;
    public AudioClip clip;
}
