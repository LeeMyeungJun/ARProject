using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] float delay = 0.5f;
    private void Start()
    {
        Destroy(this.gameObject, delay);
    }

}
