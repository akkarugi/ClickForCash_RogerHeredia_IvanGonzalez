using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScene : MonoBehaviour
{

    public string sceneName;

   
    public void LoadScene()
    {
        // Cargar la escena especificada
        SceneManager.LoadScene("MainGame");
    }
}