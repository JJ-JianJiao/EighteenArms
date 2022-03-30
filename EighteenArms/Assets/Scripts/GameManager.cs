using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    SceneFader fader;

    List<Orib> oribs;

    //public int orbNum;
    public int deathNum;

    Door lockedDoor;

    float gameTime;
    bool gameIsOver;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        deathNum = 0;
        gameIsOver = false;
        oribs = new List<Orib>();
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (gameIsOver) return;
        //orbNum = instance.oribs.Count;
        gameTime += Time.deltaTime;
        UIManager.UpdateTimeUI(gameTime);
    }
    public static void RegisterOrb(Orib orib) {
        if (!instance.oribs.Contains(orib)) {
            instance.oribs.Add(orib);
        }
        UIManager.UpdateOrbUI(instance.oribs.Count);
    }

    public static void RegisterDoor(Door door)
    {
        instance.lockedDoor = door;
    }

    public static void RegisterSceneFader(SceneFader obj) {
        instance.fader = obj;
    }

    public static void PlayerDie() {
        instance.deathNum++;
        instance.oribs.Clear();
        instance.fader.FadeOut();
        instance.Invoke("RestartScene", 1.5f);
        UIManager.UpdateDeathUI(instance.deathNum);
    }

    void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void PlayerGetOrb(Orib orb) {
        if (!instance.oribs.Contains(orb))
            return;
        instance.oribs.Remove(orb);
        UIManager.UpdateOrbUI(instance.oribs.Count);
        if (instance.oribs.Count == 0) {
            instance.lockedDoor.Open();
        }
    }

    public static void GameOver() {
        instance.gameIsOver = true;
        UIManager.UpdateGameOverUI();
        AudioManager.PlayerWinAudio();
        
    }

    public static bool GetGameOver() {
        return instance.gameIsOver;
    }
}
