using System.Collections;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class PlayerAI_Controller : MonoBehaviour
{
    string IdleNameAnim = "Idle";
    string RunNameAnim = "Run";
    string FastRunNameAnim = "Fast Run";
    string RifleRunNameAnim = "Rifle Run";
    string FiringRifleAnimName = "Firing Rifle";
    RaycastHit hit;

    PlayerActions _playerActions;
    NavMeshAgent _agent;
    Animator _animator;
    GameObject _marker;
    Vector3 _previousDestinationVector;
    [SerializeField] Transform _weaponTransform;
    Inventory _inventoryComponent;

    [Header("Movement")]
    [SerializeField] Camera _camera;
    [SerializeField] ParticleSystem ClickEffect;
    [SerializeField] LayerMask Clickableayers;
    [SerializeField] GameObject ClickPrefab;

    [Header("For Tests")]
    public string gameMode;
    public InventoryObject inventory;

    float _lookRotationSpeed = 9.0f;


    private GameModeChanger _gameModeChanger;
    WeaponManager _weaponManager;

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item != null)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
            Debug.Log($"the {other.gameObject.name} in inventory");
        }
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();

        #region for inventory
        inventory.slots.Clear();
        #endregion

        #region for marker
        _previousDestinationVector = new Vector3(0, 0, 0);
        #endregion

        #region for ai_navigation
        _agent = GetComponent<NavMeshAgent>();
        _playerActions = new PlayerActions();
        

        AssignInputs(); //какой-то встроенный метод в AI Navigation
        #endregion

        _gameModeChanger = GameManager.instance._gameModeChanger;

    }
    // Start is called before the first frame update
    void Start()
    {
        _weaponManager = GetComponent<WeaponManager>();
        _inventoryComponent = GetComponent<Inventory>();
        //Debug.Log("AI_start method");
    }
    void AssignInputs()
    {
        _playerActions.MainPlayerMap.PlayerMove.performed += ctm => ClickToMove();
    }
    void ClickToMove()
    {
        //Debug.Log("Click to move");
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, Clickableayers))
        {
            if (hit.collider == null)
            {
                Debug.Log("hit collider is null");
            }

            if (hit.collider.transform.CompareTag("Ground"))
            {
                // Устанавливаем новую точку назначения в любом случае
                _agent.destination = hit.point;
            }
            else if (hit.collider.transform.CompareTag("Obstacle"))
            {
                // Получаем размеры коллайдера
                Bounds bounds = hit.collider.bounds;

                // Вычисляем половину размера по оси X
                float halfSizeX = bounds.size.x / 2f;

                // Добавляем величину B (заменить на нужное значение)
                float offset = halfSizeX - 1f; // Здесь 1f - это примерная величина B

                // Вычисляем новую позицию, смещенную от центра объекта
                Vector3 newPosition = hit.collider.transform.position + hit.collider.transform.right * offset;
                _agent.destination = newPosition;
                if (_agent.transform.position == newPosition)
                {                    
                    Debug.Log("_agent.destination = newPosition");
                }
            }

            // Создаем новый экземпляр сферы в точке попадания, если маркер еще не существует
            if (_marker == null)
            {
                if (!hit.collider.transform.CompareTag("Obstacle"))
                {
                    _marker = Instantiate(ClickPrefab, hit.point, Quaternion.identity);
                }
            }
            else
            {
                if (!hit.collider.transform.CompareTag("Obstacle"))
                {
                    // Удаляем предыдущий маркер
                    StartCoroutine(DestroyMarkerPrefabAfterDelay(_marker, 0.10f));
                    _marker = Instantiate(ClickPrefab, hit.point, Quaternion.identity);
                }
            }
        }
    }

    private void OnEnable()
    {
        _playerActions.Enable();
    }
    private void OnDisable()
    {
        _playerActions.Disable();
    }

    void SetAnimation()
    {
        if (_agent.velocity == Vector3.zero)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                //_animator.Play(FiringRifleAnimName);
            }
            else
            {
                _animator.StopPlayback();
                _animator.Play(IdleNameAnim);
            }
        }
        else
        {
            if (_gameModeChanger.gameMode == "FreeWalkMode")
            {
                _agent.speed = 3.5f;
                _animator.Play(RifleRunNameAnim);
                //_animator.Play(RunNameAnim);
            }
            if (_gameModeChanger.gameMode == "CombatMode")
            {
                _agent.speed = 8.4f;
                _animator.Play(FastRunNameAnim);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        FaceTarget();
        SetAnimation();
        CurrWeaponTransform();

        // Убираем маркер, если игрок достаточно близко
        if (_marker != null && Vector3.Distance(transform.position, _marker.transform.position) < 0.6f)
        {
            Destroy(_marker); // Удаляем маркер немедленно
            _marker = null; // Устанавливаем _marker в null, чтобы избежать повторного удаления
        }

        if (Input.GetKey(KeyCode.Mouse1) && _agent.velocity == Vector3.zero)
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 100, Clickableayers))
            {
                LookAtInteractable();
                switch (raycastHit.collider.tag)
                {
                    case ("Enemies"):
                         Debug.Log($"raycastHit.collider.tag {raycastHit.collider.tag}");
                        _weaponManager.isFiring = true;
                        _weaponManager.target = raycastHit.transform;
                        _animator.Play(FiringRifleAnimName);
                        //Attack(_weaponManager.isFiring);
                        break;
                    case ("Boxes"):
                        Debug.Log($"raycastHit.collider.tag {raycastHit.collider.tag}");
                        break;
                    default:
                        _weaponManager.isFiring = false;
                        _weaponManager.target = null;
                        break;
                }
            }
            //Attack(_weaponManager.isFiring);
        }
        else
        {
            _weaponManager.isFiring = false;
            _weaponManager.target = null;
            //Attack(_weaponManager.isFiring);
        }

    }
    //void Attack(bool isFiring)
    //{
    //    IWeapon weapon = null;
    //    foreach (Transform childWeapon in this.transform)
    //    {
    //        if (childWeapon.CompareTag("Weapon") && childWeapon.gameObject.activeSelf == true)
    //        {                
    //            weapon = childWeapon.GetComponent<IWeapon>();
    //            weapon.WeaponFire(isFiring); // Передаем true для начала стрельбы
    //            Debug.Log($"weapon {weapon}");
    //        }
    //    }
    //}

    #region firing zone
    private void CurrWeaponTransform()
    {
        foreach (Transform childWeapon in this.transform)
        {
            if (childWeapon.CompareTag("Weapon") && childWeapon.gameObject.activeSelf == true)
            {
                childWeapon.position = _weaponTransform.position;
                childWeapon.forward = _weaponTransform.forward;
            }
        }
    }

    private bool IsFiring()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 100, Clickableayers))
            {
                LookAtInteractable();
                switch (raycastHit.collider.tag)
                {
                    case ("Enemies"):
                        //Debug.Log($"raycastHit.collider.tag {raycastHit.collider.tag}");
                        _weaponManager.isFiring = true;
                        return _weaponManager.isFiring;
                        //Attack(_weaponManager.isFiring);
                    default:
                        _weaponManager.isFiring = false;
                        return _weaponManager.isFiring;
                }                
            }
            //_weaponManager.isFiring = true;
            //Attack(_weaponManager.isFiring);
        }
        return false;
    }
    #endregion
    void FaceTarget()
    {
        if (_agent.destination == transform.position)
        {
            return;
        }
        Vector3 facing = Vector3.zero;
        facing = _agent.destination;

        Vector3 direction = (facing - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _lookRotationSpeed);
    }

    IEnumerator DestroyMarkerPrefabAfterDelay(GameObject marker, float delay)
    {
        yield return new WaitForSeconds(delay); // Ждем указанное время
        if (marker != null) // Проверяем, не был ли объект уже удален
        {
            DestroyImmediate(marker, true); // Удаляем сферу
        }
    }

    void LookAtInteractable()
    {
        RaycastHit hitMouse1;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitMouse1, 100, Clickableayers))
        {
            if (hitMouse1.collider != null/* && hitMouse1.collider.CompareTag("Enemy")*/)
            {
                Vector3 enemyPosition = hitMouse1.collider.transform.position;
                Vector3 direction = (enemyPosition - transform.position).normalized;
                Quaternion rotationToEnemy = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationToEnemy, Time.deltaTime * _lookRotationSpeed);
            }
        }
    }
}
