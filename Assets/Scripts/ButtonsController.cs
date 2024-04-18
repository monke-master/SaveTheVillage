using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    public void OnPauseButtonClicked()
    {
        PauseManager.PauseGame();
        DialogController.ShowDialog("PauseDialog");
    }

    public void OnContinueButtonClicked()
    {
        PauseManager.PauseGame();
        DialogController.HideDialog("PauseDialog");
    }

    public void OnAgainButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DialogController.HideActiveDialog();
        PauseManager.PauseGame();
    }
}
