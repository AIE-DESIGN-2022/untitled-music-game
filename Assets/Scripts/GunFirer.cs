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
    public int ammo = 0;
    [SerializeField] float reloadRotSpeed;
    [SerializeField]SpriteRenderer renderer;
    [SerializeField] AmmoDisp ammoDisp;
    [SerializeField] AudioSource audio;
    [SerializeField] float pitchRandm;
    float pitchDef;

    bool reloading;
    void Start()
    {
        pitchDef = audio.pitch;
        LoadGun(gun);
    }

    // Update is called once per frame
    void Update()
    {
       
        rotateModel();
        if (Input.GetMouseButtonDown(0) && beatTimer >= gun.beatPerShot && !reloading)
        {
            nextAction = Actions.Shoot;
        }
        if(Input.GetKeyDown(KeyCode.R) && !reloading && ammo != gun.ammo)
        {
            nextAction = Actions.Reload;
        }
        if (reloading)
        {
            renderer.transform.localRotation = Quaternion.Euler(
                renderer.transform.localRotation.eulerAngles.x,
                renderer.transform.localRotation.eulerAngles.y,
                renderer.transform.localRotation.eulerAngles.z + reloadRotSpeed*Time.deltaTime
                );
            if(beatTimer >= gun.beatPerReload)
            {
                renderer.transform.localRotation = Quaternion.Euler(0,0,0);
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

    void PlaySound(AudioClip s)
    {
        if(s == null)
        {
            return;
        }
        audio.pitch = pitchDef + (Random.value - .5f) * pitchRandm;
        audio.PlayOneShot(s);
    }



    public void OnBeat()
    {
        switch (nextAction)
        {
            case Actions.Shoot:
                if(ammo > 0)
                {
                    Shoot();
                    PlaySound(gun.fireSound);
                }
                else
                {
                    PlaySound(gun.noAmmo);
                }
                
                break;
            case Actions.Reload:
                reloading = true;
                beatTimer = 0;
                PlaySound(gun.reload);
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
        if (ammoDisp)
        {
            ammoDisp.LoadGun();
        }
    }
}
