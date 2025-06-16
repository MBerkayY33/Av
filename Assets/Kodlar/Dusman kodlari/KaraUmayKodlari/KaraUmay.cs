using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KaraUmay : MonoBehaviour
{
    public Transform avci;
    private kazandýnPanelikodu KPK;
    [SerializeField] RectTransform gecisGorseli;
    public bool yonDegistirdiMi;

    public bool avciTespitEdildiMi { get; set; }

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        KPK = GameObject.Find("FinalCanvas").GetComponent<kazandýnPanelikodu>();
        SahneyiYenidenBaslat();
    }

    public void KosmayiAyarla(bool kosuyorMu)
    {
        animator.SetBool("kosuyorMu", kosuyorMu);
    }

    public void AvciyaBak()
    {
        if (transform.position.x > avci.position.x && yonDegistirdiMi)
        {
            transform.Rotate(0f, 180f, 0f);
            yonDegistirdiMi = false;
        }
        else if (transform.position.x < avci.position.x && !yonDegistirdiMi)
        {
            transform.Rotate(0f, 180f, 0f);
            yonDegistirdiMi = true;
        }
    }

    private void SahneyiYenidenBaslat()
    {
        if (avci.GetComponent<AvciOzellikleri>().olduMu && !KPK.kazandinPaneliEtkinMi)
        {
            StartCoroutine(YenidenBaslatRoutine());
            avci.GetComponent<AvciOzellikleri>().olduMu = false;
        }
    }

    private IEnumerator YenidenBaslatRoutine()
    {
        gecisGorseli.gameObject.SetActive(true);
        LeanTween.alpha(gecisGorseli, 0f, 0f);
        LeanTween.alpha(gecisGorseli, 1f, 0.5f);
        yield return new WaitForSeconds(0.5f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Bolum3");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        LeanTween.alpha(gecisGorseli, 0f, 0.5f).setOnComplete(() =>
        {
            gecisGorseli.gameObject.SetActive(false);
        });

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
}
