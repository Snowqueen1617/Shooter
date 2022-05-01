using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]

    public class NamedSound
    {
        public AudioClip clip;
        public string name;
    }

    public NamedSound[] sounds;

    public void PlaySoundByName(string name)
    {
        // Optimize this so it uses a dictionary instead of searching through a list of strings
        foreach (NamedSound sound in sounds)
        {
            if (sound.name == name)
            {
                AudioSource.PlayClipAtPoint(sound.clip, Vector3.zero);
            }
        }
        
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
