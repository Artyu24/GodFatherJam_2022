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
    
    private float baseValue;
    public float BaseValue { get => baseValue; set => baseValue = value; }
    private string idNote;
    public string IdNote {get => idNote; set => idNote = value; }
    private GameObject noteObject;
    public GameObject NoteObject { get => noteObject; set => noteObject = value; }
    private Vector3 notePosition;
    public Vector3 NotePosition { get => notePosition; set => notePosition = value; }

    private bool hasDespawn = false;
    public bool HasDespawn { get => hasDespawn; set => hasDespawn = value; }
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> slidersObject =  new List<GameObject>();
    private float difPartOne = 0, difPartTwo = 0, difPartThree;
    [SerializeField] private float speed = 1;

    [SerializeField] private List<SliderNote> partOne = new List<SliderNote>();
    [SerializeField] private List<SliderNote> partTwo = new List<SliderNote>();
    [SerializeField] private List<SliderNote> partThree = new List<SliderNote>();

    public List<SliderNote> PartOne { get { return partOne; } }
    public List<SliderNote> PartTwo { get { return partTwo; } }
    public List<SliderNote> PartThree { get { return partThree; } }

    [Header("Gestion des points")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text rankText;
    [SerializeField] private int pointByEarly, pointByLate, pointByPerfect, rankS, rankA, rankB, rankC;
    [SerializeField] private Text missPointText, tooEarlyPointText, tooLatePointText, perfectPointText;
    private int missPoint, tooEarlyPoint, tooLatePoint, perfectPoint;
    public int MissPoint { get => missPoint; set => missPoint = value; }
    public int TooEarlyPoint { get => tooEarlyPoint; set => tooEarlyPoint = value; }
    public int TooLatePoint { get => tooLatePoint; set => tooLatePoint = value; }
    public int PerfectPoint { get => perfectPoint; set => perfectPoint = value; }
    public GameObject VictoryMenu;

    private float delay;

    private void Start()
    {
        Application.targetFrameRate = 60;
        delay = GameSettings.currentDelay;

        foreach (SliderNote note in partOne)
        {
            if (note.ValueNote > difPartOne)
                difPartOne = note.ValueNote;
            note.NotePosition = GetNotePosition(note);
            note.BaseValue = note.ValueNote;
            note.IdNote = note.sliderChoose + "_" + note.ValueNote;
        }

        foreach (SliderNote note in partTwo)
        {
            if (note.ValueNote > difPartTwo)
                difPartTwo = note.ValueNote;

            note.ValueNote += difPartOne;
            note.NotePosition = GetNotePosition(note);
            note.BaseValue = note.ValueNote;
            note.IdNote = note.sliderChoose + "_" + note.ValueNote;
        }

        foreach (SliderNote note in partThree)
        {
            note.ValueNote += difPartOne + difPartTwo;
            note.NotePosition = GetNotePosition(note);
            note.BaseValue = note.ValueNote;
            note.IdNote = note.sliderChoose + "_" + note.ValueNote;
        }
    }

    //ICI FAIRE LE DECALAGE
    private void Update()
    {
        if (delay <= 0)
        {
            if (Pausemenu.Game_Paused == false)
            {
                foreach (SliderNote note in partOne)
                {
                    note.ValueNote -= speed * Time.deltaTime;
                    note.NotePosition = GetNotePosition(note);
                }
                foreach (SliderNote note in partTwo)
                {
                    note.ValueNote -= speed * Time.deltaTime;
                    note.NotePosition = GetNotePosition(note);

                }
                foreach (SliderNote note in partThree)
                {
                    note.ValueNote -= speed * Time.deltaTime;
                    note.NotePosition = GetNotePosition(note);

                }
            }
        }
        else
        {
            delay -= Time.deltaTime * 1000f;
            Debug.Log(delay);
        }
    }

    private Vector3 GetNotePosition(SliderNote note)
    {
        GameObject slider = slidersObject[note.sliderChoose - 1];
        Slider sliderCompo = slider.GetComponent<Slider>();
        RectTransform sliderRect = slider.GetComponent<RectTransform>();

        float percent = note.ValueNote / sliderCompo.maxValue;
        if (Application.isPlaying)
            percent = note.ValueNote / sliderCompo.maxValue;

        float dist = sliderRect.rect.width * percent;
        Vector3 dir = Quaternion.AngleAxis(sliderRect.eulerAngles.z, Vector3.forward) * -transform.right;
        return slider.transform.position + dir * dist;
    }

    public void OnVictory()
    {
        VictoryMenu.SetActive(true);
        missPointText.text = missPoint.ToString();
        tooEarlyPointText.text = tooEarlyPoint.ToString();
        tooLatePointText.text = tooLatePoint.ToString();
        perfectPointText.text = perfectPoint.ToString();

        int score = tooEarlyPoint * pointByEarly + tooLatePoint * pointByLate + perfectPoint * pointByPerfect;
        scoreText.text = score.ToString();

        if (score > rankS)
            rankText.text = "S";
        else if(score > rankA)
            rankText.text = "A";
        else if (score > rankB)
            rankText.text = "B";
        else if (score > rankC)
            rankText.text = "C";
        else
            rankText.text = "D";
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
                //GameObject slider = slidersObject[0];
                GameObject slider = slidersObject[note.sliderChoose - 1];
                Slider sliderCompo = slider.GetComponent<Slider>();
                RectTransform sliderRect = slider.GetComponent<RectTransform>();

                float percent = (note.ValueNote + valToAdd) / sliderCompo.maxValue;
                if(Application.isPlaying)
                    percent = note.ValueNote / sliderCompo.maxValue;

                float dist = sliderRect.rect.width * percent;
                Vector3 dir = Quaternion.AngleAxis(sliderRect.eulerAngles.z, Vector3.forward) * -transform.right;
                Vector3 notePoint = slider.transform.position + dir * dist;

                Gizmos.color = color;
                Gizmos.DrawCube(notePoint, new Vector3(10, 10, 1));

                if (note.ValueNote > Diff)
                    Diff = note.ValueNote;
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
