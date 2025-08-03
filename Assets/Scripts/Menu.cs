using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private GameObject settings;
    private GameObject mainMenu;
    public void Start()
    {
        settings = GameObject.Find("SettingsMenu");
        settings.SetActive(false);
        mainMenu = GameObject.FindWithTag("MainMenu");
        print(settings);
    }
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Settings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }
    public void Back()
    {
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }
}
