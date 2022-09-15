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
    [SerializeField]SpriteRenderer renderer;
    bool reloading;
    void Start()
    {
       
        LoadGun(gun);
    }

    // Update is called once per frame
    void Update()
    {
       
        rotateModel();
        if (Input.GetMouseButtonDown(0) && ammo > 0 && beatTimer >= gun.beatPerShot && !reloading)
        {
            nextAction = Actions.Shoot;
        }
        if(Input.GetKeyDown(KeyCode.R) && !reloading && ammo != gun.ammo)
        {
            nextAction = Actions.Reload;
        }
        if (reloading)
        {
            if(beatTimer >= gun.beatPerReload)
            {
                beatTimer = gun.beatPerShot;
                ammo = gun.ammo;
                reloading=false;
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
        Vector2 playerToMouse = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        playerToMouse.Normalize();
        for (int i = 0; i< gun.bulletsToSpawn; i++)
        {
            Vector2 updatedVect = playerToMouse + RandVect() * gun.spread;

            updatedVect.Normalize();
            GameObject bulletGo = Instantiate(gun.bulletPrefabs[Random.Range(0, gun.bulletPrefabs.Length)]);
            bulletGo.transform.position = transform.position + (Vector3)playerToMouse*gun.barrelOffset;
            bulletGo.transform.right = playerToMouse;
            bulletGo.GetComponent<Rigidbody2D>().velocity = updatedVect * gun.bulletSpeed;
            bulletGo.GetComponent<Bullet>().damage = gun.damage;
        }
        

    }
    void rotateModel()
    {
        Vector2 playerToMouse = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        transform.right = playerToMouse;
        if(playerToMouse.x < 0)
        {
            transform.localScale = new Vector2(1, -1);
        }
        else
        {
            transform.localScale = new Vector2(1,1);
        }
        
        renderer.transform.localPosition = gun.modelOffset;
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
                beatTimer = 0;
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
        if (g.model)
        {
            renderer.sprite = g.model;
        }
        
    }
}
