using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NasilOynanirPaneli : MonoBehaviour
{
    [SerializeField] private GameObject AnaMenu;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            NasilOynanirMenusunuKapat();
        }
    }

    private void NasilOynanirMenusunuKapat()
    {
        this.gameObject.SetActive(false);
        AnaMenu.SetActive(true);
    }
}
