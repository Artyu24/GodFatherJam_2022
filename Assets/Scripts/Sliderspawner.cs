using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliderspawner : MonoBehaviour
{

    [SerializeField]
    public GameObject TopRightSlider;
    [SerializeField]
    public GameObject BottomRightSlider;
    [SerializeField]
    public GameObject TopLeftSlider;
    [SerializeField]
    public GameObject BottomLeftSlider;

    public bool existingTopRight = false;
    public bool existingBottomRight = false;
    public bool existingTopLeft = false ;
    public bool existingBottomLeft = false;

    private GameObject topleftslider;
    private GameObject bottomleftslider;
    private GameObject toprightslider;
    private GameObject bottomrightslider;





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (SliderNote note in GetComponent<GameManager>().PartOne)
        {
            if (note.valueNote <= 300)
            {
                switch (note.sliderChoose)
                {
                    case 1:
                        if (existingTopLeft == false)
                        {
                            topleftslider = Instantiate(TopLeftSlider);
                            existingTopLeft = true;
                            if (note.valueNote < 10)
                            {
                                GameObject.Destroy(topleftslider);
                                Debug.LogWarning("Destroyed");
                                existingTopLeft = false;
                            }


                        }
                        break;
                    case 2:
                        if (existingBottomLeft == false)
                        {
                            bottomleftslider = Instantiate(BottomLeftSlider);
                            existingBottomLeft = true;
                            if(note.valueNote < 10)
                            {
                                GameObject.Destroy(bottomleftslider);
                                Debug.LogWarning("Destroyed");
                                existingBottomLeft = false;
                            }
                        }
                        break;
                    case 3:
                        if(existingTopRight == false)
                        {
                            toprightslider = Instantiate(TopRightSlider);
                            existingTopRight = true;
                            if (note.valueNote < 10)
                            {
                                GameObject.Destroy(toprightslider);
                                Debug.LogWarning("Destroyed");
                                existingTopRight = false;
                            }
                        }
                        break;
                    case 4:
                        if(existingBottomRight == false)
                        {
                            bottomrightslider = Instantiate(BottomRightSlider);
                            existingBottomRight=true;
                            if(note.valueNote < 10)
                            {
                                GameObject.Destroy(bottomrightslider);
                                Debug.LogWarning("Destroyed");
                                existingBottomRight = false;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
