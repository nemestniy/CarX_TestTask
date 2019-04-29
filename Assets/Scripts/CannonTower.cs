using UnityEngine;
using System.Collections;

public class CannonTower : MonoBehaviour {

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
    private float m_projectileSpeed;

    private void Awake()
    {
        m_timer = m_shootInterval;
        
    }

    void Update () {
		if (m_projectilePrefab == null || m_shootPoint == null)
			return;

        m_timer -= Time.deltaTime;

        var target = new Monster();

		foreach (var monster in FindObjectsOfType<Monster>()) {
            var distance = Vector3.Distance(transform.position, monster.transform.position);

            if (distance > m_range)
				continue;

            if (target == null)
            {
                target = monster;
                continue;
            }

            if(Vector3.Distance(transform.position, target.transform.position) > distance)
            {
                target = monster;
            }
		}

        if (target != null)
        {
            var distance = Vector3.Distance(transform.position, target.transform.position);
            var targetPos = new Vector3(target.transform.position.x, target.transform.position.y - target.transform.localScale.y / 2, target.transform.position.z);
            var direction = targetPos + target.GetDirection() * Mathf.Sqrt(distance)/2;
            transform.LookAt(direction);

        }

        if ((m_timer <= 0 && target != null))
        {
            // shot
            var projectile = Instantiate(m_projectilePrefab, m_shootPoint.position, m_shootPoint.rotation) as GameObject;
            projectile.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * m_power);
            m_timer = m_shootInterval;
        }

    }
}
