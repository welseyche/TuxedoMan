using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float TimeToLive = 1f;
    private void Start()
    {
        Destroy(gameObject, TimeToLive);
    }
}
