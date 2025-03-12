using UnityEngine;


public class SneakableObstacle : MonoBehaviour
{
    GameObject holoPrefab;
    Renderer renderer;

    private void Start()
    {
        holoPrefab = GameObject.Find("SM_Military_1_sneak");
        if (holoPrefab != null)
        {
            //Debug.Log(string.Format("������ �� �����: {0}", holoPrefab.gameObject));
            renderer = holoPrefab.GetComponent<Renderer>();
        }

    }
    private void OnMouseEnter()
    {
        string transformName = transform.name;        

        if (transform.name.EndsWith("Right"))
        {
            Debug.Log("Is right trigger");
            holoPrefab.transform.SetParent(transform);
            holoPrefab.transform.localPosition = new Vector3(1f, 0, 0); // �������� �� -1 �� ��� X ������������ ��������
            holoPrefab.transform.localRotation = Quaternion.Euler(0, -90, 0); // ������� �� 90 �������� ������ ��� Y
            if (renderer != null)
            {
                renderer.enabled = true;
            }
        }
        if (transform.name.EndsWith("Left"))
        {
            Debug.Log("Is left trigger");
            holoPrefab.transform.SetParent(transform);
            holoPrefab.transform.localPosition = new Vector3(-1f, 0, 0); // �������� �� +1 �� ��� X ������������ ��������
            holoPrefab.transform.localRotation = Quaternion.Euler(0, 90, 0); // ������� � ����������� ���������
            {
                renderer.enabled = true;
            }
        }
    }
    private void OnMouseExit()
    {
        renderer.enabled = false;
    }
}
