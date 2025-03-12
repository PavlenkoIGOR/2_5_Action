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
            //Debug.Log(string.Format("Объект на сцене: {0}", holoPrefab.gameObject));
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
            holoPrefab.transform.localPosition = new Vector3(1f, 0, 0); // Сдвигаем на -1 по оси X относительно родителя
            holoPrefab.transform.localRotation = Quaternion.Euler(0, -90, 0); // Поворот на 90 градусов вокруг оси Y
            if (renderer != null)
            {
                renderer.enabled = true;
            }
        }
        if (transform.name.EndsWith("Left"))
        {
            Debug.Log("Is left trigger");
            holoPrefab.transform.SetParent(transform);
            holoPrefab.transform.localPosition = new Vector3(-1f, 0, 0); // Сдвигаем на +1 по оси X относительно родителя
            holoPrefab.transform.localRotation = Quaternion.Euler(0, 90, 0); // Обратно к нормальному положению
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
