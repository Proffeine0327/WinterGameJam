using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;

    [Header("Move")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpScale;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float stamina; //max : 100;
    [SerializeField] private float increaseStamina;
    [SerializeField] private float decreaseStamina;
    public bool isRunning;
    [Header("Ground")]
    [SerializeField] private float groundCastRadius;
    [SerializeField] private Vector3 groundCastOffset;
    [Header("Mouse")]
    [SerializeField] private Vector2 mouseSensivity;
    [Header("HeadHob")]
    [SerializeField] private bool headHobEnabled;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform cameraHolder;
    [Header("Interact")]
    [SerializeField] private bool isLookingCollection;
    [SerializeField] private float interactRayDistance;
    [Header("HandCamera")]
    [SerializeField] private GameObject handCamera;
    [SerializeField] private Camera handCameraLens;
    [SerializeField] private Vector3 handCamZoomPos;
    [SerializeField] private bool isZoom;

    public float Stamina { get { return stamina; } }

    private CharacterController cc;
    private float currnetHeadHobAmplitude; //walk : 0.005 run : 0.0075
    private float currentHeadHobFrequency;  //walk : 12 run : 18
    private float YVelocity;
    private float MouseAngleY;
    private float headHobToggleSpeed = 1f;
    private float staminaRecoveryTime;
    private Vector3 headStartPos;
    private Vector3 handCamStartPos;

    public bool isHide = false;

    public void SetHeadHob(bool value)
    {
        headHobEnabled = value;
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = this;

        cc = GetComponent<CharacterController>();
        headStartPos = _camera.localPosition;

        handCamStartPos = handCamera.transform.localPosition;
        stamina = 100;
    }

    private void Update()
    {
        HeadHob();
        CameraRotation();
        Interact();
        Move();
        HandCamera();
    }

    private void Move()
    {
        if (isLookingCollection) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        bool isGround = Physics.CheckSphere(transform.position + groundCastOffset, groundCastRadius, LayerMask.GetMask("Ground"));

        var dir = new Vector3(h, 0, v);
        dir = transform.TransformDirection(dir).normalized;

        if (Input.GetKey(KeyCode.LeftShift) && isGround && v == 1 && stamina >= 0) isRunning = true;
        else isRunning = false;

        if (isRunning)
        {
            dir *= runSpeed;
            stamina -= decreaseStamina * Time.deltaTime;
            staminaRecoveryTime = 0.25f;
        }
        else
        {
            dir *= walkSpeed;
            if (staminaRecoveryTime <= 0)
            {
                stamina += increaseStamina * Time.deltaTime;
                stamina = Mathf.Clamp(stamina, 0, 100);
            }
            else staminaRecoveryTime -= Time.deltaTime;
        }


        if (cc.isGrounded) YVelocity = 0;
        else YVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                YVelocity = jumpScale;
            }
        }
        dir.y = YVelocity;

        cc.Move(dir * Time.deltaTime);
    }

    private void CameraRotation()
    {
        if (isLookingCollection) return;

        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;

        MouseAngleY -= mouseY * mouseSensivity.y;
        MouseAngleY = Mathf.Clamp(MouseAngleY, -85, 80);
        cameraHolder.localRotation = Quaternion.Euler(MouseAngleY, 0, 0);

        transform.Rotate(new Vector3(0, mouseX * mouseSensivity.x, 0));
    }

    private void Interact()
    {
        RaycastHit hitInfo;
        Physics.Raycast(
            cameraHolder.transform.position + cameraHolder.transform.forward * 1,
            cameraHolder.transform.forward, out hitInfo,
            interactRayDistance,
            ~LayerMask.GetMask("Player")
            );

        if (hitInfo.collider != null)
        {
            IInteractable interactable;
            if (hitInfo.collider.TryGetComponent<IInteractable>(out interactable))
            {
                interactable.ShowUI();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }
        else
        {
            if(!isHide)
                InteractUI.ControlUI(false, "");
        }
    }

    private void HeadHob()
    {
        if (!headHobEnabled) return;

        CheckHeadHobMotion();
        ResetHeadPosition();
        _camera.LookAt(FocusTarget());
    }

    private void PlayHeadHobMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }

    private void CheckHeadHobMotion()
    {
        float speed = new Vector3(cc.velocity.x, 0, cc.velocity.z).magnitude;

        if (speed < headHobToggleSpeed) return;
        if (!cc.isGrounded) return;

        PlayHeadHobMotion(FootStepMotion());
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * (isRunning ? 18 : 12)) * (isRunning ? 0.0075f : 0.004f);
        pos.x += Mathf.Cos(Time.time * (isRunning ? 18 : 12) / 2) * (isRunning ? 0.0075f : 0.004f) * 2;
        return pos;
    }

    private void ResetHeadPosition()
    {
        if (_camera.localPosition == headStartPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, headStartPos, Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolder.localPosition.y, transform.position.z);
        pos += cameraHolder.forward * 15f;
        return pos;
    }

    private void HandCamera()
    {
        if (Input.GetMouseButton(1))
        {
            if (cc.velocity.magnitude < 1) isZoom = true;
            else isZoom = false;
        }
        else
        {
            isZoom = false;
        }

        if (isZoom)
        {
            handCamera.transform.localPosition = Vector3.Lerp(handCamera.transform.localPosition, handCamZoomPos, Time.deltaTime * 7);
            if (Vector3.Distance(handCamera.transform.localPosition, handCamZoomPos) < 0.05f && Input.GetMouseButtonDown(0))
            {
                Debug.Log("Take!");
                foreach (var collection in CollectionManager.manager.collections)
                {
                    if (collection.IsVisible(handCameraLens))
                    {
                        collection.GetComponent<IPhotoable>()?.Take();
                        Debug.Log($"Get {collection.name}");
                    }
                }
            }
        }
        else handCamera.transform.localPosition = Vector3.Lerp(handCamera.transform.localPosition, handCamStartPos, Time.deltaTime * 7);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + groundCastOffset, groundCastRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(cameraHolder.transform.position + cameraHolder.transform.forward * 1, cameraHolder.transform.position + cameraHolder.transform.forward * (1 + interactRayDistance));
    }
}
