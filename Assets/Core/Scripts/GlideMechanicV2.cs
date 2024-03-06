using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlidingSystem : MonoBehaviour
{
    [SerializeField] private float BaseSpeed = 10f;       // A moderate speed that the character starts gliding at.
    [SerializeField] private float MaxThrustSpeed = 25f;   // The max speed the character can reach while gliding.
    [SerializeField] private float MinThrustSpeed = 1f;    // The minimum speed needed to maintain glide.
    [SerializeField] private float ThrustFactor = 0.5f;    // How much thrust the character gets from gliding.
    [SerializeField] private float DragFactor = 0.05f;     // The drag applied to slow down the character.
    [SerializeField] private float MinDrag = 0.01f;        // The minimum drag applied.
    [SerializeField] private float RotationSpeed = 2f;     // How quickly the character can rotate while gliding.
    [SerializeField] private float TiltStrength = 20f;     // The banking effect when rotating.
    [SerializeField] private float LowPercent = 0.2f;      // For steep climbs, less thrust.
    [SerializeField] private float HighPercent = 1f;       // For normal flight, full thrust.

    private float CurrentThrustSpeed; // This will be modified at runtime and should start at BaseSpeed.
    private float TiltValue; // Used for the tilt effect when turning.
    private float LerpValue; // Used for smoothing out the tilt back to neutral.

    private Transform CameraTransform;
    private CharacterController characterController;
    public bool IsGliding { get; private set; }

    private void Start()
    {
        CameraTransform = GameObject.Find("PlayerCameraRoot").transform; // Make sure this matches the exact name in the hierarchy
        characterController = GetComponent<CharacterController>();
        IsGliding = false;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.X) && !characterController.isGrounded)
        {
            IsGliding = true;
        }

        if (characterController.isGrounded)
        {
            IsGliding = false;
        }

        if (IsGliding)
        {
            ManageRotation();
            GlidingMovement();
        }
    }

    private void GlidingMovement()
    {
        float pitchInDeg = transform.eulerAngles.x % 360;
        float pitchInRads = transform.eulerAngles.x * Mathf.Deg2Rad;
        float mappedPitch = -Mathf.Sin(pitchInRads);
        float accelerationPercent = pitchInDeg >= 300f ? LowPercent : HighPercent;
        Vector3 glidingDirection = transform.forward + new Vector3(0, mappedPitch * accelerationPercent, 0);
        Vector3 glidingForce = glidingDirection.normalized * CurrentThrustSpeed * Time.deltaTime;

        CurrentThrustSpeed = Mathf.Clamp(CurrentThrustSpeed + mappedPitch * accelerationPercent * ThrustFactor * Time.deltaTime, 0, MaxThrustSpeed);

        if (CurrentThrustSpeed >= MinThrustSpeed)
        {
            characterController.Move(glidingForce);
        }
        else
        {
            CurrentThrustSpeed = 0;
        }
    }

    private void ManageRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        TiltValue += mouseX * TiltStrength;

        if (mouseX == 0)
        {
            TiltValue = Mathf.Lerp(TiltValue, 0, LerpValue);
            LerpValue += Time.deltaTime;
        }
        else
        {
            LerpValue = 0;
        }

        Quaternion targetRotation = Quaternion.Euler(CameraTransform.eulerAngles.x, CameraTransform.eulerAngles.y, TiltValue);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
    }
}
