using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BubblesManager : MonoBehaviour
{
    [SerializeField]
    BubblesPool bubblesPool;

    [SerializeField]
    Transform playGround;
    [SerializeField]
    Transform leftBoundary;
    [SerializeField]
    Transform rightBoundary;
    [SerializeField]
    Transform outOfZone;

    public AudioClip audioClip;

    [HideInInspector]
    public List<BubbleSettings> bubbleSettings = new List<BubbleSettings>();

    List<Bubble> bubbles = new List<Bubble>();

    int maxAmount = 3;
    int minAmount = 1;

    public readonly float minSize = 200;
    public readonly float maxSize = 500;

    public readonly float minSpeed = 20f;
    public readonly float maxSpeed = 75f;

    float delay = 0.6f;

    bool isPlaying = false;

    [System.Serializable]
    public struct BubbleSettings
    {
        public float size;
        public Color color;
        public float speed;
    }

    void Awake()
    {
        bubblesPool.Initialize(20);
    }

    void Start()
    {
        StartCoroutine(ThrowNewBubbles());
        StartCoroutine(MoveBubbles());
    }

    IEnumerator MoveBubbles()
    {
        while (true)
        {
            if (isPlaying)
            {
                for (int i = 0; i < bubbles.Count; i++)
                {
                    if (bubbles[i].transform.position.y < outOfZone.position.y)
                    {
                        ReturnBubble(bubbles[i]);
                    }
                    else
                    {
                        bubbles[i].transform.position += Vector3.down * bubbles[i].speed * Time.deltaTime;
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ThrowNewBubbles()
    {
        while (true)
        {
            if (isPlaying)
            {
                int count = Random.Range(minAmount, maxAmount);
                for (int i = 0; i < count; i++)
                {
                    AddNewBubble();
                }
            }
            yield return new WaitForSeconds(delay);
        }
    }

    public void PlayGame(bool state)
    {
        isPlaying = state;
    }

    public void AddNewBubble()
    {
        Vector3 newPos = new Vector3(Random.Range(leftBoundary.position.x, rightBoundary.position.x),
                                    Random.Range(leftBoundary.position.y, rightBoundary.position.y), leftBoundary.position.z);
        Bubble bubble = bubblesPool.Pull();
        bubble.transform.SetParent(playGround);
        bubble.transform.localScale = Vector3.one;
        bubble.transform.position = newPos;

        if (bubbleSettings.Count > 0)
        {
            BubbleSettings bubbleSetting = bubbleSettings[Random.Range(0, bubbleSettings.Count)];
            bubble.SetupBubble(bubbleSetting.size, bubbleSetting.color, bubbleSetting.speed, (int)(maxSize - bubbleSetting.size + minSize), audioClip);
        }
        else
        {
            float size = Random.Range(minSize, maxSize);
            bubble.SetupBubble(size, Color.blue, 0.6f, (int)(maxSize - size + minSize), audioClip);
        }

        bubbles.Add(bubble);
    }

    public void ReturnBubble(Bubble b, bool playSound = false)
    {
        if (bubbles.Contains(b))
        {
            if (playSound)
            {
                b.audioSource.Play();
            }
            bubbles.Remove(b);
            bubblesPool.Push(b);
        }
        else
        {
            Debug.LogError("Trying to return unknown bubble");
        }
    }

    public void RestartGame()
    {
        for (int i = 0; i < bubbles.Count; i++)
        {
            bubblesPool.Push(bubbles[i]);
        }
        bubbles.RemoveRange(0, bubbles.Count);
    }


    public void AddNewBubbleType()
    {
        BubbleSettings b = new BubbleSettings();
        b.size = minSize;
        b.color = Color.white;
        b.speed = 0.6f;
        bubbleSettings.Add(b);
    }

    public void RemoveBubbleType()
    {
        if (bubbleSettings.Count > 0)
        {
            bubbleSettings.RemoveAt(bubbleSettings.Count - 1);
        }
        else
        {
            Debug.Log("You don't have any bubbles to remove!");
        }
    }
}
