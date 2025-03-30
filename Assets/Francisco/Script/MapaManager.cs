using UnityEngine;

public class MapaManager
{
    [Header("Propiedades inciales")]
    [SerializeField] private int alto = 20;
    [SerializeField] private int ancho = 20;
    [Header("Bloques")]
    [SerializeField] private GameObject sueloPrefab;
    [SerializeField] private GameObject paredPrefab;
    [SerializeField] private GameObject trampaPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject salidaPrefab;
    [SerializeField] private GameObject entradaPrefab;

    void start() {
        generarMapa();
        generarParedes();

        //TODO
    }

    void generarMapa() {
        for (int x = 0; x < ancho; x++)
            for (int y = 0; y < alto; y++)
            {
                //TODO
            }
    }
    void generarParedes() {
        for (int x = 0; x < ancho; x++)
        {
            mapa[x, 0] = ;
            mapa[x, alto - 1] = ;
        }

        for (int y = 0; y < alto; y++)
        {
            map[0, y] = ;
            map[ancho - 1, y] =;
        }
    }
}
