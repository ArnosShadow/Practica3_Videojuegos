using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PantallaCarga : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider barraProgreso;
    [SerializeField] private TextMeshProUGUI texto; 
    [SerializeField] private string sceneToLoad = "SampleScene";

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            barraProgreso.value = progress;
            texto.text = (progress * 100f).ToString("F0") + "%";

            // Cuando llega al 90%, se puede activar la escena
            if (operation.progress >= 0.9f)
            {
                texto.text = "Generando mapa...";
                yield return new WaitForSeconds(1f); // Simula tiempo de generación
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}

