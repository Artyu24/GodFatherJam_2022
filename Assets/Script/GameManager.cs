using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> slidersObject = new List<GameObject>();

    [System.Serializable]
    struct SliderNote
    {
        public int sliderChoose;
        public int value;
    }

    [SerializeField] private List<SliderNote> partOne = new List<SliderNote>();
    [SerializeField] private List<SliderNote> partTwo = new List<SliderNote>();
    [SerializeField] private List<SliderNote> partThree = new List<SliderNote>();

    private void OnDrawGizmos()
    {
        foreach (SliderNote note in partOne)
        {
            GameObject slider = slidersObject[note.sliderChoose - 1];
            Slider sliderCompo = slider.GetComponent<Slider>();
            RectTransform sliderRect = slider.GetComponent<RectTransform>();

            float percent = note.value / sliderCompo.maxValue;
            float dist = sliderRect.rect.width * percent;
            Vector3 dir = Quaternion.AngleAxis(sliderRect.eulerAngles.z, Vector3.forward) * -transform.right;
            Vector3 notePoint = slider.transform.position + dir * dist;

            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(notePoint, new Vector3(10, 10, 1));
        }
    }
}
