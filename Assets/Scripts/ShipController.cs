using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private bool hasShield;
    private bool isOnPlanet;
    private float timeOnPlanet;
    public float sightDuration = 5f;
    public float destructionDuration = 10f;
    public GameObject shield;
    public GameObject sightObject;

    // Start is called before the first frame update
    void Start()
    {
        hasShield = true;
        isOnPlanet = true;
        timeOnPlanet = 0f;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            isOnPlanet= true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            isOnPlanet = false;
            HideSight();
            timeOnPlanet = 0f;
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

    }
}
