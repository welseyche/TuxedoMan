using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamagePlayer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        TuxedoManController player = hitInfo.GetComponent<TuxedoManController>();
        player?.TakeDamage(1);
    }
}
