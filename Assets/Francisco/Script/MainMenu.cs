using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // [SerializeField] private InputField anchoInput;
    //[SerializeField] private InputField altoInput;
    private void Start()
    {
       // anchoInput.text = GameSettings.Instance.ancho.ToString();
        //altoInput.text = GameSettings.Instance.alto.ToString();
    }
    public void PlayGame()
    {/*
        int ancho = int.Parse(anchoInput.text);
        int alto = int.Parse(altoInput.text);

        GameSettings.Instance.ancho = ancho;
        GameSettings.Instance.alto = alto;
       */ 
        SceneManager.LoadScene("CargarEscena");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
