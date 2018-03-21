using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OxygenBar : MonoBehaviour {
    public Image CurrerntOxygen;

    private float Oxygen = 100;
    private float MaxOxygen = 100;

    private void Start()
    {
        UpdateOxygenBar();
    }

    private void UpdateOxygenBar()
    {
        float ratio = Oxygen / MaxOxygen;
        CurrerntOxygen.rectTransform.localScale = new Vector3(1, ratio, 1);
    }

    private void Update()
    {
        //Put timer here
    }
}
