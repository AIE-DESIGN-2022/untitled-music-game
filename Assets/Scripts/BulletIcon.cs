using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletIcon : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bullet.SetActive(active);
    }
}
