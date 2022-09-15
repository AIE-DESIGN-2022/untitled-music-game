using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDisp : MonoBehaviour
{
    public List<BulletIcon> icoList = new List<BulletIcon>();
    [SerializeField] GameObject prefab;
    [SerializeField] GunFirer gun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(icoList.Count > 0)
        {
            for (int i = 0; i < icoList.Count; i++)
            {
                icoList[icoList.Count-i-1].active = i < gun.ammo;
            }
        }
        
    }

    public void LoadGun()
    {
        if(gun.gun.ammo > icoList.Count)
        {
            int t = gun.gun.ammo - icoList.Count;
            for (int i =0; i< t;i++)
            {
                GameObject go = Instantiate(prefab, transform);
                icoList.Add(go.GetComponent<BulletIcon>());
            }
        }
        if(gun.gun.ammo > icoList.Count)
        {
            int t = icoList.Count - gun.gun.ammo;
            for (int i = 0; i < t; i++)
            {
                BulletIcon temp = icoList[i];
                icoList.RemoveAt(i);
                Destroy(temp.gameObject);
            }
        }
    }
}
