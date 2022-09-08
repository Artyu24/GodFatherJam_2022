using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushButton : MonoBehaviour
{
    [SerializeField] private KeyCode leftUp, rightUp, leftDown, rightDown;
    [SerializeField] private int nothing, tooEarly, perfect, tooLate, miss;
    [SerializeField] private Image placeHolderImage;
    [SerializeField] private Sprite nothingImage, tooEarlyImage, perfectImage, tooLateImage, missImage;
    [SerializeField] private Animator drumAnim;
    [SerializeField] private HealthSystem Health;
    
    private void Update()
    {
        if (Input.GetKeyDown(leftUp) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            FindNotes(1);
        }
        else if (Input.GetKeyDown(rightUp) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FindNotes(2);
        }
        else if (Input.GetKeyDown(leftDown) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            FindNotes(3);
        }
        else if (Input.GetKeyDown(rightDown) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            FindNotes(4);
        }
    }

    public void FindNotes(int sliderID)
    {
        List<SliderNote> noteFound = new List<SliderNote>();

        foreach (SliderNote note in Sliderspawner.DictNote.Values)
        {
            if (note.sliderChoose == sliderID)
            {
                noteFound.Add(note);
            }
        }

        drumAnim.SetTrigger("drum");

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

        if (noteChoose.valueNote > nothing)
        {
            return;
        }

        GameManager gm = GetComponent<GameManager>();
        StopCoroutine(RemoveText());

        if (noteChoose.ValueNote <= nothing && noteChoose.ValueNote > tooEarly)
        {
            //miss
            //Perd un coeur
            Health.currentHealth -= 1;
            placeHolderImage.sprite = missImage;
            gm.MissPoint++;
        }
        else if (noteChoose.ValueNote <= tooEarly && noteChoose.ValueNote > perfect)
        {
            //too early
            placeHolderImage.sprite = tooEarlyImage;
            gm.TooEarlyPoint++;
        }
        else if (noteChoose.ValueNote <= perfect && noteChoose.ValueNote > tooLate)
        {
            //Perfect
            placeHolderImage.sprite = perfectImage;
            gm.PerfectPoint++;
        }
        else if (noteChoose.ValueNote <= tooLate && noteChoose.ValueNote > miss)
        {
            //too late
            placeHolderImage.sprite = tooLateImage;
            gm.TooLatePoint++;
        }
        else
        {
            //miss
            //Perd un coeur
            Health.currentHealth -= 1;
            placeHolderImage.sprite = missImage;
            gm.MissPoint++;
        }

        StartCoroutine(RemoveText());
        Destroy(noteChoose.NoteObject);
        noteChoose.HasDespawn = true;
        Sliderspawner.DictNote.Remove(noteChoose.IdNote);
    }

    private IEnumerator RemoveText()
    {
        //fbText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        //fbText.gameObject.SetActive(false);
    }
}
