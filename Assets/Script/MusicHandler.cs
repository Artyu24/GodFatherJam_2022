using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicHandler : MonoBehaviour
{
    public float bpm;
    public float bps;

    [SerializeField] GameObject slider; 
    public float value;
    public AudioClip clip;
    float clipDuration;
    float fourBars;

    private void OnDrawGizmos()
    {
        clipDuration = clip.length/60f;

        fourBars = (bpm / clipDuration) * 4f;

        for (int i = 0; i < bpm; i++)
        {
            Vector3 breakPoint = new Vector3(0f, i * fourBars, 0f);
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(breakPoint, new Vector3(5, 5, 1));
        }

        //    Slider sliderCompo = slider.GetComponent<Slider>();
        //    RectTransform sliderRect = slider.GetComponent<RectTransform>();
        //    float percent = value / sliderCompo.maxValue;

        //    float dist = sliderRect.rect.width * percent;

        //    Vector3 dir = Quaternion.AngleAxis(sliderRect.eulerAngles.z, Vector3.forward) * -transform.right;
        //    Vector3 beatPoint = slider.transform.position + dir * dist;

        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawCube(beatPoint, new Vector3(10, 10, 1));
        //}
    }
}