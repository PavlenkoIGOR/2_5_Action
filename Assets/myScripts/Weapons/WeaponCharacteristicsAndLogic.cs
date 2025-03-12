using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponCharacteristicsAndLogic : MonoBehaviour, IWeapon
{
    public float FiringTemp;
    public float FireingDistance;
    public float FiringRange;
    public float BulletSpeed;
    public float MagazineCapacity;

    public Transform StartFiringPosition;
    public GameObject BulletPrefab;

    public abstract void WeaponFire(bool isFiring, Transform target);
}
