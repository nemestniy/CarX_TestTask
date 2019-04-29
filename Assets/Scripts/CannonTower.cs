using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonTower : MonoBehaviour {

    [SerializeField]
    private int _projectileLimit;

    [SerializeField]
    private float m_power;

    [SerializeField]
	private float m_shootInterval = 0.5f;

    [SerializeField]
	private float m_range = 4f;

    [SerializeField]
	private GameObject m_projectilePrefab;
    private CannonProjectile m_projectile;

    [SerializeField]
	private Transform m_shootPoint;

	private float m_timer;
    private Monster m_target;
    private List<GameObject> _listProjectile;

    private void Awake()
    {
        m_timer = m_shootInterval;
        _listProjectile = new List<GameObject>();
    }

    void Update () {
		if (m_projectilePrefab == null || m_shootPoint == null)
			return;

        m_timer -= Time.deltaTime;

		foreach (var monster in FindObjectsOfType<Monster>()) {
            var distance = Vector3.Distance(transform.position, monster.transform.position);

            if (distance > m_range)
				continue;

            if (m_target == null)
            {
                m_target = monster;
                continue;
            }

            if(Vector3.Distance(transform.position, m_target.transform.position) > distance)
            {
                m_target = monster;
            }
		}

        if (m_target != null)
        {
            var distance = Vector3.Distance(transform.position, m_target.transform.position);
            var targetPos = new Vector3(m_target.transform.position.x, m_target.transform.position.y - m_target.transform.localScale.y / 2, m_target.transform.position.z);
            var direction = targetPos + m_target.GetDirection() * Mathf.Sqrt(distance)/2;
            transform.LookAt(direction);

        }

        if ((m_timer <= 0 && m_target != null))
        {
            // shot
            Shot(m_projectilePrefab, m_power);
            
        }

    }

    private void Shot(GameObject projectileType, float power)
    {
        var projectile = Instantiate(projectileType, m_shootPoint.position, m_shootPoint.rotation) as GameObject;
        projectile.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * power);
        m_timer = m_shootInterval;

        CheckProjectileCount(projectile);
    }

    private void CheckProjectileCount(GameObject projectile)
    {
        _listProjectile.Add(projectile);
        if (_listProjectile.Count > _projectileLimit)
        {
            Destroy(_listProjectile[0]);
            _listProjectile.Remove(_listProjectile[0]);
        }
    }
}
