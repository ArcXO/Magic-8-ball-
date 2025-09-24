using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class Magic8Ball : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI answerText;          // Assign a UI Text (or TextMeshProUGUI if using TMP - see note)
    public Button shakeButton;       // Assign a UI Button
    public Animator ballAnimator;    // Optional: Animator to play shake animation

    [Header("Behavior")]
    [TextArea(5,10)]
    public string[] responses = new string[] {
        "It is certain.",
        "Without a doubt.",
        "You may rely on it.",
        "Ask again later.",
        "Cannot predict now.",
        "Don't count on it.",
        "My sources say no.",
        "Very doubtful."
    };
    public float shakeDuration = 1.2f;
    public AudioClip shakeSound;
    public AudioClip revealSound;

    AudioSource _audio;
    bool _isShaking = false;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        if (shakeButton != null)
            shakeButton.onClick.AddListener(OnShakePressed);
        if (answerText != null)
            answerText.text = "Ask a question, then press Shake";
    }

    void OnDestroy()
    {
        if (shakeButton != null)
            shakeButton.onClick.RemoveListener(OnShakePressed);
    }

    public void OnShakePressed()
    {
        if (_isShaking) return;
        StartCoroutine(DoShake());
    }

    IEnumerator DoShake()
    {
        _isShaking = true;
        if (ballAnimator != null)
            ballAnimator.SetTrigger("Shake");

        if (shakeSound != null)
            _audio.PlayOneShot(shakeSound);

        // simple wait while "shaking"
        float t = 0f;
        while (t < shakeDuration)
        {
            t += Time.deltaTime;
            yield return null;
        }

        // choose random response
        string resp = "No answer.";
        if (responses != null && responses.Length > 0)
            resp = responses[Random.Range(0, responses.Length)];

        if (revealSound != null)
            _audio.PlayOneShot(revealSound);

        if (answerText != null)
            answerText.text = resp;

        _isShaking = false;
    }
}
