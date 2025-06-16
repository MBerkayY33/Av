using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DurdurmaMenusu : MonoBehaviour
{
    private AvciHareketKodu AvciHK;
    private AvciYakinSaldiri AvciYS;
    public static bool oyunDurduMu = false;
    public static bool ayarlarMenusuAcikMi ;

    public GameObject durdurmaMenusuUI;

    private void Awake()
    {
        AvciHK = GameObject.FindGameObjectWithTag("Player").GetComponent<AvciHareketKodu>();
        AvciYS = GameObject.FindGameObjectWithTag("Player").GetComponent<AvciYakinSaldiri>();
    }
    private void Start()
    {
        ayarlarMenusuAcikMi = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !ayarlarMenusuAcikMi)
        {
            if (oyunDurduMu)
            {
                DevamEt();
            }
            else
            {
                OyunuDurdur();
            }
        }
    }

    public void DevamEt()
    {
        AvciHK.enabled = true;
        AvciYS.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        durdurmaMenusuUI.SetActive(false);
        Time.timeScale = 1f;
        oyunDurduMu = false;
        ayarlarMenusuAcikMi = false;
    }

    private void OyunuDurdur()
    {
        AvciHK.enabled = false;
        AvciYS.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        durdurmaMenusuUI.SetActive(true);
        Time.timeScale = 0f;
        oyunDurduMu = true;
    }

    public void YenidenBaslat()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        durdurmaMenusuUI.SetActive(false);
        Time.timeScale = 1f;
        oyunDurduMu = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ayarlarMenusuAcikMi = false;
    }

    public void MenuyuYukle()
    {
        oyunDurduMu = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        ayarlarMenusuAcikMi = false;
    }

    public void OyundanCik()
    {
        Application.Quit();
        ayarlarMenusuAcikMi = false;
    }
}