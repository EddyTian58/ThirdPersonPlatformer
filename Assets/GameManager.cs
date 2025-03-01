using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    private CookieController[] cookies;

    private void Start()
    {
        cookies = FindObjectsByType<CookieController>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var cookie in cookies)
        {
            cookie.cookieCollected.AddListener(IncrementScore); 
        }
    }
    private void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }
}
