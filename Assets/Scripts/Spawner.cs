using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Projectile> projectiles;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRandomProjectile()
    {
        Vector3 place = new Vector3(transform.position.x, Random.Range(2,9) + transform.position.y, transform.position.z);
        Instantiate(projectiles[Random.Range(0, projectiles.Count)],
            place, Quaternion.identity);
    }
}
