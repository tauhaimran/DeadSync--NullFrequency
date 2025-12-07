using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEnemyOnDefeat : MonoBehaviour
{
    public GameObject winscreen;
    void OnDestroy()
    {
        winscreen.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
