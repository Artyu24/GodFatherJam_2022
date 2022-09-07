using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SliderNote
{
    public int sliderChoose;
    public float valueNote;
    public float ValueNote { get { return valueNote; } set { valueNote = value; } }
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> slidersObject =  new List<GameObject>();
    private float difPartOne = 0, difPartTwo = 0, difPartThree;
    [SerializeField] private float speed = 1;

    [SerializeField] private List<SliderNote> partOne = new List<SliderNote>();
    [SerializeField] private List<SliderNote> partTwo = new List<SliderNote>();
    [SerializeField] private List<SliderNote> partThree = new List<SliderNote>();

    private void Start()
    {
        foreach (SliderNote note in partOne)
        {
            if (note.valueNote > difPartOne)
                difPartOne = note.valueNote;
        }

        foreach (SliderNote note in partTwo)
        {
            if (note.valueNote > difPartTwo)
                difPartTwo = note.valueNote;

            note.ValueNote += difPartOne;
        }

        foreach (SliderNote note in partThree)
            note.ValueNote += difPartOne + difPartTwo;
    }

    private void Update()
    {
        foreach (SliderNote note in partOne)
        {
            note.ValueNote -= speed * Time.deltaTime;
        }
        foreach (SliderNote note in partTwo)
        {
            note.ValueNote -= speed * Time.deltaTime;
        }
        foreach (SliderNote note in partThree)
        {
            note.ValueNote -= speed * Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        difPartOne = 0;
        difPartTwo = 0;
        difPartThree = 0;

        DrawNotes(partOne, 0, ref difPartOne);
        DrawNotes(partTwo, difPartOne, ref difPartTwo);
        DrawNotes(partThree, difPartOne + difPartTwo, ref difPartThree);
    }

    private void DrawNotes(List<SliderNote> listNote, float valToAdd, ref float Diff)
    {
        foreach (SliderNote note in listNote)
        {
            Color color = GetNoteColor(note.sliderChoose);
            if (color != Color.white)
            {
                GameObject slider = slidersObject[note.sliderChoose - 1];
                Slider sliderCompo = slider.GetComponent<Slider>();
                RectTransform sliderRect = slider.GetComponent<RectTransform>();

                float percent = (note.valueNote + valToAdd) / sliderCompo.maxValue;
                if(Application.isPlaying)
                    percent = note.valueNote / sliderCompo.maxValue;

                float dist = sliderRect.rect.width * percent;
                Vector3 dir = Quaternion.AngleAxis(sliderRect.eulerAngles.z, Vector3.forward) * -transform.right;
                Vector3 notePoint = slider.transform.position + dir * dist;

                Gizmos.color = color;
                Gizmos.DrawCube(notePoint, new Vector3(10, 10, 1));

                if (note.valueNote > Diff)
                    Diff = note.valueNote;
            }
        }
    }

    private Color GetNoteColor(int sliderChoose)
    {
        switch (sliderChoose)
        {
            case 1:
                return Color.yellow;
            case 2:
                return Color.blue;
            case 3:
                return Color.green;
            case 4:
                return Color.red;
            default:
                return Color.white;
        }
    }
}
