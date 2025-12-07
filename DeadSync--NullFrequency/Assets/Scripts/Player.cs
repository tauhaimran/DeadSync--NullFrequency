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
    public float shootRange = 200f;
    public float damage = 20f;
    public LayerMask hitLayers;

    [Header("Impact FX")]
    public GameObject bulletHolePrefab;
    public float bulletHoleLife = 10f;

    void Start()
    {
        SelectGun(0);
    }

    void Update()
    {
        // Switch Gun
        if (Input.GetKeyDown(switchKey))
            SwitchGun();

        // Shoot
        if (Input.GetKeyDown(shootKey))
            Shoot();
    }

    void Shoot()
    {
        // VISUAL + SOUND FEEDBACK
        if (muzzleFlashFx[currentGun] != null)
            StartCoroutine(PlayFlashOnce(muzzleFlashFx[currentGun]));

        if (gunSounds[currentGun] != null)
            gunSounds[currentGun].PlayOneShot(gunSounds[currentGun].clip);

        // RAYCAST
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, shootRange, hitLayers))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // Bullet hole
            if (bulletHolePrefab != null)
            {
                Vector3 spawnPos = hit.point + hit.normal * 0.02f;
                Quaternion spawnRot = Quaternion.LookRotation(hit.normal);

                GameObject hole = Instantiate(bulletHolePrefab, spawnPos, spawnRot);
                hole.transform.localScale = Vector3.one * 0.1f;
                hole.transform.SetParent(hit.collider.transform);
            }

            // Damage Enemy
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(damage);
        }
    }

    void SwitchGun()
    {
        currentGun = (currentGun + 1) % guns.Length;
        SelectGun(currentGun);
    }

    void SelectGun(int index)
    {
        for (int i = 0; i < guns.Length; i++)
            guns[i].SetActive(i == index);
    }

    IEnumerator PlayFlashOnce(ParticleSystem fx)
    {
        fx.Play();
        yield return new WaitForSeconds(0.25f);
        fx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    // Draw gizmos for debugging raycast
    void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(Camera.main.transform.position,
                       Camera.main.transform.forward * shootRange);
    }
}
