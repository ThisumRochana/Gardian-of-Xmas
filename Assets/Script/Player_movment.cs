using UnityEngine;

public class Player_movment : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movmentDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = movmentDirection.magnitude;
        magnitude = Mathf.Clamp01(magnitude);
        movmentDirection.Normalize();

        transform.Translate(movmentDirection * magnitude * speed * Time.deltaTime, Space.World);

        if(movmentDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movmentDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
}
