using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMotor : MonoBehaviour
{
    public GunData gunData;
    public Transform muzzle;
    private PlayerUI playerUI;

    float timeSinceLastShot;
    public GameObject bulletImpact;

    WaitForSeconds rapidFireWait;

    public GameObject lightpoint;

    bool rapidFire = false;

    private void Start()
    {
        gunData.currentAmmo = 31;
        playerUI = GetComponentInParent<PlayerUI>();
    }

    private void Awake()
    {
        rapidFireWait = new WaitForSeconds(1 / gunData.fireRate);
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }

        }
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            if (gunData.currentAmmo != gunData.magSize)
                StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        playerUI.UpdateAmmoText("RELOADING");

        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzle.position, muzzle.forward);

        if (!gunData.reloading)
        {
            playerUI.UpdateAmmoText(gunData.currentAmmo + "/" + gunData.magSize);
        }
    }

    private void OnGunShot()
    {
        if (Physics.Raycast(muzzle.position, transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
        {
            StartCoroutine(MuzzleFlash());
            IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
            Instantiate(bulletImpact, hitInfo.point, transform.rotation);
            damageable?.Damage(gunData.damage);
        }
    }

    public void SwitchFireMode()
    {
        rapidFire = !rapidFire;
        if (rapidFire == true)
        {
            playerUI.UpdateFireModeText("AUTO");
        }
        else if (rapidFire == false)
        {
            playerUI.UpdateFireModeText("SEMI-AUTO");
        }
    }

    public IEnumerator RapidFire()
    {
        if (rapidFire == true)
        {
            while (true)
            {
                Shoot();
                yield return new WaitForSeconds(1 / gunData.fireRate);
            }
        }
        else
        {
            Shoot();
            yield return null;
        }
    }

    public IEnumerator MuzzleFlash()
    {

        lightpoint.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        lightpoint.SetActive(false);
        yield return null;
    }
}
