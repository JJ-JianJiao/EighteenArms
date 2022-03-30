using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public TextMeshProUGUI orbText, timeText, deathText, gameOver;

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

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        instance.gameOver.enabled = true;
    }
}
