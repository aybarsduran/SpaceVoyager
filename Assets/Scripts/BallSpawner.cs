using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject footballBallPrefab;
    [SerializeField] private GameObject basketballBallPrefab;
    public float spawnInterval = 1f;
    private List<GameObject> cloneObjects = new List<GameObject>();

    private float timer;

    private void Start()
    {
        // Start spawning balls immediately
        timer = spawnInterval;
    }

    private void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;

        // Check if it's time to spawn a ball
        if (timer <= 0f)
        {
            // Reset the timer
            timer = spawnInterval;

            // Spawn a ball
            SpawnBall();
        }
        if (GameManager.Instance.isGameOver)
        {
            DestroyCloneObjects();
        }
    }

    private void SpawnBall()
    {
        if (!GameManager.Instance.isGameOver)
        {
            GameObject selectedBallPrefab = (Random.Range(0, 2) == 0) ? footballBallPrefab : basketballBallPrefab;
            // Instantiate the ball prefab at the spawner's position
            GameObject ball = Instantiate(selectedBallPrefab, transform.position, Quaternion.identity);
            cloneObjects.Add(ball);

        }



    }
    private void DestroyCloneObjects()
    {
        foreach (GameObject clone in cloneObjects)
        {
            Destroy(clone);
        }

        cloneObjects.Clear();
    }
}
