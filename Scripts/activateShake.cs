using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateShake : MonoBehaviour
{
    public cameraShake shake;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shake.shake(1f, 3f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
