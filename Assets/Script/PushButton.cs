using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushButton : MonoBehaviour
{
    [SerializeField] private KeyCode leftUp, rightUp, leftDown, rightDown;
    [SerializeField] private int nothing, tooEarly, perfect, tooLate, miss;
    [SerializeField] private Image placeHolderImage;
    [SerializeField] private Sprite tooEarlyImage, perfectImage, tooLateImage, missImage;
    [SerializeField] private Animator drumAnim;
    [SerializeField] private HealthSystem Health;
    [SerializeField] private Animator playerAnim;


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
        playerAnim.SetTrigger("AttackTrigger");


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
            MissEvent();
        }
        else if (noteChoose.ValueNote <= tooEarly && noteChoose.ValueNote > perfect)
        {
            //too early
            placeHolderImage.sprite = tooEarlyImage;
            gm.TooEarlyPoint++;
            StartCoroutine(RemoveText());
        }
        else if (noteChoose.ValueNote <= perfect && noteChoose.ValueNote > tooLate)
        {
            //Perfect
            placeHolderImage.sprite = perfectImage;
            gm.PerfectPoint++;
            StartCoroutine(RemoveText());
        }
        else if (noteChoose.ValueNote <= tooLate && noteChoose.ValueNote > miss)
        {
            //too late
            placeHolderImage.sprite = tooLateImage;
            gm.TooLatePoint++;
            StartCoroutine(RemoveText());
        }
        else
        {
            //miss
            //Perd un coeur
            MissEvent();
        }

        noteChoose.HasDespawn = true;
        Destroy(noteChoose.NoteObject);
        Sliderspawner.DictNote.Remove(noteChoose.IdNote);
    }

    public void MissEvent()
    {
        GameManager gm = GetComponent<GameManager>();
        if (gm != null)
        {
            StopCoroutine(RemoveText());
            Health.currentHealth -= 1;
            placeHolderImage.sprite = missImage;
            gm.MissPoint++;
            StartCoroutine(RemoveText());
        }
    }

    private IEnumerator RemoveText()
    {
        placeHolderImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        placeHolderImage.gameObject.SetActive(false);
    }
}
