using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFadeIn : MonoBehaviour
{
    public AudioSource bgm;
    float fadeTime = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        bgm.loop = true;
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(bgm.volume < 0.8)
        {
            bgm.volume += fadeTime;
        }
        
    }
}
