using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gun",menuName ="Gun")]
public class Gun : ScriptableObject
{
    public GameObject[] bulletPrefabs;
    public int ammo;
    public Sprite model;
}
