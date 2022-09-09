using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayFunction : MonoBehaviour
{
    public Text DelayTextUI;
    private float currentDelay;
    // Start is called before the first frame update
    void Start()
    {
        currentDelay = GameSettings.currentDelay;
    }
    

    public void DelayLevel(float delayLevel)
    {
        currentDelay = delayLevel;
        GameSettings.currentDelay = currentDelay;
        DelayTextUI.text = delayLevel.ToString("0") + "ms";
    }
}
