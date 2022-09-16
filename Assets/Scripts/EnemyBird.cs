using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : MonoBehaviour
{
    int beatCount;
    public GameObject bulletPrefab;
    public float detectionRad,roamRange,moveSpeed;
    public int beatsToShoot;
    public LayerMask pLayer;
    public float bulletSpeed;
    public float damage;
    Vector2 origin,dest;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (BeatManager.beatFrame)
        {
            beatCount++;
            if(beatCount > beatsToShoot)
            {
                beatCount = 0;
            }
            Collider2D playerCol = Physics2D.OverlapCircle(transform.position, detectionRad, pLayer);
            if (playerCol)
            {
                if(beatCount == beatsToShoot)
                {
                    GameObject bulletGo = Instantiate(bulletPrefab);
                    bulletGo.transform.position = transform.position;
                    bulletGo.GetComponent<Rigidbody2D>().velocity = (playerCol.transform.position - transform.position).normalized * bulletSpeed;
                    bulletGo.GetComponent<Bullet>().damage = damage;
                }
                
            }
            if (beatCount == beatsToShoot / 2)
            {
                dest = Random.insideUnitCircle * roamRange + origin;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, dest, moveSpeed * Time.deltaTime);
        
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,detectionRad);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, roamRange);
    }
}
