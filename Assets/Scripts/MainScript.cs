using System;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public ImageButtonTimer villageButton;
    public ImageButtonTimer warriorButton;
    
    public ImageTimer eatingTimer;
    public float eatingTime = 10;
    public int warriorEats = 2;
    public int villagerEats = 1;
    
    public ImageTimer harvestTimer;
    public float harvestTime = 5;
    public int harvestFoodIncome = 2;

    public GameObject enemyTimerGroup;
    public ImageTimer enemyTimer;
    public float enemyTime = 5;
    public int enemiesCount = 1;
    public double enemiesTimeModifier = 1.2;
    public double enemiesCountModifier = 1.4f;

    public Text foodText;
    public Text villagersText;
    public Text warriorsText;
    public GameObject enemiesCountText;
    public Text enemiesCyclesCount;
    
    public int food = 5;
    public int villagers = 1;
    public int warriors = 0;
    public int ticks = 0;
    public int ticksForEnemyAttack = 3;

    public float villagersTimeout = 1f;
    public float warriorTimeout = 2f;

    public int villagerCost = 3;
    public int warriorCost = 5;

    public int villagersForWin = 100;

    private AudioSource _audio;
    public AudioSource music;

    private bool _soundsDisabled = false;
    public Image soundButton;

    private void Awake()
    {
        harvestTimer.SetMaxTime(harvestTime);
        eatingTimer.SetMaxTime(eatingTime);
        enemyTimer.SetMaxTime(harvestTime);
        
        villageButton.OnClickTried = HireVillager;
        warriorButton.OnClickTried = HireWarrior;

        villageButton.OnTimerComplete = VillagerHired;
        warriorButton.OnTimerComplete = WarriorHired;
        
        villageButton.SetMaxTime(villagersTimeout);
        warriorButton.SetMaxTime(warriorTimeout);

        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (harvestTimer.tick)
        {
            food += villagers*harvestFoodIncome;
            ticks++;
            _audio.PlayOneShot((AudioClip)Resources.Load("Sounds/harvest", typeof(AudioClip)));
        }

        if (eatingTimer.tick)
        {
            food -= villagers * villagerEats + warriors * warriorEats;
            if (food < 0)
            {
                food = 0;
                OnDefeat();
            }
        }

        if (ticks ==  ticksForEnemyAttack && !enemiesCountText.activeSelf)
        {
            EnableEnemies();
        }
        else
        {
            enemiesCyclesCount.text = $"через {ticksForEnemyAttack - ticks} цикла";
        }
        
        if (enemyTimer.tick && ticks > ticksForEnemyAttack)
        {
            StartBattle();
        }
        
        UpdateText();
    }
    
    public bool HireVillager()
    {
        if (food >= villagerCost)
        {
            Debug.Log("HireVillager");
            food -= villagerCost;
            return true;
        }
        return false;
    }

    public bool HireWarrior()
    {
        if (food >= warriorCost)
        {
            Debug.Log("HireWarrior");
            food -= warriorCost;
            return true;
        }

        return false;
    }

    private void StartBattle()
    {
        _audio.PlayOneShot((AudioClip)Resources.Load("Sounds/battle", typeof(AudioClip)));
        if (enemiesCount > warriors)
        {
            OnDefeat();
            return;
        }

        Statistics.enemiesKilled += enemiesCount;
        Statistics.cyclesWon++;
        
        warriors -= enemiesCount;
        enemyTime = (float)Math.Ceiling(enemyTime * enemiesTimeModifier);
        enemyTimer.SetMaxTime(enemyTime);
        enemiesCount = (int)Math.Ceiling(enemiesCount * enemiesCountModifier);
    }
    
    private void OnDefeat()
    {
        Debug.Log("DEFEAT");
        music.Stop();
        music.PlayOneShot((AudioClip)Resources.Load("Sounds/defeat", typeof(AudioClip)));
        PauseManager.PauseGame();
        DialogController.ShowDialog("DefeatDialog");
    }

    private void OnWin()
    {
        PauseManager.PauseGame();
        music.Stop();
        music.PlayOneShot((AudioClip)Resources.Load("Sounds/win", typeof(AudioClip)));
        DialogController.ShowDialog("WinDialog");
    }

    private void EnableEnemies()
    {
        enemyTimerGroup.SetActive(true);
        enemyTimer.maxTime = enemyTime;
        enemiesCyclesCount.enabled = false;
        enemiesCountText.SetActive(true);
    }

    private void UpdateText()
    {
        foodText.text = $"{food} пищи";
        warriorsText.text = $"{warriors} воинов";
        villagersText.text = $"{villagers} крестьян";
        var enemiesText = enemiesCountText.transform.GetChild(0).gameObject;
        enemiesText.GetComponent<Text>().text = $"x{enemiesCount.ToString()}";
    }

    private void VillagerHired()
    {
        villagers++;
        if (villagers >= villagersForWin)
        {
            OnWin();
        }
        _audio.PlayOneShot((AudioClip)Resources.Load("Sounds/villager", typeof(AudioClip)));
    }

    private void WarriorHired()
    {
        warriors++;
        _audio.PlayOneShot((AudioClip)Resources.Load("Sounds/warrior", typeof(AudioClip)));
    }

    public void DisableSounds()
    {
        if (_soundsDisabled)
        {
            _audio.mute = false;
            music.Play();
            _soundsDisabled = false;
            soundButton.GetComponent<Image>().sprite = (Sprite)Resources.Load("Sprites/music", typeof(Sprite));
        }
        else
        {
            _audio.mute = true;
            music.Pause();
            _soundsDisabled = true;
            soundButton.GetComponent<Image>().sprite = (Sprite)Resources.Load("Sprites/music_disabled", typeof(Sprite));
        }
    }
}
