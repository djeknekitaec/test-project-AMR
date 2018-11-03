using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bubble : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    Image image;

    [SerializeField]
    RectTransform rectTransform;

    public AudioSource audioSource;

    [HideInInspector]
    public GameManager gameManager;

    float _speed;
    public float speed { get { return _speed; } }

    int _points;
    public int points { get { return _points; } }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.BubbleCLicked(this);
    }

    public void SetupBubble(float size, Color color, float speed, int points, AudioClip audioClip)
    {
        image.color = color;
        rectTransform.sizeDelta = new Vector2(size, size);
        _speed = speed;
        _points = points;
        audioSource.clip = audioClip;
    }

}
