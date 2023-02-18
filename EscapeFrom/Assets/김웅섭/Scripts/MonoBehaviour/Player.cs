using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;

    [Header("Death")]
    public bool isDeath;
    [Header("Move")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpScale;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float stamina; //max == 100;
    [SerializeField] private float increaseStamina;
    [SerializeField] private float decreaseStamina;
    public bool isRunning;
    [Header("Move Sound")]
    [SerializeField] private float walkSoundTime;
    [SerializeField] private float runSoundTime;
    [Header("Ground")]
    [SerializeField] private float groundCastRadius;
    [SerializeField] private Vector3 groundCastOffset;
    [SerializeField] private bool isGround;
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
    [SerializeField] private float photoDist;
    public bool hasHandCam;
    [SerializeField] private bool isZoom;
    [Header("Hide")]
    public bool isHide = false;
    [Header("Inventory")]
    public List<Item> items;

    public static List<Item> Items { get { return player.items; } }
    public static float Stamina { get { return player.stamina; } }

    private CharacterController cc;
    private float currnetHeadHobAmplitude; //walk : 0.005 run : 0.0075
    private float currentHeadHobFrequency;  //walk : 12 run : 18
    private float YVelocity;
    private float MouseAngleY;
    private float headHobToggleSpeed = 1f;
    private float staminaRecoveryTime;
    private float currentMoveSoundTime;
    private Vector3 headStartPos;
    private Vector3 handCamStartPos;

    public void SetHeadHob(bool value)
    {
        headHobEnabled = value;
    }

    private void Awake()
    {
        player = this;

        cc = GetComponent<CharacterController>();
        headStartPos = _camera.localPosition;

        handCamStartPos = handCamera.transform.localPosition;
        stamina = 100;
    }

    private void Update()
    {
        GetSettingValue();
        EventHandle();
        CameraRotation();
        Interact();
        Move();
        MoveSound();
        HandCamera();
        HeadHob();
    }

    private void GetSettingValue()
    {
        mouseSensivity.x = SettingUI.SettingInfo.mouseSensivity.x;
        mouseSensivity.y = SettingUI.SettingInfo.mouseSensivity.y;
    }

    private void EventHandle()
    {
        //criteria
        if (GameManager.manager.isEnd) return;
        if (!StartMenuUI.IsStart) return;
        if (EscUI.IsShowing) return;
        if (SettingUI.IsShowing) return;
        if (InventoryUI.ShowType != InventoryUIShowType.disable) return;
        if (isDeath) return;

        if (Input.GetKeyDown(KeyCode.Escape)) EscUI.ActiveUI(true);
        if (Input.GetKeyDown(KeyCode.Q)) InventoryUI.ShowUI();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        isGround = Physics.CheckSphere(transform.position + groundCastOffset, groundCastRadius, LayerMask.GetMask("Ground"));

        //criteria
        if (
            EscUI.IsShowing ||
            SettingUI.IsShowing ||
            InventoryUI.ShowType != InventoryUIShowType.disable ||
            !StartMenuUI.IsStart ||
            !cc.enabled ||
            isDeath ||
            GameManager.manager.isEnd
        )
        {
            h = 0;
            v = 0;
            isGround = false;
        }

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

            if (staminaRecoveryTime > 0) staminaRecoveryTime -= Time.deltaTime;
            else
            {
                stamina += increaseStamina * Time.deltaTime;
                stamina = Mathf.Clamp(stamina, 0, 100);
            }
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

        if(cc.enabled) cc.Move(dir * Time.deltaTime);
    }

    private void CameraRotation()
    {
        //criteria
        if (GameManager.manager.isEnd) return;
        if (!StartMenuUI.IsStart) return;
        if (EscUI.IsShowing) return;
        if (SettingUI.IsShowing) return;
        if (InventoryUI.ShowType != InventoryUIShowType.disable) return;
        if (isDeath) return;

        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;

        MouseAngleY -= mouseY * mouseSensivity.y * 100;
        MouseAngleY = Mathf.Clamp(MouseAngleY, -85, 80);
        cameraHolder.localRotation = Quaternion.Euler(MouseAngleY, 0, 0);

        transform.Rotate(new Vector3(0, mouseX * mouseSensivity.x * 100, 0));
    }

    private void Interact()
    {
        RaycastHit hitInfo;
        Physics.Raycast(
            cameraHolder.transform.position,
            cameraHolder.transform.forward, out hitInfo,
            interactRayDistance,
            ~LayerMask.GetMask("Player", "Ignore Raycast")
            );

        if (hitInfo.collider != null)
        {
            IInteractable interactable;
            if (hitInfo.collider.TryGetComponent<IInteractable>(out interactable))
            {
                interactable.ShowUI();

                //criteria
                if (
                    !EscUI.IsShowing &&
                    !SettingUI.IsShowing &&
                    InventoryUI.ShowType == InventoryUIShowType.disable &&
                    StartMenuUI.IsStart &&
                    !isDeath
                )
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interactable.Interact();
                    }
                }
            }
            else InteractUI.ControlUI(false, "");
        }
        else InteractUI.ControlUI(false, "");
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

        if (!cc.enabled) return;
        if (speed < headHobToggleSpeed) return;
        if (!cc.isGrounded) return;

        PlayHeadHobMotion(FootStepMotion());
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * (isRunning ? 18 : 12)) * (isRunning ? 0.0075f : 0.005f);
        pos.x += Mathf.Cos(Time.time * (isRunning ? 18 : 12) / 2) * (isRunning ? 0.0075f : 0.005f) * 2;
        return pos * Time.deltaTime * 100;
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

    private void MoveSound()
    {
        var vel = new Vector3(cc.velocity.x, 0, cc.velocity.z).magnitude;

        if (cc.enabled && vel > 1 && isGround)
            if (currentMoveSoundTime > (isRunning ? runSoundTime : walkSoundTime))
            {
                var random = Random.Range(0, 9);

                SoundManager.PlaySound("ConcreteWalk", random, 0.3f, transform.position);
                currentMoveSoundTime = 0;
            }
            else
            {
                currentMoveSoundTime += Time.deltaTime;
            }
    }

    private void HandCamera()
    {
        if(!hasHandCam)
        {
            handCamera.SetActive(false);
            return;
        }
        handCamera.SetActive(true);

        var velocity = new Vector3(cc.velocity.x, 0, cc.velocity.z);

        if (cc.enabled && cc.isGrounded && velocity.magnitude >= 1)
        {
            Vector3 pos = Vector3.zero;
            pos.y -= Mathf.Sin(Time.time * (isRunning ? 18 : 12)) * (isRunning ? 0.0017f : 0.001f);
            pos.x -= Mathf.Cos(Time.time * (isRunning ? 18 : 12) / 2) * (isRunning ? 0.0017f : 0.001f) * 2;
            handCamera.transform.localPosition += pos * Time.deltaTime * 100;
        }

        //criteria
        if (GameManager.manager.isEnd) return;
        if (!StartMenuUI.IsStart) return;
        if (EscUI.IsShowing) return;
        if (SettingUI.IsShowing) return;
        if (InventoryUI.ShowType != InventoryUIShowType.disable) return;
        if (isDeath) return;

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
            if (Vector3.Distance(handCamera.transform.localPosition, handCamZoomPos) < 0.02f && Input.GetMouseButtonDown(0))
            {
                Debug.Log("Take!");
                foreach (var collection in CollectionManager.manager.collections)
                {
                    if (collection.IsVisible(handCameraLens) && Vector3.Distance(handCamera.transform.position, collection.transform.position) < photoDist)
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
        Gizmos.DrawLine(cameraHolder.transform.position, cameraHolder.transform.position + cameraHolder.transform.forward * (0.5f + interactRayDistance));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(handCamera.transform.position, photoDist);
    }
}
