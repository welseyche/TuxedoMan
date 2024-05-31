using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PitRespawn : MonoBehaviour
{
    public int Respawn;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        TuxedoManController player = hitInfo.GetComponent<TuxedoManController>();
        SceneManager.LoadScene(Respawn);
    }
}