using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AyarlarMenusu : MonoBehaviour
{
    public Slider genelSesCubugu;
    public Slider muzikCubugu;
    public Slider sesEfektiCubugu;

    public GameObject ayarlarMenusuUI;
    public GameObject anaMenuUI;
    public GameObject durdurmaMenusuUI;

    private const float minSesDuzeyi = 0.0001f;

    private void Start()
    {        
        TumSesleriYukle();
    }
    private void Update()
    {
        DurdurmaMenusu.ayarlarMenusuAcikMi = true;
        if (Input.GetKeyDown(KeyCode.Escape) && DurdurmaMenusu.ayarlarMenusuAcikMi)
        {
            AyarlarMenusunuKapat();
        }
    }

    public void GenelSesiAyarla()
    {
        float ses = Mathf.Max(genelSesCubugu.value, minSesDuzeyi);
        SesYoneticisi.sesYoneticisi.GenelSesiAyarla(ses);
    }

    public void MuzikSesiniAyarla()
    {
        float ses = Mathf.Max(muzikCubugu.value, minSesDuzeyi);
        SesYoneticisi.sesYoneticisi.MuzikSesiniAyarla(ses);
    }

    public void SesEfektiniAyarla()
    {
        float ses = Mathf.Max(sesEfektiCubugu.value, minSesDuzeyi);
        SesYoneticisi.sesYoneticisi.SesEfektiAyarla(ses);
    }

    private void TumSesleriYukle()
    {
        genelSesCubugu.value = SesYoneticisi.sesYoneticisi.SesiAl("genel", 0.5f);
        muzikCubugu.value = SesYoneticisi.sesYoneticisi.SesiAl("muzik", 0.5f);
        sesEfektiCubugu.value = SesYoneticisi.sesYoneticisi.SesiAl("efekt", 0.5f);

        GenelSesiAyarla();
        MuzikSesiniAyarla();
        SesEfektiniAyarla();
    }

    public void TamEkranOlarakAyarla(bool tamEkranMi)
    {
        Screen.fullScreen = tamEkranMi;
    }
   
    private void AyarlarMenusunuKapat()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            ayarlarMenusuUI.SetActive(false);
            anaMenuUI.SetActive(true);
            DurdurmaMenusu.ayarlarMenusuAcikMi = false;
        }
        else
        {
            ayarlarMenusuUI.SetActive(false);
            durdurmaMenusuUI.SetActive(true);
            DurdurmaMenusu.ayarlarMenusuAcikMi = false;
        }
    }
}