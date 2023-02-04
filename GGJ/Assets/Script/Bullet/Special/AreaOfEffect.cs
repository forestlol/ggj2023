using CartoonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : Bullet
{
    List<GameObject> m_EnemyControllers = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        Explode();
    }
    void Explode()
    {
        if(m_EnemyControllers.Count == 0)
        {
            if (bulletHitEffect != null)
            {
                Vector3 spawnPosition = transform.position + new Vector3(0, 0.25f, 0);
                Instantiate(bulletHitEffect, spawnPosition, Quaternion.identity);
            }
            return;
        }
        int damage = m_EnemyControllers.Count * bulletDamage;
        for (int i = 0; i < m_EnemyControllers.Count; ++i)
        {
            
            m_EnemyControllers[i].GetComponent<EnemyController>().TakeDamage(bulletDamage);
        }
        if (bulletHitEffect != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0.25f, 0);
            Vector3 damageSpawnPosition = transform.position + new Vector3(0, 1f, 0);
            Instantiate(bulletHitEffect, spawnPosition, Quaternion.identity);
            CFXR_ParticleText temp = Instantiate(damageText, damageSpawnPosition, Quaternion.identity);
            temp.UpdateText(damage.ToString());
            Destroy(temp.gameObject, 0.5f);
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Explode();
        }
    }
}
