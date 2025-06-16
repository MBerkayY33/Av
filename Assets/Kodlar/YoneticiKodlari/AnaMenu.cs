using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState= CursorLockMode.None;
        Cursor.visible = true;
        SesYoneticisi.sesYoneticisi.TumSesleriYukle();
    }

    public void Oyna()
    {
        Cursor.visible= false;
        Cursor.lockState= CursorLockMode.Locked;
        SahneYoneticisi.sahneYoneticisi.SonrakiBolumeGec();
    }

    public void OyundanCik()
    {
        Application.Quit();
    }
}
