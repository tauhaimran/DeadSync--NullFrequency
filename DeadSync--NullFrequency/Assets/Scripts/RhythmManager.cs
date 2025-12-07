using UnityEngine;
using TMPro;
using DG.Tweening;

public class RhythmManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource music;
    public string songPath = "sfx/Dysmn - Burn In Hell (I Am Sorry)";
    public float BPM = 65f;

    [Header("UI")]
    public TMP_Text rhythmText;

    [Header("Camera")]
    public Camera cam;

    [Header("Chaos Phase")]
    public int beatsBeforeChaos = 30;        // Number of beats before chaos starts
    public float chaosMaxShake = 1f;        // Maximum camera shake intensity
    public float chaosDecayPerHit = 0.2f;   // How much each E press reduces shake
    public float chaosShakeSpeed = 0.25f;    // Flicker speed

    private float beatInterval;
    private float nextBeatTime = 0;
    private int beatCount = 0;

    private bool isChaos = false;
    private float currentShake = 0f;
    private float flickerTimer = 0f;

    void Start()
    {
        DOTween.KillAll();

        AudioClip clip = Resources.Load<AudioClip>(songPath);
        if (clip == null)
        {
            Debug.LogError("❌ AUDIO NOT FOUND: " + songPath);
            return;
        }

        music.clip = clip;
        music.time = 0;
        music.Play();

        if (cam == null)
            cam = Camera.main;

        beatInterval = 60f / BPM;
        nextBeatTime = beatInterval;

        if (rhythmText != null)
            rhythmText.text = "STABLE...";

        Debug.Log($"🎵 Loaded {clip.name}, BPM={BPM}, Interval={beatInterval:F3}s");
    }

    void Update()
    {
        if (!music.isPlaying) return;

        // Beat detection
        if (music.time >= nextBeatTime)
        {
            TriggerBeat();
            nextBeatTime += beatInterval;
        }

        // Chaos handling
        if (isChaos)
        {
            HandleChaos();
        }
    }

    void TriggerBeat()
    {
        beatCount++;

        // Start chaos after a few beats
        if (!isChaos && beatCount >= beatsBeforeChaos)
        {
            StartChaos();
            return;
        }

        // Optional: small camera shake for normal beats before chaos
        if (!isChaos && cam != null)
        {
            cam.transform.DOKill();
            cam.transform.DOShakePosition(
                duration: 0.2f,
                strength: 0.2f,
                vibrato: 60,
                randomness: 90,
                snapping: false,
                fadeOut: true
            );
        }

        Debug.Log($"🎧 Beat at {music.time:F3}");
    }

    void HandleChaos()
    {
        // Shake the camera heavily
        if (cam != null)
        {
            cam.transform.DOShakePosition(
                duration: 0.1f,
                strength: currentShake,
                vibrato: 30,
                randomness: 90,
                snapping: false,
                fadeOut: true
            );
        }

        // Flicker UI text
        if (rhythmText != null)
        {
            flickerTimer += Time.deltaTime;
            if (flickerTimer >= chaosShakeSpeed)
            {
                rhythmText.alpha = rhythmText.alpha == 0 ? 1 : 0;
                flickerTimer = 0f;
            }
        }

        // Reduce shake only when E is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentShake -= chaosDecayPerHit;
            currentShake = Mathf.Max(currentShake, 0f);

            if (rhythmText != null)
            {
                rhythmText.DOKill();
                rhythmText.text = "RESIST!";
                rhythmText.color = Color.yellow;
                rhythmText.alpha = 1;
                rhythmText.DOFade(0f, 0.3f);
            }

            if (currentShake <= 0f)
            {
                EndChaos();
            }
        }
    }

    void StartChaos()
    {
        isChaos = true;
        currentShake = chaosMaxShake;

        if (rhythmText != null)
        {
            rhythmText.DOKill();
            rhythmText.text = "CHAOS!!! PRESS E!";
            rhythmText.color = Color.red;
            rhythmText.alpha = 1;
        }

        Debug.Log("💥 CHAOS STARTED!");
    }

    void EndChaos()
    {
        isChaos = false;

        if (rhythmText != null)
        {
            rhythmText.DOKill();
            rhythmText.text = "STABLE... BRACE!!";
            rhythmText.color = Color.white;
            rhythmText.alpha = 1;
        }

        Debug.Log("✅ CHAOS ENDED!");
    }
}
