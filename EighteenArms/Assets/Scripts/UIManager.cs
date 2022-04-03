using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI orbText, timeText, deathText, gameOver;

    public Image currentHealthImg;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public PlayerAnimation playerAnimation;

    public Button playAgainBtn;
    public Button PlayBtn;
    public GameObject StartGamePanel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        Time.timeScale = 0;
        AudioListener.pause = true;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playAgainBtn.onClick.AddListener(PlayAgain);
        PlayBtn.onClick.AddListener(StartGame);
    }

    private void PlayAgain()
    {
        gameOver.gameObject.SetActive(false);
        GameManager.PlayAgain();
    }
    private void StartGame()
    {
        AudioListener.pause = false;
        Time.timeScale = 1;
        StartGamePanel.SetActive(false);
        GameManager.instance.isFreezon = false;
    }

    public static void UpdateOrbUI(int orbNum) {
        instance.orbText.text = orbNum.ToString();
    }
    public static void UpdateDeathUI(int deathNum)
    {
        instance.deathText.text = deathNum.ToString();
    }

    public static void UpdateTimeUI(float time)
    {
        int mins = (int)(time / 60f);
        int second = (int)(time % 60f);


        instance.timeText.text = mins.ToString("00") + ":" + second.ToString("00");
    }
    public static void UpdateGameOverUI() {
        //instance.gameOver.enabled = true;
        instance.gameOver.gameObject.SetActive(true);
    }

    public static void UpdateHealthBar(float healthPercent) {
        instance.currentHealthImg.fillAmount = healthPercent;
    }

    public static void UpdateHealthBar()
    {
        instance.currentHealthImg.fillAmount = instance.playerHealth.currentHealth/ instance.playerHealth.totalHealth;
    }

    public static void SetPlayerObj(GameObject gameObject)
    {
        if (gameObject.tag == "Player")
        {
            instance.playerAnimation = gameObject.GetComponent<PlayerAnimation>();
            instance.playerMovement = gameObject.GetComponent<PlayerMovement>();
            instance.playerHealth = gameObject.GetComponent<PlayerHealth>();
        }
    }
}
