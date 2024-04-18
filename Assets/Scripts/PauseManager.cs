using UnityEngine;

public class PauseManager : MonoBehaviour
{

    private static bool _paused;

    public static void PauseGame()
    {
        if (_paused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }

        _paused = !_paused;
    }
}
