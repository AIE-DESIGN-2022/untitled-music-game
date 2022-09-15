using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float damage;
    [SerializeField] float bulletSpeed;
    [SerializeField] int beatToShoot;
    [SerializeField] Vector2 angle;
    int beatnum;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).right = -angle.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (BeatManager.beatFrame)
        {
            beatnum++;
            if(beatnum == beatToShoot)
            {
                GameObject bulletGo = Instantiate(bulletPrefab);
                bulletGo.transform.position = transform.GetChild(0).position;
                bulletGo.GetComponent<Rigidbody2D>().velocity = angle.normalized * bulletSpeed;
                bulletGo.GetComponent<Bullet>().damage = damage;
                beatnum = 0;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.GetChild(0).position, (Vector2)transform.GetChild(0).position + angle.normalized * 5);
    }
}
