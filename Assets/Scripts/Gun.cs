using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gun",menuName ="Gun")]
public class Gun : ScriptableObject
{
    public GameObject[] bulletPrefabs;
    public float spread;
    public int bulletsToSpawn;
    public int ammo;
    public Sprite model;
    public int beatPerShot;
    public int beatPerReload;
    public bool auto = false;
    public float bulletSpeed;
}
