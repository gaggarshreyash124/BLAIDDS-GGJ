using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator Anim;
    public void onPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void Play()
    {
        Anim.SetTrigger("Play");
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
