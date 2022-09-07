using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {

            FindObjectOfType<SoundManager>().Play("Menu");

        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SampleScene"))
        {
            FindObjectOfType<SoundManager>().Play("rock");
        }
    
}

}
