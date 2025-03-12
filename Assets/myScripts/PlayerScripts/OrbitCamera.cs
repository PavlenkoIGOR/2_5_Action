using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Vector3 Offset = new Vector3(-5.0f ,-5.0f, 5.0f);
    [SerializeField] Transform Target;
    [SerializeField] float mouseSens = 5.0f;
    [SerializeField] float mouseScrollSensitivity = 2.0f;

    float _currentAngleY;
    float _currentAngleX;
    float zOffset;
    [SerializeField] float minDistance = 1.0f; // Минимальное расстояние от камеры до цели
    [SerializeField] float maxDistance = 10.0f; // Максимальное расстояние от камеры до цели
    private void Awake()
    {

    }
    void Start()
    {
        GameManager.instance.gameControls.MouseSensetivity = mouseSens;
        if (GameManager.instance == null)
        {
            Debug.Log("GameManager.instance = null");
        }
        else if (GameManager.instance.gameControls == null)
        {
            Debug.Log("GameManager.instance.gameControls = null");
        }

        transform.position = Target.position - Offset;
        transform.LookAt(Target);

        _currentAngleY = 0; //иначе камера будет под определенным углом относительно глобальных осей в начале старта
        _currentAngleX = 0;
    }


    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            //Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float inputMouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime * 20.0f;
            float inputMouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime * 20.0f;
            _currentAngleY -= inputMouseX;
            _currentAngleX += inputMouseY;

            Quaternion rotation = Quaternion.Euler(_currentAngleX, _currentAngleY, 0);
            transform.position = Target.position - (rotation * Offset);
            transform.LookAt(Target);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Quaternion rotation = Quaternion.Euler(_currentAngleX, _currentAngleY, 0);
            transform.position = Target.position - (rotation * Offset);
            transform.LookAt(Target);
            //StartCameraPos = transform.position;
        }
    }
}