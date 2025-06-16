using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kazandınPanelikodu : MonoBehaviour
{
    public GameObject kazandinPaneli;
    private KaraUmayCanKodu KUCan;
    public bool kazandinPaneliEtkinMi;
    private float panelTetiklenmeZamani, beklemeSuresi;

    private void Start()
    {
        KUCan = GameObject.Find("KaraUmay").GetComponent<KaraUmayCanKodu>();
        beklemeSuresi = 4f;
        panelTetiklenmeZamani = Mathf.Infinity;
    }

    private void Update()
    {
        KazandinPaneliniEtkinleştir();
        if (kazandinPaneliEtkinMi && Time.time >= panelTetiklenmeZamani + beklemeSuresi)
        {
            kazandinPaneli.SetActive(true);
        }
    }

    private void KazandinPaneliniEtkinleştir()
    {
        if (KUCan.olduMu && !kazandinPaneliEtkinMi)
        {
            panelTetiklenmeZamani = Time.time;
            kazandinPaneliEtkinMi = true;
            SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.dusmeSesi);
        }
    }
}
