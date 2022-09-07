using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> slidersObject =  new List<GameObject>();

    [System.Serializable]
    struct SliderNote
    {
        public int sliderChoose;
        public int value;
    }

    [SerializeField] private List<SliderNote> partOne = new List<SliderNote>();
    [SerializeField] private List<SliderNote> partTwo = new List<SliderNote>();
    [SerializeField] private List<SliderNote> partThree = new List<SliderNote>();

    public int difPartOne = 0, difPartTwo = 0, diffPartThree;

    private void OnDrawGizmos()
    {
        difPartOne = 0;
        difPartTwo = 0;
        diffPartThree = 0;

        DrawNotes(partOne, 0, ref difPartOne);
        DrawNotes(partTwo, difPartOne, ref difPartTwo);
        DrawNotes(partThree, difPartOne + difPartTwo, ref diffPartThree);
    }

    private void DrawNotes(List<SliderNote> listNote, int valToAdd, ref int Diff)
    {
        foreach (SliderNote note in listNote)
        {
            Color color = GetNoteColor(note.sliderChoose);
            if (color != Color.white)
            {
                GameObject slider = slidersObject[note.sliderChoose - 1];
                Slider sliderCompo = slider.GetComponent<Slider>();
                RectTransform sliderRect = slider.GetComponent<RectTransform>();

                float percent = (note.value + valToAdd) / sliderCompo.maxValue;
                float dist = sliderRect.rect.width * percent;
                Vector3 dir = Quaternion.AngleAxis(sliderRect.eulerAngles.z, Vector3.forward) * -transform.right;
                Vector3 notePoint = slider.transform.position + dir * dist;

                Gizmos.color = color;
                Gizmos.DrawCube(notePoint, new Vector3(10, 10, 1));

                if (note.value > Diff)
                    Diff = note.value;
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
