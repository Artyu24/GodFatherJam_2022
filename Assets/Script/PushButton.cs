using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushButton : MonoBehaviour
{
    [SerializeField] private int tooEarly, perfect, tooLate, miss;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            FindNotes(1);
        }
        else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FindNotes(2);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            FindNotes(3);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            FindNotes(4);
        }
    }

    private void FindNotes(int sliderID)
    {
        List<SliderNote> noteFound = new List<SliderNote>();

        foreach (SliderNote note in Sliderspawner.DictNote.Values)
        {
            if (note.sliderChoose == sliderID)
            {
                noteFound.Add(note);
            }
        }

        if (noteFound.Count == 0)
        {
            return;
        }

        float lowValue = noteFound[0].ValueNote;
        SliderNote noteChoose = noteFound[0];
        foreach (SliderNote note in noteFound)
        {
            if (lowValue > note.ValueNote)
            {
                noteChoose = note;
                lowValue = note.ValueNote;
            }
        }

        if (noteChoose.ValueNote > tooEarly)
        {
            //miss
        }
        else if (noteChoose.ValueNote <= tooEarly && noteChoose.ValueNote > perfect)
        {
            //too early
        }
        else if (noteChoose.ValueNote <= perfect && noteChoose.ValueNote > tooLate)
        {
            //Perfect
        }
        else if (noteChoose.ValueNote <= tooLate && noteChoose.ValueNote > miss)
        {
            //too late
        }
        else
        {
            //miss
        }

        Destroy(noteChoose.NoteObject);
        noteChoose.HasDespawn = true;
        Sliderspawner.DictNote.Remove(noteChoose.IdNote);
    }
}
