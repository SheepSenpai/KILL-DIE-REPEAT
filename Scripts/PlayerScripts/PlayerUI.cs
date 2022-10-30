using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptTextButton;

    [SerializeField]
    private TextMeshProUGUI promptTextAmmo;

    [SerializeField]
    private TextMeshProUGUI promptTextFireMode;

    [SerializeField]
    private TextMeshProUGUI promptWaveSpawning;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateInteractText(string promptMessage)
    {
        promptTextButton.text = promptMessage;
    }

    public void UpdateAmmoText(string promptMessage)
    {
        promptTextAmmo.text = promptMessage;
    }

    public void UpdateFireModeText(string promptMessage)
    {
        promptTextFireMode.text = promptMessage;
    }

    public void UpdateWaveText(string promptMessage)
    {
        promptWaveSpawning.text = promptMessage;
    }
}
