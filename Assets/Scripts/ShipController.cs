using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private bool hasShield;
    private bool isOnPlanet;
    [SerializeField]private float timeOnPlanet;
    public float sightDuration = 5f;
    public float destructionDuration = 10f;
    public GameObject shield;
    public GameObject sightObject;




    public GameObject planetPrefab;
    public float planetSpacing = 2f; // The distance between each planet
    public float firstPlanetX = -8f; // The X position of the first planet
    public float firstPlanetY = -4f; // The Y position of the first planet
    public float secondPlanetX = 8f; // The X position of the second planet
    public float secondPlanetY = 0f; // The Y position of the second planet
    public float thirdPlanetX = -8f; // The X position of the third planet
    public float thirdPlanetY = 4f; // The Y position of the third planet
    public int initialPlanetCount = 3; // The initial number of planets to spawn
    public int planetsPerSpawn = 3; // The number of planets to spawn when reaching the middle one


    // Start is called before the first frame update
    void Start()
    {
        hasShield = true;
        isOnPlanet = false;
        timeOnPlanet = 0f;



        SpawnPlanet(new Vector3(firstPlanetX, firstPlanetY, 0f));

        // Spawn the second planet
        SpawnPlanet(new Vector3(secondPlanetX, secondPlanetY, 0f));

        // Spawn the third planet
        SpawnPlanet(new Vector3(thirdPlanetX, thirdPlanetY, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (hasShield) 
        {
            shield.SetActive(true);
        }
        else { shield.SetActive(false); }

        if (isOnPlanet)
        {
            timeOnPlanet += Time.deltaTime;

            if (timeOnPlanet >= sightDuration)
            {
                ShowSight();
                
            }

            if (timeOnPlanet >= destructionDuration)
            {
                GameOver();
            }
        }



    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            isOnPlanet= true;
        }
        else if(collision.gameObject.CompareTag("Rock"))
         {
             if (hasShield)
             {
                hasShield = false;
             }
             else
             {
                GameOver();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            isOnPlanet = false;
            HideSight();
            timeOnPlanet = 0f;

            if (collision.gameObject.transform.position.x == secondPlanetX && collision.gameObject.transform.position.y == secondPlanetY)
            {
                for (int i = 0; i < planetsPerSpawn; i++)
                {
                    float offsetX = (initialPlanetCount + i) * planetSpacing;
                    SpawnPlanet(new Vector3(secondPlanetX + offsetX, secondPlanetY, 0f));
                }
            }

        }

    }
    private void ShowSight()
    {
        sightObject.SetActive(true);
    }

    private void HideSight()
    {
        sightObject.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("gameover");
    }
    private void SpawnPlanet(Vector3 position)
    {
        GameObject planet = Instantiate(planetPrefab, position, Quaternion.identity);
        // Set up any additional properties or behaviors of the planet
        // ...
    }

}
