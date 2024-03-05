using UnityEngine;

public class AdvancedSheepGlide : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float baseGlideSpeed = 5f;
    [SerializeField] private float tiltIntensity = 5f; // How much the sheep will tilt
    [SerializeField] private float tiltLerpSpeed = 5f; // Speed of tilting transition
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float glideGravityScale = 0.2f; // Reduced gravity scale during glide
    [SerializeField] private float groundApproachGravityScale = 0.1f; // Further reduced gravity scale when close to the ground
    [SerializeField] private float groundDetectionDistance = 10f; // Distance to start reducing gravity further
    [SerializeField] private LayerMask groundLayer; // Layer mask to detect the ground

    private bool isGliding = false;
    private float currentGlideSpeed;

    void Update()
    {
        if (characterController.isGrounded && isGliding)
        {
            isGliding = false;
            Debug.Log("I hit the ground - animation"); // Placeholder for landing animation
        }

        if (!characterController.isGrounded && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartGlide();
        }

        if (isGliding)
        {
            Glide();
        }
        else
        {
            ResetTilt();
        }
    }

    private void StartGlide()
    {
        isGliding = true;
        currentGlideSpeed = baseGlideSpeed;
    }

    private void Glide()
    {
        float gravity = CalculateDynamicGravity();

        // Apply movement with dynamic gravity
        Vector3 glideDirection = cameraTransform.forward * currentGlideSpeed + Vector3.up * gravity;
        characterController.Move(glideDirection * Time.deltaTime);

        // Apply tilting based on camera direction
        ApplyTilt();
    }

    private float CalculateDynamicGravity()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundDetectionDistance, groundLayer))
        {
            // Closer to the ground, reduce gravity
            return Physics.gravity.y * groundApproachGravityScale;
        }
        // Standard reduced gravity during glide
        return Physics.gravity.y * glideGravityScale;
    }

    private void ApplyTilt()
    {
        Vector3 targetEulerAngles = cameraTransform.eulerAngles;
        targetEulerAngles.x *= tiltIntensity; // Apply tilt intensity to x-axis rotation
        targetEulerAngles.z = 0; // Keep z-axis rotation zero to avoid roll

        // Smoothly interpolate to the target tilt to simulate animated tilt
        Vector3 currentEulerAngles = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetEulerAngles), Time.deltaTime * tiltLerpSpeed).eulerAngles;
        // Apply tilting within constrained angles to prevent flipping
        transform.rotation = Quaternion.Euler(currentEulerAngles.x, targetEulerAngles.y, currentEulerAngles.z);
    }

    private void ResetTilt()
    {
        if (!isGliding)
        {
            // Gradually reset tilt when not gliding
            Quaternion resetRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, resetRotation, Time.deltaTime * tiltLerpSpeed);
        }
    }
}
