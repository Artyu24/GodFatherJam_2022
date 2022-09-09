using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Pausemenu : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool Game_Paused;
    public GameObject Pause_Menu_UI;
    public Text countdown;
    public GameObject countdown_text;
    public SoundManager music;

    public Text DelayTextUI;
    public static float currentDelay;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log(Game_Paused);
            if (Game_Paused)
            {
                StartCoroutine(Countdown());

            }
            else
            {
                Pause();
            }
        }


    }


    public void Resume()
    {
        Pause_Menu_UI.SetActive(false);
        Game_Paused = false;
        
        music.sounds[2].source.Play();
    }

    void Pause()
    {
        Pause_Menu_UI.SetActive(true);
        Game_Paused = true;

        music.sounds[2].source.Pause();
        //foreach (Sound s in music.sounds)
        //{
        //    s.source.Pause();
        //}
    }

    public void DelayLevel(float delayLevel)
    {
        currentDelay = delayLevel;
        DelayTextUI.text = delayLevel.ToString("0.00") + "ms";
    }

    IEnumerator Countdown()
    {
        Pause_Menu_UI.SetActive(false);
        countdown_text.SetActive(true);
        countdown.text = "3";
        yield return new WaitForSeconds(1);
        countdown.text = "2";
        yield return new WaitForSeconds(1);
        countdown.text = "1";
        yield return new WaitForSeconds(1);
        countdown_text.SetActive(false);
        Resume();
    }


}
