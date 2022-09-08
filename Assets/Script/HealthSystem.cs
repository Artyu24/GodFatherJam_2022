using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Heart
{
    public int id;
    [SerializeField] public GameObject heartObject;
}
public class HealthSystem : MonoBehaviour
{
    public int currentHealth;
    public int fullHealth;
    

    [SerializeField] private List<Heart> hearts = new List<Heart>();

    void Start()
    {
        currentHealth = fullHealth;
        foreach (Heart heart in hearts)
        {
            Heart Heart = hearts[heart.id - 1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Heart heart in hearts)
        {
            if (currentHealth + 1 == heart.id)
            {
                Destroy(heart.heartObject);
            }

        }
    }
}
