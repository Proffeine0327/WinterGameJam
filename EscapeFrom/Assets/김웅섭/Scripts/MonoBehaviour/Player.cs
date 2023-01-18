using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player player;

    [Header("Move")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpScale;
    [SerializeField] private float gravityMultiplier;
    [Header("Ground")]
    [SerializeField] private float groundCastRadius;
    [SerializeField] private Vector3 groundCastOffset;
    [Header("Mouse")]
    [SerializeField] private Vector2 mouseSensivity;
    [Header("HeadHob")]
    [SerializeField] private bool headHobEnabled;
    [SerializeField, Range(0, 0.1f)] private float headHobAmplitude;
    [SerializeField, Range(0, 30)] private float headHobFrequency;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform cameraHolder;

    private CharacterController cc;
    private float YVelocity;
    private float MouseAngleY;
    private float headHobToggleSpeed = 3.0f;
    private Vector3 headStartPos;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = this;

        cc = GetComponent<CharacterController>();

        headStartPos = _camera.localPosition;
    }

    private void Update()
    {
        HeadHob();
        CameraMove();
        Move();
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
        pos.y += Mathf.Sin(Time.time * headHobFrequency) * headHobAmplitude;
        pos.x += Mathf.Cos(Time.time * headHobFrequency / 2) * headHobAmplitude * 2;
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

    private void CameraMove()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;

        MouseAngleY -= mouseY * mouseSensivity.y;
        MouseAngleY = Mathf.Clamp(MouseAngleY, -90, 80);
        cameraHolder.localRotation = Quaternion.Euler(MouseAngleY, 0, 0);

        transform.Rotate(new Vector3(0, mouseX * mouseSensivity.x, 0));
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        var dir = new Vector3(h, 0, v);
        dir = transform.TransformDirection(dir).normalized;
        dir *= moveSpeed;

        if (cc.isGrounded) YVelocity = 0;
        else YVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.CheckSphere(transform.position + groundCastOffset, groundCastRadius, LayerMask.GetMask("Ground")))
            {
                YVelocity = jumpScale;
            }
        }
        dir.y = YVelocity;

        cc.Move(dir * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + groundCastOffset, groundCastRadius);
    }
}
