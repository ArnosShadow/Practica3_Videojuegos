using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    // Quiero hacer que el script valga tanto para spawnear zombies en zonas concretas del mapa.
    // Como para que lo hagan en cualquier sitio.
    [SerializeField] bool randomSpawn = false;


    // Caso de usar spawnPoints.
    [SerializeField] private Transform[] spawnPoints;

    // Caso de usar el mapa completo.
    [SerializeField] private Vector3[] spawnBounds = new Vector3[2]; // Define las esquinas del mapa donde pueden spawnear.


    [SerializeField] public GameObject enemyPrefab; // Prefab del zombie a instanciar

    [SerializeField] public float spawnInterval = 5f; // Tiempo entre spawns

    private float spawnTimer;

    void Start()
    {

        if (!randomSpawn)
        {
            GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");
            spawnPoints = new Transform[spawnPointObjects.Length];

            for (int i = 0; i < spawnPointObjects.Length; i++)
            {
                spawnPoints[i] = spawnPointObjects[i].transform;
            }
        }

    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = GetSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetSpawnPosition()
    {
        if (!randomSpawn)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            return spawnPoints[randomIndex].position;
        }
        else
        {
            Vector3 min = spawnBounds[0];
            Vector3 max = spawnBounds[1];

            float x = Random.Range(min.x, max.x);
            float y = Random.Range(min.y, max.y);
            float z = Random.Range(min.z, max.z);

            return new Vector3(x, y, z);
        }
    }
}