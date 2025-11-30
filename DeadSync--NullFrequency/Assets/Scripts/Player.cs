using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("WEAPONS")]
    public GameObject[] guns = new GameObject[3];
    public ParticleSystem[] muzzleFlashFx = new ParticleSystem[3];
    public AudioSource[] gunSounds = new AudioSource[3];

    private int currentGun = 0;

    [Header("SHOOT SETTINGS")]
    public KeyCode switchKey = KeyCode.Q;
    public KeyCode shootKey = KeyCode.Mouse0;

    [Header("PAUSE")]
    public KeyCode pauseKey = KeyCode.Escape;
    //public TMP_Text pauseText;
    //public TMP_Text pauseText = "..";
    private bool isPaused = false;

    void Start()
    {
        SelectGun(0);
    }

    void Update()
    {
        if (isPaused)
        {
            if (Input.GetKeyDown(pauseKey)) 
                TogglePause();
            else
            return;
        }

        // Switch Gun
        if (Input.GetKeyDown(switchKey))
            SwitchGun();

        // 🔥 Fire single shot only ON CLICK
        if (Input.GetKeyDown(shootKey))
            Shoot();

        // Pause
        if (Input.GetKeyDown(pauseKey))
            TogglePause();
    }

    void Shoot()
    {
        // --- Particle one-shot ---
        if (muzzleFlashFx[currentGun] != null)
            //muzzleFlashFx[currentGun].Play();
            StartCoroutine(PlayFlashOnce(muzzleFlashFx[currentGun]));
        else
            Debug.LogWarning("Muzzle Flash missing on gun " + currentGun);

        // --- Sound one-shot ---
        if (gunSounds[currentGun] != null)
            gunSounds[currentGun].PlayOneShot(gunSounds[currentGun].clip);
        else
            Debug.LogWarning("Sound missing on gun " + currentGun);
    }

    void SwitchGun()
    {
        currentGun = (currentGun + 1) % 3;
        SelectGun(currentGun);
    }

    void SelectGun(int index)
    {
        for (int i = 0; i < guns.Length; i++)
            guns[i].SetActive(i == index);
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        Debug.Log(isPaused ? "Paused" : "Unpaused");
        //Text.pauseText.setActive(isPaused);
    }

    // Coroutine to play muzzle flash once
    IEnumerator PlayFlashOnce(ParticleSystem fx)
    {
        fx.Play();                     // play effect instantly
        yield return new WaitForSeconds(0.25f); // muzzle flash lasts a fraction of a second
        fx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // force stop
    }
}
