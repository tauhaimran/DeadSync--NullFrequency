using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public LevelManager levelManager;
    [SerializeField] List<GameObject> Enemies;
    bool levelstarted = false;
    private void Start()
    {
        foreach (GameObject enemy in Enemies)
        {
            enemy.SetActive(false);
        }
    }

    public void StartLevel()
    {
        if (levelstarted) return;
        levelstarted = true;
        foreach (GameObject enemy in Enemies)
        {
            enemy.transform.SetParent(transform.parent, worldPositionStays: true);
            enemy.SetActive(true);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.OnPlayerEnteredTrigger(this);
        }
    }
}
