using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Referencia al contenedor")]
    [SerializeField] private Contenedor contenedor;

    [Header("Campos de entrada")]
    [SerializeField] private TMP_InputField anchoInput;
    [SerializeField] private TMP_InputField altoInput;

    private void Start()
    {
        if (contenedor != null)
        {
            anchoInput.text = contenedor.ancho.ToString();
            altoInput.text = contenedor.alto.ToString();
        }
        else
        {
            Debug.LogError("No se ha asignado el contenedor en el inspector.");
        }
    }

    public void PlayGame()
    {
        if (contenedor == null) return;

        
        contenedor.ancho = int.Parse(anchoInput.text);
        contenedor.alto = int.Parse(altoInput.text);

        SceneManager.LoadScene("CargarEscena");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
