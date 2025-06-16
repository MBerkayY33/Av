using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SesYoneticisi : MonoBehaviour
{
    public static SesYoneticisi sesYoneticisi;
    [Header("------------Ses kaynaklari-----------")]
    [SerializeField] AudioSource muzikKaynagi;
    [SerializeField] AudioSource efektKaynagi;

    [Header("------------Ses klipleri-------------")]
    public AudioClip menuArkaplanMuzigi;
    public AudioClip bolum1ArkaplanMuzigi;
    public AudioClip bolum2ArkaplanMuzigi;
    public AudioClip bolum3ArkaplanMuzigi;
    public AudioClip olumSesi;
    public AudioClip kontrolNoktasiSesi;
    public AudioClip ziplamaSesi;
    public AudioClip yakinVurusSesi;
    public AudioClip atesEtmeSesi;
    public AudioClip hasarAlmaSesi;
    public AudioClip DusmanHasarAlmaSesi;
    public AudioClip karaUmayBüyüSesi;
    public AudioClip kurtSaldiriSesi;
    public AudioClip ayiSaldiriSesi;
    public AudioClip ayiTakipSesi;
    public AudioClip kurtTakipSesi;
    public AudioClip dusmeSesi;
    public AudioClip avciOlmeSesi;
    public AudioClip karaUmayOlmeSesi;

    [Header("-----------Ses Karistiricisi----------")]
    public AudioMixer sesKaristiricisi;

    private const float minSesDuzeyi = 0.0001f;
    private void Awake()
    {
        if(sesYoneticisi == null)
        {
            sesYoneticisi = this;
            DontDestroyOnLoad(gameObject);
            TumSesleriYukle();
            SceneManager.sceneLoaded += SahneYuklendigindeCalis;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (sesYoneticisi == this)
        {
            SceneManager.sceneLoaded -= SahneYuklendigindeCalis;
        }
    }

    private void Start()
    {
        ArkaplanMuziginiGuncelle(SceneManager.GetActiveScene().name);
    }
    private void SahneYuklendigindeCalis(Scene sahne ,LoadSceneMode mode)
    {
        ArkaplanMuziginiGuncelle(sahne.name);
    }
   
    private void ArkaplanMuziginiOynat(AudioClip klip)
    {
        if (muzikKaynagi.clip == klip && muzikKaynagi.isPlaying)
        {
            return;
        }

        muzikKaynagi.clip = klip;
        muzikKaynagi.Play();
    }

    private void ArkaplanMuziginiGuncelle(string sahneIsmi)
    {
        switch (sahneIsmi)
        {
            case "Menu":
                ArkaplanMuziginiOynat(menuArkaplanMuzigi);
                break;
            case "Bolum1":
                ArkaplanMuziginiOynat(bolum1ArkaplanMuzigi);
                break;
            case "Bolum2":
                ArkaplanMuziginiOynat(bolum2ArkaplanMuzigi);
                break;
            case "Bolum3":
                ArkaplanMuziginiOynat(bolum3ArkaplanMuzigi);
                break;
        }
    }

    public void SesEfektiOynat(AudioClip klip)
    {

        efektKaynagi.PlayOneShot(klip);

    }

    public void GenelSesiAyarla(float ses)
    {
        float ayarlanmisSes = Mathf.Max(ses, minSesDuzeyi);
        sesKaristiricisi.SetFloat("genelSes", Mathf.Log10(ayarlanmisSes) * 20);
        PlayerPrefs.SetFloat("genel", ayarlanmisSes);
    }

    public void MuzikSesiniAyarla(float ses)
    {
        float ayarlanmisSes = Mathf.Max(ses, minSesDuzeyi);
        sesKaristiricisi.SetFloat("muzikSesi", Mathf.Log10(ayarlanmisSes) * 20);
        PlayerPrefs.SetFloat("muzik", ayarlanmisSes);
    }

    public void SesEfektiAyarla(float ses)
    {
        float ayarlanmisSes = Mathf.Max(ses, minSesDuzeyi);
        sesKaristiricisi.SetFloat("sesEfekti", Mathf.Log10(ayarlanmisSes) * 20);
        PlayerPrefs.SetFloat("efekt", ayarlanmisSes);
    }

    public void TumSesleriYukle()
    {
        GenelSesiYukle();
        MuzikSesiniYukle();
        SesEfektiniYukle();
    }

    private void GenelSesiYukle()
    {
        float ses = PlayerPrefs.HasKey("genel") ? PlayerPrefs.GetFloat("genel") : 0.5f;
        GenelSesiAyarla(ses);
    }

    private void MuzikSesiniYukle()
    {
        float ses = PlayerPrefs.HasKey("muzik") ? PlayerPrefs.GetFloat("muzik") : 0.5f;
        MuzikSesiniAyarla(ses);
    }

    private void SesEfektiniYukle()
    {
        float ses = PlayerPrefs.HasKey("efekt") ? PlayerPrefs.GetFloat("efekt") : 0.5f;
        SesEfektiAyarla(ses);
    }

    public float SesiAl(string anahtar, float varsayilanDeger)
    {
        return PlayerPrefs.HasKey(anahtar) ? PlayerPrefs.GetFloat(anahtar) : varsayilanDeger;
    }
}
