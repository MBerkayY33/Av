using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BitisNoktasi : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            YeniBolumuAc();
            SahneYoneticisi.sahneYoneticisi.SonrakiBolumeGec();
        }
    }

    private void YeniBolumuAc()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ulasilanIndex"))
        {
            PlayerPrefs.SetInt("ulasilanIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("acilanBolum", PlayerPrefs.GetInt("acilanBolum", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}