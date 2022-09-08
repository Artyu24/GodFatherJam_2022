using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Sliderspawner : MonoBehaviour
{

    [SerializeField]
    public GameObject topRightNote;
    [SerializeField]
    public GameObject bottomRightNote;
    [SerializeField]
    public GameObject topLeftNote;
    [SerializeField]
    public GameObject bottomLeftNote;


    [SerializeField]
    public GameObject topRightSliderSpawn;
    [SerializeField]
    public GameObject bottomLeftSliderSpawn;
    [SerializeField]
    public GameObject topLeftSliderSpawn;
    [SerializeField]
    public GameObject bottomRightSliderSpawn;

    public bool existingTopRight = false;
    public bool existingBottomRight = false;
    public bool existingTopLeft = false ;
    public bool existingBottomLeft = false;

    public GameObject canvas;



    private static Dictionary<string, SliderNote> dictNote = new Dictionary<string, SliderNote> ();
    public static Dictionary<string, SliderNote> DictNote => dictNote;

    void Update()
    {
        if(Pausemenu.Game_Paused == false)
        {
            foreach (SliderNote note in GetComponent<GameManager>().PartOne)
            {
                SpawnSlider(note);
            }
            foreach (SliderNote note in GetComponent<GameManager>().PartTwo)
            {
                SpawnSlider(note);
            }
            foreach (SliderNote note in GetComponent<GameManager>().PartThree)
            {
                SpawnSlider(note);
            }

            for (int i = 0; i < dictNote.Count;) 
            {
                var patate = dictNote.ElementAt(i);
                SliderNote note = patate.Value;
                if(note.valueNote <= -30f)
                {
                    Destroy(note.NoteObject);
                    dictNote.Remove(note.IdNote);
                    note.HasDespawn = true;
                    GetComponent<PushButton>().MissEvent();
                }
                else
                {
                    note.NoteObject.transform.position = note.NotePosition;
                    i++;
                }
            }
        }

    }

    private void SpawnSlider(SliderNote note)
    {
        if (note.valueNote <= 300)
        {
            GameObject slider = null;
            if (!dictNote.ContainsKey(note.IdNote) && !note.HasDespawn)
            {
                switch (note.sliderChoose)
                {
                    case 1:
                        slider = Instantiate(topLeftNote,canvas.transform );
                        break;
                    case 2:
                        slider = Instantiate(bottomLeftNote, canvas.transform);
                        break;
                    case 3:
                        slider = Instantiate(topRightNote, canvas.transform);
                        break;
                    case 4:

                        slider = Instantiate(bottomRightNote, canvas.transform);
                        break;
                    default:
                        break;
                }

            }

            if (slider != null)
            {
                note.NoteObject = slider;
                dictNote.Add(note.IdNote, note);
            }
        }
    }
}
