using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector3> OnMove = new UnityEvent<Vector3>();
    public UnityEvent jumpInput = new UnityEvent();
    void Update()
    {
        Vector3 inputVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputVector += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector += Vector3.right;
            Debug.Log("D pressed");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpInput?.Invoke();
        }
        OnMove?.Invoke(inputVector);
    }
}
