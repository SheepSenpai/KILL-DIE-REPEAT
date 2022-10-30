using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainbowmaterial : MonoBehaviour
{
    public float Speed = 1;
    public Material mat;

    void Update()
    {
        mat.SetColor("_Color", HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * Speed, 1), 1, 1)));
    }
}
