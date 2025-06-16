using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanBari : MonoBehaviour
{
    public Slider cubuk;

    public void MaxCaniAyarla(float can)
    {
        cubuk.maxValue = can;
        cubuk.value = can;
    }
    public void CaniAyarla(float can)
    {
        cubuk.value = can;
    }
}