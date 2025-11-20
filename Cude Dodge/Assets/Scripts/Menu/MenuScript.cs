using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void _PlayButton()
    {
        SceneManager.LoadScene("main");
    }

    public void _SettingsButton()
    {
        SceneManager.LoadScene("settings");
    }

    public void _ExitButton()
    {
        Application.Quit();
    }

    public void _BackToMenuButton()
    {
        SceneManager.LoadScene("menu");
    }
}
