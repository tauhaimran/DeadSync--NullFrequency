using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public LevelManager levelManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.OnPlayerEnteredTrigger(gameObject);
        }
    }
}
