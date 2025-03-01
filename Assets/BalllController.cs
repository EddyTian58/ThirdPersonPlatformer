using System;
using Unity.VisualScripting;
using UnityEngine;

public class BalllController : MonoBehaviour
{
    [SerializeField] private Rigidbody sphereRigidbody;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private InputManager inputManager;
    [SerializeField] Transform ballTransform;
    [SerializeField] Transform cameraTransform;
    [Header("Movement Settings")]
    [SerializeField] private float ballSpeed = 3f;
    [SerializeField] private float ballDrag = 1f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float gravityMuliplier = 0.5f;

    private Boolean onGround = false;
    private int jumpCount = 0;
    float cooldownEndTime = 0f;

    private void Start()
    {
        inputManager.jumpInput.AddListener(jump);
        sphereRigidbody.linearDamping = ballDrag;
    }
    public void MoveBall(Vector3 input)
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 inputXZPlane = (cameraForward * input.z + cameraRight * input.x).normalized;
        Vector3 inputXYZPlane = new(inputXZPlane.x * ballSpeed/5, 0, inputXZPlane.z * ballSpeed/5);

        ballTransform.rotation = cameraTransform.rotation;
        sphereRigidbody.AddForce(inputXYZPlane * ballSpeed);
        sphereRigidbody.AddForce(Physics.gravity * (sphereRigidbody.mass*gravityMuliplier));
    }
    public void jump()
    {
        if (onGround == true || jumpCount < 2)
        {
            jumpCount++;
            sphereRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log(jumpCount);
        }
    }

//detecting if the ball is on the ground or has hit a wall
public void OnCollisionEnter(Collision collision)
    {
        audioSource.time = 0.1f;
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            jumpCount = 0;
            audioSource.Play();
            Debug.Log("ground touched");
        }
        if (collision.gameObject.tag == "Wall")
        {
            audioSource.Play();
        }
    }
    
    //detecting if the ball has left the ground
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Vector3 newVector3 = new Vector3(0, -10, 0);
            sphereRigidbody.AddForce(newVector3);
            onGround = false;
            Debug.Log("We have liftoff!");
        }
    }
}
