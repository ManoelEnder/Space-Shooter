using UnityEngine;
using System.Collections;

public class AsteroidRainSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 0.4f;
    public float minSize = 0.6f;
    public float maxSize = 1.4f;
    public float minSpeed = 3f;
    public float maxSpeed = 7f;

    Camera cam;
    float camWidth;
    float camHeight;

    void Start()
    {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2f;
        camWidth = camHeight * cam.aspect;

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnAsteroid();
        }
    }

    void SpawnAsteroid()
    {
        float x = Random.Range(-camWidth / 2f, camWidth / 2f);

        Vector3 spawnPos = new Vector3(x, cam.transform.position.y + camHeight / 2f + 1f, 0f);

        GameObject a = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

        Asteroid asteroid = a.GetComponent<Asteroid>();

        Vector2 dir = Vector2.down;
        float size = Random.Range(minSize, maxSize);
        float speed = Random.Range(minSpeed, maxSpeed);

        asteroid.Initialize(dir, size, speed);

        a.tag = "Enemy";
    }
}
