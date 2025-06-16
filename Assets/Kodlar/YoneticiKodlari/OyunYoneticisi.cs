using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class OyunYoneticisi : MonoBehaviour
{
    private Vector2 kontrolNoktasiKonumu;
    private Rigidbody2D rb;
    private GameObject avci;
    private AvciOzellikleri avciOzellikleri;
    private Animator animator;

    private void Awake()
    {
        avci = GameObject.FindGameObjectWithTag("Player");
        rb = avci.GetComponent<Rigidbody2D>();
        avciOzellikleri = avci.GetComponent<AvciOzellikleri>();
        animator = avci.GetComponent<Animator>();
    }

    private void Start()
    {
        kontrolNoktasiKonumu = avci.transform.position;
    }

    public void KontrolNoktasiniGuncelle(Vector2 konum)
    {
        kontrolNoktasiKonumu = konum;
    }

    public IEnumerator YenidenDog(float sure)
    {
        yield return new WaitForSeconds(sure);
        rb.simulated = false;
        avci.transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(sure);
        RuntimeAnimatorController originalController = animator.runtimeAnimatorController;
        animator.runtimeAnimatorController = null;
        animator.runtimeAnimatorController = originalController;
        avci.transform.position = kontrolNoktasiKonumu;
        avci.transform.localScale = Vector3.one;
        rb.velocity = Vector2.zero;
        rb.simulated = true;
        avci.GetComponent<AvciHareketKodu>().enabled = true;
        avci.GetComponent<AvciYakinSaldiri>().enabled = true;
        avci.GetComponent<AtesEtmeKodu>().enabled = true;
        animator.SetBool("saldiri1", false);
        avciOzellikleri.CaniSifirla();
    }
}
