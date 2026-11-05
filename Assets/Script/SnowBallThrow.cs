using UnityEngine;
using System.Collections;

public class SnowBallThrow : MonoBehaviour
{
    public GameObject snowBallPrefab;
    public Transform throwPoint;
    public float throwForce = 30f; // increase for longer distance
    public float throwCooldown = 0.5f;

    private Animator animator;
    private bool isThrowing = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isThrowing)
        {
            StartCoroutine(ThrowRoutine());
        }
    }

    IEnumerator ThrowRoutine()
    {
        isThrowing = true;

        animator.SetTrigger("Throw"); // play throw animation

        yield return new WaitForSeconds(0.4f); // wait for animation

        GameObject snowBall = Instantiate(snowBallPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = snowBall.GetComponent<Rigidbody>();
        rb.linearVelocity = throwPoint.forward * throwForce;

        Destroy(snowBall, 5f); // destroy after 5 seconds

        yield return new WaitForSeconds(throwCooldown);
        isThrowing = false;
    }
}
