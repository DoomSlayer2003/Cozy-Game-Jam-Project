using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnCreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnBackButton()
    {
        SceneManager.LoadScene("Menu");
    }

}
