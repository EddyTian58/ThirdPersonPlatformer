using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CookieController : MonoBehaviour
{
    [SerializeField] private Transform cookieTransform;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject cookie;
    public UnityEvent cookieCollected = new();
    public bool isCookieCollected = false;

    private void OnTriggerEnter(Collider triggeredObject)
    {
        Debug.Log("Trigger entered by: " + triggeredObject.name);
        isCookieCollected = true;
        audioSource.Play();
        cookieCollected?.Invoke();
        Debug.Log("Cookie collected!");

        StartCoroutine(DeactivateCookieAfterAudio());
    }
    private IEnumerator DeactivateCookieAfterAudio()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        cookie.SetActive(false);
    }
    void Update()
    {
        cookieTransform.Rotate(0, 1, 0);
    }
}
