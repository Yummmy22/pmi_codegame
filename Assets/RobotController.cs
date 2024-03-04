using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float tileSize = 1.0f;
    public float gravityValue = -9.81f;
    public CharacterController controller;
    public float rotationSpeed = 90f; // Degrees per second
    private Animator animator;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private Quaternion targetRotation; // Target rotation
    private bool isRotating = false; // Flag to indicate if currently rotating

    private void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        targetRotation = transform.rotation; // Initialize target rotation
    }

    void Update()
    {
        if (!isMoving && Input.anyKeyDown)
        {
            RotateCharacter();
        }

        if (transform.position == targetPosition && isMoving)
        {
            isMoving = false;
            animator.SetBool("isMoving", false);
            animator.SetBool("isBack", false);
        }

        // Smoothly rotate towards the target rotation
        if (isRotating)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 720f / 360f);
            // Check if the rotation is close enough to the target rotation
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1.0f) // 1.0f is the tolerance, adjust as needed
            {
                transform.rotation = targetRotation; // Snap to the target rotation to avoid overshooting
                isRotating = false; // Stop the rotation
                // Reset the animator parameters
                animator.SetBool("isRight", false);
                animator.SetBool("isLeft", false);
            }
        }
    }

    void RotateCharacter()
    {
        if (isRotating)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetRotation *= Quaternion.Euler(0, 90, 0); // Update target rotation
            isRotating = true; // Indicate that rotation has started
            animator.SetBool("isRight", true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetRotation *= Quaternion.Euler(0, -90, 0); // Update target rotation
            isRotating = true; // Indicate that rotation has started
            animator.SetBool("isLeft", true);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetPosition += transform.forward * tileSize;
            isMoving = true;
            animator.SetBool("isMoving", true);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetPosition -= transform.forward * tileSize;
            isMoving = true;
            animator.SetBool("isBack", true);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(MoveToPosition(targetPosition));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(MoveToPosition(targetPosition));
        }
    }

    System.Collections.IEnumerator MoveToPosition(Vector3 target)
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 1f * Time.deltaTime);
            yield return null;
        }
        // Movement has ended, reset animations here if needed
        animator.SetBool("isMoving", false);
    }
}
