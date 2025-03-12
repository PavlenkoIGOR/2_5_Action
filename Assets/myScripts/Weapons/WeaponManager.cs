using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    IWeapon currentWeapon;
    public Transform target;

    public bool isFiring = false;
    private void Start()
    {

    }
    public void Update()
    {
        currentWeapon = GetComponentInChildren<IWeapon>();

        if (isFiring == true)
        {
            foreach (Transform item in this.transform)
            {
                if (item.gameObject.activeSelf == true && item.tag == "Weapon")
                {
                    //Debug.Log($"it is {item.name} firing from weapon manager");
                    currentWeapon?.WeaponFire(isFiring, target);
                }
            }
        }
        else
        {
            currentWeapon?.WeaponFire(isFiring, target);
        }
    }
    //// Корутин для стрельбы с задержкой
    //private IEnumerator ShootContinuously()
    //{
    //    // Защита от повторного вызова корутины
    //    while (Input.GetKey(KeyCode.Space))
    //    {
    //        Shoot(bulletPrefab, bulletSpeed, startFiringPosition);
    //        yield return new WaitForSeconds(0.3f); // Пауза между выстрелами
    //    }
    //}


    //public void ReloadCurrentWeapon()
    //{
    //    currentWeapon?.Reload();
    //}
}
