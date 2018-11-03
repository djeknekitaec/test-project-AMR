using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetBundleManager : MonoBehaviour
{
    [SerializeField]
    BubblesManager bubblesManager;

    [SerializeField]
    Image backgroundImage;

    public delegate void AssetsDownloaded();
    public event AssetsDownloaded DownloadCompleted;

    private void Awake()
    {
        StartCoroutine(downloadAssets());
    }

    IEnumerator downloadAssets()
    {
        WWW www = WWW.LoadFromCacheOrDownload("https://dl.dropboxusercontent.com/s/royx3iwq4bmmvh0/newbundle?dl=0", 0);
        yield return www;
        if (www.assetBundle != null)
        {
            //Loading background image
            Texture2D textureBg = www.assetBundle.LoadAsset("background_bundle") as Texture2D;
            if (textureBg != null)
            {
                Sprite spiteBg = Sprite.Create(textureBg, new Rect(0f, 0f, textureBg.width, textureBg.height), new Vector2(0.5f, 0.5f));
                backgroundImage.sprite = spiteBg;
            }
            else
            {
                Debug.LogError("Couldn't load Background texture from the asset budnle!");
            }

            //Loading bubble burst sound
            AudioClip audioClip = www.assetBundle.LoadAsset("bubble_burst") as AudioClip;
            if (audioClip != null)
            {
                bubblesManager.audioClip = audioClip;
            }
            else
            {
                Debug.LogError("Couldn't load bubble burst sound from the asset budnle!");
            }
        }
        else
        {
            Debug.Log("Couldn't load asset bundle!");
        }
        if (DownloadCompleted != null)
        {
            DownloadCompleted();
        }
        yield return null;
    }

}
