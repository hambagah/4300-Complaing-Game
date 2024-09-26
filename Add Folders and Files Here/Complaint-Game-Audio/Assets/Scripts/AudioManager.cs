using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
//I think dont need manager
public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    [System.Serializable]

    public class Sound
    {

        [Header("audio clip")]
        public AudioClip clip;

        [Header("diff music")]
        public AudioMixerGroup outputGroup;

        [Header("audio volume")]
        [Range(0, 1)]
        public float volume;

       [Header("is play")]
        public bool playOnAwake;

        [Header("Loop")]
        public bool loop;
    }

    public List<Sound> sounds;
    private Dictionary<string, AudioSource> audiosDic;


    private void Awake()
    {
        audiosDic = new Dictionary<string, AudioSource>();
    }

    private void Start()
    {
        foreach (var sound in sounds)
        {
            GameObject obj = new GameObject(sound.clip.name);
            obj.transform.SetParent(transform);

            AudioSource source = obj.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.playOnAwake = sound.playOnAwake;
            source.loop = sound.loop;
            source.volume = sound.volume;
            source.outputAudioMixerGroup = sound.outputGroup;

            if(sound.playOnAwake)
                source.Play();

            audiosDic.Add(sound.clip.name,source);
        }
    }

}
