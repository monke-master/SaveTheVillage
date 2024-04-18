using UnityEngine;
using UnityEngine.UI;

public class GameEndDialog : MonoBehaviour
{
    public GameObject gameStats;
    void Start()
    {
        var cyclesWonText = gameStats.transform.Find("CyclesWon").gameObject.GetComponent<Text>();
        var enemiesKilledText = gameStats.transform.Find("EnemiesKilled").gameObject.GetComponent<Text>();

        cyclesWonText.text = $"Волн пережито: {Statistics.cyclesWon}";
        enemiesKilledText.text = $"Врагов убито: {Statistics.enemiesKilled}";
    }
}
