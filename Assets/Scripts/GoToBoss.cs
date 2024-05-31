using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBoss : MonoBehaviour
{
    public int Scene;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        TuxedoManController player = hitInfo.GetComponent<TuxedoManController>();
        SceneManager.LoadScene(Scene);
    }
}