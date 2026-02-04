using UnityEngine;
using UnityEngine.SceneManagement;

public class relodLevel : MonoBehaviour
{
    public Animator Anim;
    public PlayerData Data;
    void Awake()
    {
        Anim = GetComponent<Animator>();
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
    void Update()
    {
        Anim.SetBool("Dead", Data.Dead);
    }

}
