using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CookieController : MonoBehaviour
{
    [SerializeField] private Transform cookieTransform;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject cookie;
    public UnityEvent cookieCollected = new();
    public int timesHit = 0;

    private void OnTriggerEnter(Collider triggeredObject)
    {
        timesHit++;
        if (timesHit == 1)
        {
            audioSource.Play();
            StartCoroutine(DeactivateCookieAfterAudio());
            Debug.Log("Cookie collected!");
        }
    }
    private IEnumerator DeactivateCookieAfterAudio()
    {
        cookieCollected?.Invoke();
        yield return new WaitForSeconds(audioSource.clip.length);
        cookie.SetActive(false);
    }
    void Update()
    {
        cookieTransform.Rotate(0, 1, 0);
    }
}
