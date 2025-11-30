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
    public float shootRange = 200f;       // how far we can hit
    public float damage = 20f;            // how much damage per shot
    public LayerMask hitLayers;           // what we can shoot (Enemy, environment, etc.)

    [Header("Impact FX")]
    public GameObject bulletHolePrefab;           // decal / bullet mark
    public float bulletHoleLife = 10f;            // how long before it disappears

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
       /* // --- Particle one-shot ---
        if (muzzleFlashFx[currentGun] != null)
            //muzzleFlashFx[currentGun].Play();
            StartCoroutine(PlayFlashOnce(muzzleFlashFx[currentGun]));
        else
            Debug.LogWarning("Muzzle Flash missing on gun " + currentGun);

         --- Sound one-shot ---
        if (gunSounds[currentGun] != null)
            gunSounds[currentGun].PlayOneShot(gunSounds[currentGun].clip);
        else
            Debug.LogWarning("Sound missing on gun " + currentGun);*/


        // 🔥 VISUAL & SOUND FEEDBACK
        if (muzzleFlashFx[currentGun] != null)
            StartCoroutine(PlayFlashOnce(muzzleFlashFx[currentGun]));
        else
            Debug.LogWarning("Muzzle Flash missing on gun " + currentGun);

        if (gunSounds[currentGun] != null)
            gunSounds[currentGun].PlayOneShot(gunSounds[currentGun].clip);
        else
            Debug.LogWarning("Muzzle Flash missing on gun " + currentGun);

        // ===========================
        // 🔫 RAYCAST SHOOTING
        // ===========================
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));

        /*if (Physics.Raycast(ray, out RaycastHit hit, shootRange, hitLayers))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // If the thing we hit has "Enemy" script → deal damage
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Enemy took " + damage);
            }
        }*/

         if (Physics.Raycast(ray, out RaycastHit hit, shootRange, hitLayers))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // -------- BULLET HOLE / IMPACT --------
            if (bulletHolePrefab != null)
            {
                Vector3 spawnPos = hit.point + hit.normal * 0.02f; // small offset to avoid z-fighting
                Quaternion spawnRot = Quaternion.LookRotation(hit.normal);

                GameObject hole = Instantiate(bulletHolePrefab, spawnPos, spawnRot);
                hole.transform.localScale = Vector3.one * 0.1f;

                // Optional: parent to surface (comment out if holes disappear)
                hole.transform.SetParent(hit.collider.transform);

                //Destroy(hole, bulletHoleLife);
            }

            // -------- DAMAGE --------
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Enemy took " + damage);
            }
        }

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


    // DRAW GIZMOS FOR RAYCAST
    void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        Gizmos.color = Color.yellow;

        // Draw shooting ray from camera forward
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootRange);
    }

}
