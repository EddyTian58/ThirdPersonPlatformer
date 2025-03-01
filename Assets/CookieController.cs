using UnityEngine;
using UnityEngine.Events;

public class CookieController : MonoBehaviour
{
    [SerializeField] Transform cookieTransform;
    [SerializeField] GameObject cookie;
    public UnityEvent cookieCollected = new();
    public bool isCookieCollected = false;

    private void OnTriggerEnter(Collider triggeredObject)
    {
        isCookieCollected = true;
        cookieCollected?.Invoke();
        cookie.SetActive(false);
        Debug.Log("Cookie collected!");
    }
    void Update()
    {
        cookieTransform.Rotate(0, 1, 0);
    }
}
