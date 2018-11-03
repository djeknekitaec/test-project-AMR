using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BubblesManager bubblesManager;

    [SerializeField]
    ParticlesManager particlesManager;

    [SerializeField]
    AssetBundleManager assetBundleManager;

    [SerializeField]
    Text pointsCounter;
    [SerializeField]
    Text timeCounter;

    [SerializeField]
    GameObject loadingScreen;

    [SerializeField]
    Image backgroundImg;

    public static GameManager Instance;


    bool isPlaying = false;
    long points = 0;
    float timeForStart = 4f;

    private void Awake()
    {
        Instance = this;
        ShowPoints();
        assetBundleManager.DownloadCompleted += () =>
        {
            loadingScreen.SetActive(false);
            StartGame();
        };
    }

    public void BubbleCLicked(Bubble bubble)
    {
        points += bubble.points;
        ShowPoints();

        particlesManager.ShowParticle(bubble.transform.position);

        bubblesManager.ReturnBubble(bubble, true);

    }

    void StartGame()
    {
        StartCoroutine(startGame());
    }

    IEnumerator startGame()
    {
        yield return new WaitForEndOfFrame();
        float timeSpent = 0f;
        while (timeSpent < timeForStart)
        {
            timeSpent += Time.deltaTime;
            int val = (int)Mathf.Floor(timeForStart - timeSpent);
            string msg;
            if (val > 0f)
            {
                msg = val.ToString();
            }
            else
            {
                msg = "GO!";
            }
            timeCounter.text = msg;
            yield return new WaitForEndOfFrame();
        }
        timeCounter.text = "";
        isPlaying = true;
        bubblesManager.PlayGame(isPlaying);
        yield return null;
    }

    void ShowPoints()
    {
        pointsCounter.text = points.ToString();
    }

    public void RestartGame()
    {
        if (!isPlaying) return;

        isPlaying = false;
        bubblesManager.PlayGame(isPlaying);
        bubblesManager.RestartGame();
        points = 0;
        ShowPoints();
        StartGame();
    }

}
