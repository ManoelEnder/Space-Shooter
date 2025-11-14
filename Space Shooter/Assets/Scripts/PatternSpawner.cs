using System.Collections;
using UnityEngine;

public class PatternSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 2f;
    public int wavesPerBurst = 3;
    public float burstInterval = 0.6f;
    public int bulletsPerWave = 7;
    public float waveAngleSpread = 60f;
    public float childSize = 0.6f;
    public float spawnRadius = 0.5f;
    public bool spawnFromMotherPosition = true;
    public float projectileSpeed = 6f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            for (int b = 0; b < wavesPerBurst; b++)
            {
                SpawnWave();
                yield return new WaitForSeconds(burstInterval);
            }
        }
    }

    void SpawnWave()
    {
        if (asteroidPrefab == null) return;

        Vector3 center = spawnFromMotherPosition
            ? transform.position
            : (GameObject.FindGameObjectWithTag("Player")?.transform.position ?? transform.position);

        float half = waveAngleSpread * 0.5f;
        float step = bulletsPerWave > 1 ? waveAngleSpread / (bulletsPerWave - 1) : 0f;
        float startAngle = -half;

        for (int i = 0; i < bulletsPerWave; i++)
        {
            float angleDeg = startAngle + step * i;
            float angleRad = angleDeg * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;
            Vector3 spawnPos = center - (Vector3)dir * spawnRadius;

            GameObject go = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
            Asteroid a = go.GetComponent<Asteroid>();
            if (a != null)
            {
                a.Initialize(dir, childSize, projectileSpeed);
            }

            go.tag = "Enemy";
        }
    }
}
