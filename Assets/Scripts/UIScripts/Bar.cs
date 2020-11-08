using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private Transform bar;

    // Update is called once per frame
    public void setValue(float value)
    {

            bar.localScale = new Vector3(value, 1f);
    }
}
