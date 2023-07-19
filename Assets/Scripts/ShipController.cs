using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipController : MonoBehaviour
{
    private bool hasShield;
    [SerializeField]private bool isOnPlanet;
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

    [SerializeField]
    private Sprite[] planetSprites;

    [SerializeField]
    private Sprite[] rockSprites;

    private int score;
    public TextMeshProUGUI scoreText;

    public GameObject main;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverPanelScore;
    public TextMeshProUGUI gameOverPanelMaxScore;

    public Sprite[] selectedShipSprites;
    public SpriteRenderer shipSprite;

    public GameObject pauseButton;
    public GameObject explosion;

    private void Awake()
    {
        shipSprite.sprite = selectedShipSprites[PlayerPrefs.GetInt("selectedShip",0)];
    }
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        score = 0;
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
        scoreText.text = score.ToString();
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
                if (hasShield)
                {
                    hasShield = false;
                    HideSight();
                    timeOnPlanet = 0;
                }
                else
                {
                    GameOver();
                }
                
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
            score++;
            Debug.Log(score);
            
        }
        else if (collision.gameObject.CompareTag("starterPlanet"))
        {
            isOnPlanet = true;

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
        if (collision.gameObject.CompareTag("starterPlanet"))
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
        if (score > PlayerPrefs.GetInt("MaxScore"))
        {
            // Update the maximum score
            PlayerPrefs.SetInt("MaxScore", score);
        }

        StartCoroutine(ShowExplosionAndActivatePanel());
    }

    private IEnumerator ShowExplosionAndActivatePanel()
    {
        explosion.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        scoreText.transform.parent.gameObject.SetActive(false);
        foreach (Transform planet in planets)
        {
            planet.gameObject.SetActive(false);
        }
        main.SetActive(false);
        pauseButton.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOverPanelScore.text = "score " + score.ToString();
        gameOverPanelMaxScore.text = "record " +  GameManager.Instance.GetMaxScore().ToString();
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

        Sprite randomPlanetSprite = planetSprites[Random.Range(0, planetSprites.Length)];
        Sprite randomRockSprite = rockSprites[Random.Range(0,rockSprites.Length)];


        Sprite randomSecPlanetSprite = planetSprites[Random.Range(0, planetSprites.Length)];
        Sprite randomSecRockSprite = rockSprites[Random.Range(0, rockSprites.Length)];


        Vector3 spawnRightPosition = planets[currentPlanetIndex].transform.position;
        Vector3 spawnLeftPosition = planets[currentPlanetIndex+1].transform.position;

        spawnLeftPosition.y += ySpacing*2;
        spawnRightPosition.y += ySpacing*2;

        planetPrefab.GetComponent<SpriteRenderer>().sprite = randomPlanetSprite;
        planetPrefab.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = randomRockSprite;
        GameObject newLeftPlanet = Instantiate(planetPrefab, spawnLeftPosition, Quaternion.identity);

        planetPrefab.GetComponent<SpriteRenderer>().sprite = randomSecPlanetSprite;
        planetPrefab.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = randomSecRockSprite;
        GameObject newRightPlanet = Instantiate(planetPrefab, spawnRightPosition, Quaternion.identity);

        planets.Add(newRightPlanet.transform);
        planets.Add(newLeftPlanet.transform);
        
    }

}
