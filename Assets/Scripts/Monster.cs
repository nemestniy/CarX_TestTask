using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

    const float m_reachDistance = 0.3f;

    [SerializeField]
	private Transform m_moveTarget;

    [SerializeField]
    private float m_speed = 0.1f;

    [SerializeField]
    private int m_maxHP = 30;

	private int m_hp;
    private Vector3 m_translation;

	void Start() {
		m_hp = m_maxHP;
	}

    public void DecreaseHP(int damage)
    {
        m_hp -= damage;
        if (m_hp <= damage)
            Destroy(gameObject);
    }

	void Update () {
		if (m_moveTarget == null)
			return;
		
		if (Vector3.Distance (transform.position, m_moveTarget.position) <= m_reachDistance) {
			Destroy (gameObject);
			return;
		}

		m_translation = m_moveTarget.transform.position - transform.position;
		if (m_translation.magnitude > m_speed) {
			m_translation = m_translation.normalized * m_speed;
		}
		transform.Translate (m_translation);
	}

    public Vector3 GetDirection()
    {
        return (m_moveTarget.transform.position - transform.position).normalized;
    }

    public void ChangeMoveTarget(Transform target)
    {
        m_moveTarget = target;
    }
}
