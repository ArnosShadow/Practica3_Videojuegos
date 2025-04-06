using System;
using System.Security.Cryptography;
using Unity.AI.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapaManager : MonoBehaviour
{
    [Header("Configuración de mapa")]
    [SerializeField] private Contenedor contenedor;
    [Header("Propiedades inciales")]
    [SerializeField] private int alto = 20;
    [SerializeField] private int ancho = 20;
    [Header("Bloques")]
    [SerializeField] private GameObject sueloPrefab;
    [SerializeField] private GameObject paredPrefab;
    [SerializeField] private GameObject[] trampasPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject salidaPrefab;
    [SerializeField] private GameObject entradaPrefab;
    /*
    [SerializeField] private GameObject movingWall;
    [SerializeField] private GameObject sueloInteractivo;
    [SerializeField] private GameObject tileTrap1;
    */
    [Header("Navegador")]
    [SerializeField] private NavMeshSurface navMeshSurface;

    private GameObject trampa;
    private GameObject[][] mapa;
    private void Awake()
    {
        mapa = new GameObject[ancho][];
        for (int x = 0; x < ancho; x++)
            mapa[x] = new GameObject[alto];

        alto = contenedor.alto;
        ancho= contenedor.ancho;
    }
    public MapaManager(GameObject[][] mapa)
    {
        this.mapa = mapa;
    }

    void Start() {
        GenerarMapa();
        GenerarParedes();
        GenerarPasillo();
        GenerarZonasSecundarias();
        GenerarTrampasYEnemigos();
        InstanciarMapa();
        navMeshSurface.BuildNavMesh();
    }
    private void GenerarTrampasYEnemigos()
    {
        Vector2Int entrada = new Vector2Int(1, 1);
        Vector2Int salida = new Vector2Int(ancho - 2, alto - 2);

        for (int x = 1; x < ancho - 1; x++)
        {
            for (int y = 1; y < alto - 1; y++)
            {
                Vector2Int actual = new Vector2Int(x, y);

                if (Vector2Int.Distance(actual, entrada) < 3f || Vector2Int.Distance(actual, salida) < 3f)
                    continue;

                if (mapa[x][y] != sueloPrefab)
                    continue;

                if (Random.value < 0.05f && trampasPrefab.Length > 0 && mapa[x][y] !=trampa && mapa[x][y] != enemyPrefab)
                {
                    trampa = trampasPrefab[Random.Range(0, trampasPrefab.Length)];
                    Instantiate(trampa, new Vector3(x * 12, 0, y * 12), Quaternion.identity);
                    mapa[x][y] = trampa;
                    continue; 
                }

                if (Random.value < 0.05f && mapa[x][y] != trampa && mapa[x][y] != enemyPrefab)
                {
                    Instantiate(enemyPrefab, new Vector3(x * 12, 0, y * 12), Quaternion.identity);
                    mapa[x][y] = enemyPrefab;
                }
            }
        }
    }


    private void GenerarZonasSecundarias()
    {
        for (int x = 1; x < ancho - 1; x++)
        {
            for (int y = 1; y < alto - 1; y++)
            {
                if (mapa[x][y] == paredPrefab && Random.value < 0.5f && (mapa[x+1][y] == sueloPrefab || mapa[x - 1][y] == sueloPrefab || mapa[x][y+1] == sueloPrefab || mapa[x][y-1] == sueloPrefab))
                {
                    mapa[x][y] = sueloPrefab;
                }
            }
        }
    }

    private void GenerarPasillo()
    {
        int x = 1, y = 1;
        mapa[x][y] = entradaPrefab;

        while (x < ancho - 2 && y < alto - 2)
        {
            if (Random.value < 0.5f) x++; else y++;
            mapa[x][ y] = sueloPrefab;
        }

        mapa[x][y] = salidaPrefab;
         
    }

    void GenerarMapa() {
        for (int x = 0; x < ancho; x++)
            for (int y = 0; y < alto; y++)
            {
                mapa[x][y] = paredPrefab;
                Debug.Log("Se han generado una pared");
            }
        Debug.Log("Se han generado el Mapa");
    }
    void GenerarParedes() {
        for (int x = 0; x < ancho; x++)
        {
            mapa[x][0] = paredPrefab;
            mapa[x][alto - 1] = paredPrefab;
            Debug.Log("Se han generado una pared");
        }

        for (int y = 0; y < alto; y++)
        {
            mapa[0][y] = paredPrefab;
            mapa[ancho -1][y] = paredPrefab;

            Debug.Log("Se han generado una pared");
        }
        Debug.Log("Se han generado las paredes");
    }
    void InstanciarMapa()
    {
        for (int x = 0; x < ancho; x++)
            for (int y = 0; y < alto; y++)
            {
                Vector3 pos = new Vector3(x * 12, 0, y * 12);
                GameObject toSpawn = null;

                bool esTrampa = trampasPrefab != null && Array.IndexOf(trampasPrefab, mapa[x][y]) >= 0;

                if (mapa[x][y] == paredPrefab)
                {
                    if (mapa[x][y].transform.position.x == 0) {
                        pos = new Vector3((x * 12) + 5.5f, 6.5f, y * 12);
                    }
                    toSpawn = paredPrefab;
                }
                else
                {
                    toSpawn = sueloPrefab;
                }

                
                if (mapa[x][y] ==   esTrampa) {
                    Debug.Log("Trampa");
                    mapa[x][y] = trampa;
                    toSpawn = trampa;
                }
                if (mapa[x][y] != esTrampa) {
                    GameObject instanciado = Instantiate(toSpawn, pos, Quaternion.identity);
                    Debug.Log("Instanciado en " + instanciado.transform.position);
                }
                if (mapa[x][y] == entradaPrefab)
                    Instantiate(entradaPrefab, pos + Vector3.up * 0.5f, Quaternion.identity, transform);
                else if (mapa[x][y] == salidaPrefab)
                    Instantiate(salidaPrefab, pos + Vector3.up * 0.5f, Quaternion.identity, transform);
            }
    }


}
