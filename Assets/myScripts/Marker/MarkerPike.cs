using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MarkerPike : MonoBehaviour
{
    Vector3 _startPos;
    Vector3 _endPos;
    sbyte direction = 1;

    public float speed = 2.1f;
    public float path = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _endPos = _startPos + new Vector3(0, path, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == 1)
        {

            if (transform.position.y <= _endPos.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                direction = -1;
                _startPos = _endPos;
                _endPos = new Vector3(_endPos.x, _startPos.y - path, _endPos.z);
            }
        }
        else
        {
            if (transform.position.y >= _endPos.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                direction = 1;
                _startPos = _endPos;
                _endPos = new Vector3(_endPos.x, _startPos.y + path, _endPos.z);
            }
        }
    }
}

/*
 
 */