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

    private bool canTouch;
    private bool isMoving;



    public List<Transform> planets = new List<Transform>(); // Gezegenleri saklamak için bir liste kullanýyoruz
    [SerializeField]private int currentPlanetIndex = 0;

    [SerializeField] private float movementSpeed = 2f; // Hareket hýzý
    [SerializeField] private float stoppingDistance = 0.1f;


    [SerializeField] private float cameraLerpSpeed = 0.5f; // Kamera geçiþ hýzý

    private Vector3 targetCameraPosition; // Hedef kamera pozisyonu
    private bool moveCamera;


    public GameObject planetPrefab;
    [SerializeField] private float ySpacing = 3f;



    // Start is called before the first frame update
    void Start()
    {
        canTouch = true;
        moveCamera = false;
        isMoving= false;
        hasShield = true;
        isOnPlanet = true;
        timeOnPlanet = 0f;

        transform.rotation = Quaternion.Euler(0f, 0f, -45f);

        targetCameraPosition = Camera.main.transform.position;


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


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (canTouch) {
                MoveToNextPlanet();
                }
            }
        }
        if (isMoving)
        {
            canTouch = false;
            // Hedef gezegenin pozisyonuna doðru hareket etmek için bir vektör hesaplayýn
            Vector3 targetPosition = planets[currentPlanetIndex].position;
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            Vector3 movement = moveDirection * movementSpeed * Time.deltaTime;

            // Gemiyi yeni konuma hareket ettirin
            transform.position += movement;
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            if (distanceToTarget <= stoppingDistance)
            {
                canTouch = true;
                //moveCamera = false;
                transform.position = targetPosition;
                if (currentPlanetIndex % 2 == 0)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, -45f);

                }
                else
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 45f);

                }
            }
        }
        if (moveCamera)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetCameraPosition, cameraLerpSpeed * Time.deltaTime);
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
    private void MoveToNextPlanet()
    {
        // Bir sonraki gezegene hareket etmek için indeksi bir artýrýn
        currentPlanetIndex++;
       
        // Gezegenler listesinin sonuna ulaþtýðýmýzda, yeni bir gezegen spawn edebilirsiniz
        if (currentPlanetIndex >= planets.Count-2)
        {
            SpawnNewPlanet();
            if(currentPlanetIndex > 2) 
            { 
                planets[currentPlanetIndex-2].gameObject.SetActive(false);
                planets[currentPlanetIndex -3].gameObject.SetActive(false);

            }
        }

        isMoving = true;
       
        targetCameraPosition.y += 3f;
        moveCamera = true;
        
            
       

    }


    private void SpawnNewPlanet()
    {
        Debug.Log("spawn");
        Vector3 spawnRightPosition = planets[currentPlanetIndex].transform.position;
        Vector3 spawnLeftPosition = planets[currentPlanetIndex+1].transform.position;
        spawnLeftPosition.y += ySpacing*2;
        spawnRightPosition.y += ySpacing*2;
        GameObject newLeftPlanet = Instantiate(planetPrefab, spawnLeftPosition, Quaternion.identity);
        GameObject newRightPlanet = Instantiate(planetPrefab, spawnRightPosition, Quaternion.identity);
        planets.Add(newRightPlanet.transform);
        planets.Add(newLeftPlanet.transform);
        
    }

}
