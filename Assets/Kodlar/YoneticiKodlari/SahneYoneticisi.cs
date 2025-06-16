using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneYoneticisi : MonoBehaviour
{
    public static SahneYoneticisi sahneYoneticisi;
    [SerializeField] RectTransform gecisGorseli;

    private void Awake()
    {
        if (sahneYoneticisi == null)
        {
            sahneYoneticisi = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        gecisGorseli.gameObject.SetActive(true);
        LeanTween.alpha(gecisGorseli, 1f, 0f);
        LeanTween.alpha(gecisGorseli, 0f, 0.5f).setOnComplete(() =>
        {
            gecisGorseli.gameObject.SetActive(false);
        });
    }

    public void SonrakiBolumeGec()
    {
        gecisGorseli.gameObject.SetActive(true);
        LeanTween.alpha(gecisGorseli, 0f, 0f);
        LeanTween.alpha(gecisGorseli, 1f, 0.5f).setOnComplete(() =>
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1).completed += operation =>
            {
                LeanTween.alpha(gecisGorseli, 0f, 0.5f).setOnComplete(() =>
                {
                    gecisGorseli.gameObject.SetActive(false);
                });
            };
        });
    }

    public void BolumuYukle(string sahneAdi)
    {
        gecisGorseli.gameObject.SetActive(true);
        LeanTween.alpha(gecisGorseli, 0f, 0f);
        LeanTween.alpha(gecisGorseli, 1f, 0.5f).setOnComplete(() =>
        {
            SceneManager.LoadSceneAsync(sahneAdi).completed += operation =>
            {
                LeanTween.alpha(gecisGorseli, 0f, 0.5f).setOnComplete(() =>
                {
                    gecisGorseli.gameObject.SetActive(false);
                });
            };
        });
    }
}