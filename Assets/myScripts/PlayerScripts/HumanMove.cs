using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanMove : MonoBehaviour
{
    HumanActions _humanActions;
    NavMeshAgent _agent;
    [SerializeField] LayerMask clickableMasks;
    float lookRotSpeed = 8.0f;

    private void Awake()
    {
                _agent = GetComponent<NavMeshAgent>();
        _humanActions = new HumanActions();
        AssignInputs();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    void AssignInputs()
    {
        _humanActions.HumMain.HumMove.performed += ctm => ClickToMove();
    }
    void ClickToMove()
    {
        Debug.Log("ctm Human CTM method");
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableMasks))
        {
            _agent.destination = hit.point;
        }
    }
    private void OnEnable()
    {
        _humanActions.Enable();
    }
    private void OnDisable()
    {
        _humanActions.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
