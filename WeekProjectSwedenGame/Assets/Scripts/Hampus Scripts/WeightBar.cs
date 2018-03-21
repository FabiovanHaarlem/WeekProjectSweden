using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeightBar : MonoBehaviour {
    public Image Weight;
    private float CurrentWeight = 0;
    private float MaxWeight = 100;

    private void Start()
    {
        
    }

    void UpdateWeight()
    {
        float weight = CurrentWeight / MaxWeight;
        Weight.rectTransform.localScale = new Vector3(1, CurrentWeight, 1);
    }

    private void Update()
    {
        //place timer here
    }
}
