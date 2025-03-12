using UnityEngine;

/// <summary>
/// класс реализует урпавл€ющую кнопку дл€ управлени€ поблизости устройством
/// </summary>
public class DeviceOperator : MonoBehaviour
{
    public float Radius = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius); //возвращает список ближайших объектов

            foreach (Collider collider in hitColliders) 
            {
                Vector3 hitPosition = collider.transform.position;
                hitPosition.y = transform.position.y; //вертикальна€ коррекци€, чтобы направление не было ориентировано вверх или вниз

                Vector3 direction = hitPosition - transform.position;
                if (Vector3.Dot(transform.forward, direction.normalized) > 0.5f)
                {
                    collider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver); //метод пытаетс€ вызвать именованую функцию независимо от типа целевого объекта    
                }         
            }
        }
    }
}
