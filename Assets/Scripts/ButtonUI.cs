using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("MainMap");
    }
    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
