using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : Bullet
{
    List<GameObject> m_EnemyControllers = new List<GameObject>();

    void Explode()
    {
        if(m_EnemyControllers.Count == 0)
        {
            if (bulletHitEffect != null)
            {
                Vector3 spawnPosition = transform.position + new Vector3(0, 0.25f, 0);
                Instantiate(bulletHitEffect, spawnPosition, Quaternion.identity);
                //Destroy(bulletObject, 0.5f);
            }
            return;
        }

        for (int i = 0; i < m_EnemyControllers.Count; ++i)
        {
            m_EnemyControllers[i].GetComponent<EnemyController>().TakeDamage(bulletDamage);
        }
        if (bulletHitEffect != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0.25f, 0);
            Instantiate(bulletHitEffect, spawnPosition, Quaternion.identity);
            //Destroy(bulletObject, 0.5f);
        }
        Destroy(gameObject);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            m_EnemyControllers.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            m_EnemyControllers.Remove(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
}
