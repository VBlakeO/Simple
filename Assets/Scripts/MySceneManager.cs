using UnityEngine.SceneManagement;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    static public MySceneManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
