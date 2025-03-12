using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPScript : MonoBehaviour
{
    public int HPValue = 10000;
    public Slider HPSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        Debug.Log($"{HPValue}");
        int dmg = Random.Range(5, 15);
        GetDamage(dmg);
    }

    public void GetDamage(int dmgValue)
    {
        HPValue -= dmgValue;
        HPSlider.value = HPValue;
        if (HPValue <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void BulletPenetration()
    {
        int dmg = Random.Range(5, 15);
        GetDamage(dmg);
    }
    private void OnCollisionEnter(Collision collision)
    {
        BulletPenetration();
        if (collision.collider.transform.CompareTag("Bullet"))
        {
            Debug.Log("bullet");
            Destroy(collision.gameObject);  
        }
    }
}
