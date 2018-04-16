using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyClickDetector : MonoBehaviour {

    private ButterflyBehaviourScript butterflyBehavior;

    private void OnMouseDown()
    {
        butterflyBehavior.OnClick();
    }

    private void Awake()
    {
        butterflyBehavior = GetComponent<ButterflyBehaviourScript>();
    }
}
