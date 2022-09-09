using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    public Sound[] sounds;
    AudioSource audioSource;
    public GameManager gameManager;

    void Awake()
    {
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.duration = s.source.clip.length;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {

            Play("Menu");

        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SampleScene"))
        {

            Stop("Menu");
            Play("Rock");
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Musique_01"))
        {
            Play("Game_01");
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found mate!");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {

        Sound s = Array.Find(sounds,(sound)=>sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found mate!");
            return;
        }
        s.source.Stop();
    }


    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Musique_01"))
        {
            foreach(Sound s in sounds)
            {
                if(s.name == "Game_01")
                {
                   if(!s.source.isPlaying && Pausemenu.Game_Paused == false)
                    {
                        gameManager.OnVictory();
                        Debug.LogWarning("Victory");
                    }
                }
            }
        }
    }
}
