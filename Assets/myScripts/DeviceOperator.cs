using UnityEngine;

/// <summary>
/// ����� ��������� ����������� ������ ��� ���������� ���������� �����������
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
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius); //���������� ������ ��������� ��������

            foreach (Collider collider in hitColliders) 
            {
                Vector3 hitPosition = collider.transform.position;
                hitPosition.y = transform.position.y; //������������ ���������, ����� ����������� �� ���� ������������� ����� ��� ����

                Vector3 direction = hitPosition - transform.position;
                if (Vector3.Dot(transform.forward, direction.normalized) > 0.5f)
                {
                    collider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver); //����� �������� ������� ���������� ������� ���������� �� ���� �������� �������    
                }         
            }
        }
    }
}
