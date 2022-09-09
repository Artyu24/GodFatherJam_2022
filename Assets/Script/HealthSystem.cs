using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Heart
{
    public int id;
    [SerializeField] public GameObject heartObject;
    [SerializeField] public GameObject iceHeartObject;
}
[RequireComponent(typeof(ParticleSystem))]
public class HealthSystem : MonoBehaviour
{
    public int currentHealth;
    public int fullHealth;
    

    [SerializeField] private List<Heart> hearts = new List<Heart>();
    private ParticleSystem snowParticle;
    void Start()
    {
        currentHealth = fullHealth;
        foreach (Heart heart in hearts)
        {
            Heart Heart = hearts[heart.id - 1];
            heart.iceHeartObject.SetActive(false);
        }
        snowParticle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var emission = snowParticle.emission;
        var defaultParticle = snowParticle.main;
        //var emission = snowParticle.emission;
        foreach (Heart heart in hearts)
        {
            if (currentHealth + 1 == heart.id)
            {
                Destroy(heart.heartObject);
                heart.iceHeartObject.SetActive(true);
            }
        }

        switch (currentHealth)
        {
            case 4:
                emission.rateOverTime = 300f;
                defaultParticle.simulationSpeed = 1.5f;
                //snowParticle = ;
                break;
            case 3:
                emission.rateOverTime = 500f;
                defaultParticle.simulationSpeed = 2.0f;
                break;
            case 2:
                emission.rateOverTime = 700f;
                defaultParticle.simulationSpeed = 2.5f;
                break;
            case 1:
                emission.rateOverTime = 900f;
                defaultParticle.simulationSpeed = 3.0f;
                break;
            case 0:
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                break;
        }
    }
}   

