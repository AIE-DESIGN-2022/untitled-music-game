using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gun",menuName ="Gun")]
public class Gun : ScriptableObject
{
    public GameObject[] bulletPrefabs;
    public float damage = 50;
    public float spread = .1f;
    public int bulletsToSpawn = 1;
    public int ammo = 1;
    public Sprite model;
    public int beatPerShot = 1;
    public int beatPerReload = 1;
    public bool auto = false;
    public float bulletSpeed = 50;
    public Vector2 modelOffset;
    public float barrelOffset = 0.1f;

    public AudioClip fireSound;
    public AudioClip noAmmo;
    public AudioClip reload;
}
