using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BolumlerMenusu : MonoBehaviour
{
    private SahneYoneticisi SahneY;
    public GameObject bolumlerMenusuUI;
    public GameObject anaMenuUI;

    public Button[] butonlar;
    private void Awake()
    {
        SahneY = GameObject.FindGameObjectWithTag("Sahne").GetComponent<SahneYoneticisi>();
        int acilanBolum = PlayerPrefs.GetInt("acilanBolum", 1);
        for (int i = 0; i < butonlar.Length; i++)
        {
            butonlar[i].interactable = false;
        }
        for (int i = 0; i < acilanBolum; i++)
        {
            butonlar[i].interactable = true;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BolumlerMenusunuKapat();
        }
    }

    public void BolumeGir(int bolumID)
    {
        string bolumAdi = "Bolum" + bolumID;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SahneY.BolumuYukle(bolumAdi);
    }

    private void BolumlerMenusunuKapat()
    {
        bolumlerMenusuUI.SetActive(false);
        anaMenuUI.SetActive(true);
    }
}