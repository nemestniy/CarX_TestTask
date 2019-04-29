using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleTower : MonoBehaviour {

    [SerializeField]
    private int _projectileLimit;

    [SerializeField]
	private float m_shootInterval = 1;

    [SerializeField]
	private float m_range = 4f;

    [SerializeField]
	private GameObject m_projectilePrefab;

	private float m_timer;
    private List<GameObject> _listProjectile;

    private void Awake()
    {
        m_timer = m_shootInterval;
        _listProjectile = new List<GameObject>();
    }

    void Update () {
		if (m_projectilePrefab == null)
			return;

        m_timer -= Time.deltaTime;

		foreach (var monster in FindObjectsOfType<Monster>()) {
			if (Vector3.Distance (transform.position, monster.transform.position) > m_range)
				continue;

			if (m_timer > 0)
				continue;

            Shot(m_projectilePrefab , monster.transform);

            m_timer = m_shootInterval;
		}
	
	}

    private void Shot(GameObject projectileType, Transform target)
    {
        var projectile = Instantiate(projectileType, transform.position + Vector3.up * 1.5f, Quaternion.identity) as GameObject;
        var projectileBeh = projectile.GetComponent<GuidedProjectile>();
        projectileBeh.Init(target);

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
