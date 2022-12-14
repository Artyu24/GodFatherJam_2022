using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicHandler : MonoBehaviour
{
    public float bpm;

    [SerializeField] GameObject slider; 
    public AudioClip clip;
    public AudioSource source;
    float clipDuration;
    float fourBars;
    public float physicalLength = 0;


    private void Update()
    {

    }

    private void OnDrawGizmos()
    {
        clipDuration = clip.length / 60f;

        fourBars = (bpm / clipDuration) / 2f;

        physicalLength = 0f;

        Slider sliderCompo = slider.GetComponent<Slider>();
        RectTransform sliderRect = slider.GetComponent<RectTransform>();

        for (int i = 0; i < bpm; i++)
        {

            Vector3 dir = Quaternion.AngleAxis(sliderRect.eulerAngles.z, Vector3.forward) * -transform.right;
            Vector3 breakPoint = slider.transform.position + dir * (i * fourBars * 4f);

            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(breakPoint, new Vector3(10, 10, 1));

            physicalLength += fourBars;
        }
    }

}