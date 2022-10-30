using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    private WeaponSway weaponSway;
    private WeaponMotor weaponmotor;
    private SpawnEntities spawner;
    private PauseMenu pause;


    Coroutine fireCoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        weaponmotor = GetComponentInChildren<WeaponMotor>();
        weaponSway = GetComponentInChildren<WeaponSway>();
        spawner = GetComponent<SpawnEntities>();
        pause = GetComponentInChildren<PauseMenu>();
        onFoot.Jump.performed += ctx => motor.Jump();
        look = GetComponent<PlayerLook>();
        onFoot.Shoot.started += ctx => startFiring();
        onFoot.Shoot.canceled += ctx => stopFiring();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.Reload.performed += ctx => weaponmotor.StartReload();
        onFoot.SpawnEntities.performed += ctx => spawner.SpawnTargets();
        onFoot.Quit.performed += ctx => pause.PauseGame();
        onFoot.FireMode.performed += ctx => weaponmotor.SwitchFireMode();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector3>());
    }

    void startFiring()
    {
        fireCoroutine = StartCoroutine(weaponmotor.RapidFire());
    }

    void stopFiring()
    {
        if(fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
    }

    void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        weaponSway.ProcessWeaponSway(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
