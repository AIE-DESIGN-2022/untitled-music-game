using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFirer : MonoBehaviour
{
    public enum Actions
    {
        Shoot,
        Reload,
        none
    }
    [SerializeField]Actions nextAction;
    public Gun gun;
    [SerializeField]int beatTimer = 0;
    [SerializeField] int ammo = 0;
    bool reloading;
    void Start()
    {
        LoadGun(gun);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ammo > 0 && beatTimer >= gun.beatPerShot)
        {
            nextAction = Actions.Shoot;
        }
        if (reloading)
        {
            if(beatTimer >= gun.beatPerReload)
            {
                beatTimer = gun.beatPerShot;
                ammo = gun.ammo;
            }
        }
        if (BeatManager.beatFrame)
        {
            OnBeat();
            beatTimer++;
        }
    }
    void Shoot()
    {
        ammo--;
        beatTimer = 0;
        for(int i = 0; i< gun.bulletsToSpawn; i++)
        {
            Vector2 playerToMouse = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * 5;
            playerToMouse += RandVect() * gun.spread;
            playerToMouse.Normalize();
            GameObject bulletGo = Instantiate(gun.bulletPrefabs[Random.Range(0, gun.bulletPrefabs.Length)]);
            bulletGo.transform.position = transform.position;
            bulletGo.transform.right = playerToMouse;
            bulletGo.GetComponent<Rigidbody2D>().velocity = playerToMouse * gun.bulletSpeed;
            bulletGo.GetComponent<Bullet>().damage = gun.damage;
        }
        

    }

    public void OnBeat()
    {
        switch (nextAction)
        {
            case Actions.Shoot:
                Shoot();
                break;
            case Actions.Reload:
                reloading = true;
                break;
            case Actions.none:
                break;
        }
        nextAction = Actions.none;
    }

    Vector2 RandVect()
    {
        return new Vector2(Random.value-.5f, Random.value-.5f).normalized;
    }

    void LoadGun(Gun g)
    {
        gun = g;
        ammo = g.ammo;
        beatTimer = g.beatPerShot;
        //update ui
    }
}
