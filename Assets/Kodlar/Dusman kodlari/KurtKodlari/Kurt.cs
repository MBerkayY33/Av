using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt : MonoBehaviour
{
    public Transform avci;
    public bool yonDegistirdiMi;

    public bool avciTespitEdildiMi { get; set; }

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
}
