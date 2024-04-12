using UnityEngine;
using UnityEngine.Events;

public class RobotController : MonoBehaviour
{
    public float tileSize = 1.0f;
    public CharacterController controller; // Ensure this is the Unity CharacterController component if used for movement
    public float rotationSpeed = 90f; // Degrees per second, adjust as needed
    private Animator animator;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private Quaternion targetRotation; // Target rotation
    private bool isRotating = false; // Flag to indicate if currently rotating
    public UnityEvent onPlayerEnterLaser;

    private void Start()
    {
        Cursor.visible = true; // Make the cursor visible
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor so it can move freely
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        targetRotation = transform.rotation; // Initialize target rotation
    }

    void Update()
    {
        Cursor.visible = true; // Make the cursor visible
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor so it can move freely
        // Removed the keyboard input from here to keep rotation and movement logic separate

        // Smoothly rotate towards the target rotation
        if (isRotating)
        {
            RotateTowardsTarget();
        }

        // Handle the movement
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    public void MoveUp()
    {
        targetPosition += transform.forward * tileSize;
        BeginMovement();
    }

    public void MoveDown()
    {
        targetPosition -= transform.forward * tileSize;
        BeginMovement(isBackward: true);
    }

    public void RotateLeft()
    {
        targetRotation *= Quaternion.Euler(0, -90, 0);
        BeginRotation(isLeft: true);
    }

    public void RotateRight()
    {
        targetRotation *= Quaternion.Euler(0, 90, 0);
        BeginRotation();
    }

    private void BeginMovement(bool isBackward = false)
    {
        if (!isMoving) // Start moving if not already doing so
        {
            isMoving = true;
            animator.SetBool("isMoving", true);
            animator.SetBool("isBack", isBackward);
            StartCoroutine(MoveToPosition(targetPosition));
        }
    }

    private void BeginRotation(bool isLeft = false)
    {
        if (!isRotating) // Start rotating if not already doing so
        {
            isRotating = true;
            animator.SetBool("isRight", !isLeft);
            animator.SetBool("isLeft", isLeft);
        }
    }

    System.Collections.IEnumerator MoveToPosition(Vector3 target)
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 1f * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
        animator.SetBool("isMoving", false);
        animator.SetBool("isBack", false);
    }

    private void RotateTowardsTarget()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1.0f) // Adjust tolerance as needed
        {
            transform.rotation = targetRotation;
            isRotating = false;
            animator.SetBool("isRight", false);
            animator.SetBool("isLeft", false);
        }
    }

    private void MoveTowardsTarget()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0.01f) // Small tolerance to ensure the position is reached
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 1f * Time.deltaTime);
        }
        else
        {
            isMoving = false;
            animator.SetBool("isMoving", false);
            animator.SetBool("isBack", false);
        }
    }

    void OnTriggerEnter (Collider other){
        if (other.CompareTag("Laser"))
        {
            onPlayerEnterLaser.Invoke();
        }
    }
}
