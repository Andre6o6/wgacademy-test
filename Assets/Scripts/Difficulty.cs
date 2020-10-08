using UnityEngine;
using UnityEngine.SceneManagement;

public class Difficulty : MonoBehaviour
{
    public void SetEasy()
    {
        GridField.shuffleIterations = 20;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetMedium()
    {
        GridField.shuffleIterations = 100;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetHard()
    {
        GridField.shuffleIterations = 1000;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
