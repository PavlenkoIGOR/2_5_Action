using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : WeaponCharacteristicsAndLogic, IWeapon
{
    private float _shotLife = 2.0f;

    private float _lastShotTime = 0;
    private bool _isFiring = false;

    public void Shoot()
    {

        // �������� ����
        GameObject plasmaShot_L = Instantiate(BulletPrefab, StartFiringPosition.position, Quaternion.identity);
        plasmaShot_L.transform.up = StartFiringPosition.forward;

        // ������� Rigidbody �� ��������� ����
        Rigidbody rb = plasmaShot_L.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Bullet prefab must have a Rigidbody component.");
            return; // ���� Rigidbody �����������, ��������� ����������
        }

        // ������������� ����������� ������
        Vector3 fireDirection = StartFiringPosition.forward; // ���������� forward �� StartFiringPosition
        rb.AddForce(fireDirection * BulletSpeed, ForceMode.Impulse);

        // ������ ��� ��� ������������
        Ray ray = new Ray(StartFiringPosition.position, fireDirection);
        Debug.DrawRay(StartFiringPosition.position, fireDirection * 10, Color.red);
        Destroy(plasmaShot_L, 2.0f);
        Debug.Log("Shoot");
    }
    // ����� ��� ������ ��� ����������� ��������
    public override void WeaponFire(bool isFiring, Transform trgt)
    {
        _isFiring = isFiring;

        if (_isFiring)
        {
            transform.LookAt(trgt);
            // ���������, ������ �� ���������� ������� � ���������� ��������
            if (Time.time >= _lastShotTime + FiringTemp)
            {
                Shoot(); // ��������� �������
                _lastShotTime = Time.time; // ��������� ����� ���������� ��������
            }
            Debug.Log("is firing");
        }
        else
        {
            Debug.Log("is not firing");
        }
    }
}