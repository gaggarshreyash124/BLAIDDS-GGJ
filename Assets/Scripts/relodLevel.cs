using UnityEngine;
using UnityEngine.SceneManagement;

public class relodLevel : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
