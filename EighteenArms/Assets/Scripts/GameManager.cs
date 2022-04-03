using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    SceneFader fader;

    public List<Orib> oribs;

    public List<CheckPoint> checkPoints;

    //public int orbNum;
    public int deathNum;

    Door lockedDoor;

    float gameTime;
    bool gameIsOver;

    public Vector3 checkPointPos;
    public GameObject playerObj;

    public bool isFreezon;

    public int CurrentCheckPointID;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        CurrentCheckPointID = -1;
        instance = this;
        deathNum = 0;
        gameIsOver = false;
        oribs = new List<Orib>();
        isFreezon = true;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (gameIsOver) return;
        //orbNum = instance.oribs.Count;
        gameTime += Time.deltaTime;
        UIManager.UpdateTimeUI(gameTime);
    }

    internal static void PlayAgain()
    {
        instance.CurrentCheckPointID = -1;
        instance.checkPointPos = Vector3.zero;
        PlayerDie();
        //instance.oribs.Clear();
        //instance.checkPoints.Clear();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void RegisterOrb(Orib orib) {
        if (!instance.oribs.Contains(orib)) {
            instance.oribs.Add(orib);
        }
        UIManager.UpdateOrbUI(instance.oribs.Count);
    }

    public static void RegisterCheckPoint(CheckPoint checkPoint)
    {
        if (!instance.checkPoints.Contains(checkPoint))
        {
            instance.checkPoints.Add(checkPoint);
        }
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
        instance.checkPoints.Clear();

        //instance.fader.FadeOut();
        //instance.Invoke("RestartScene", 1.5f);
        //UIManager.UpdateDeathUI(instance.deathNum);
        //UIManager.UpdateHealthBar();

        instance.StartCoroutine("ReLoadGameScene");
        
    }

    IEnumerator ReLoadGameScene() {
        instance.isFreezon = true;
        yield return new WaitForSeconds(1.5f);
        FadeOut();
        while (!instance.fader.FadeOutEnd())
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        //instance.RestartScene();
        var check = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        while (!check.isDone) {
            yield return null;
        }
        Debug.Log("Scene load done");
        UIManager.UpdateHealthBar();
        playerObj.transform.position = GetCheckPointPosition();
        FadeIn();
        while (!instance.fader.FadeInEnd())
        {
            yield return null;
        }
        //yield return new WaitForSeconds(0.5f);
        instance.fader.ResetFader();
        instance.isFreezon = false;
    }

    public static void FadeIn()
    {

        instance.fader.FadeIn();
        
    }

    public static void FadeOut()
    {

        instance.fader.FadeOut();

    }

    public static bool FadeOutEnd()
    {

        return instance.fader.FadeOutEnd();

    }

    public static bool FadeInEnd()
    {

        return instance.fader.FadeInEnd();

    }



    void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public static void PlayerGetOrb(Orib orb, Vector3 pos) {
        //instance.checkPointPos = pos;
        if (!instance.oribs.Contains(orb))
            return;
        instance.oribs.Remove(orb);
        UIManager.UpdateOrbUI(instance.oribs.Count);
        if (instance.oribs.Count == 0) {
            instance.lockedDoor.Open();
        }
    }

    public static Vector3 GetCheckPointPosition()
    {
        return instance.checkPointPos;
    }

    public static void GameOver() {
        instance.gameIsOver = true;
        UIManager.UpdateGameOverUI();
        AudioManager.PlayerWinAudio();
        
    }

    public static bool GetGameOver() {
        return instance.gameIsOver;
    }

    public static bool SetCheckPointPosition(Vector3 checkPointPos, int type, int checkPointID = -1) {
        bool temp = false;
        if (type == 0 && instance.checkPointPos == Vector3.zero && checkPointID == -1)
        {
            instance.checkPointPos = checkPointPos;
            temp = true;
        }
        else if (type == 1)
        {
            foreach (var checkPoint in instance.checkPoints)
            {
                checkPoint.CloseLight();
            }

            if (checkPointID >= instance.CurrentCheckPointID)
            {
                instance.CurrentCheckPointID = checkPointID;
                instance.checkPointPos = checkPointPos;
                temp = true;


            }
        }
        return temp;
    }

    public static void SetPlayerObj(GameObject playerObj) {
        instance.playerObj = playerObj;
    }
}
