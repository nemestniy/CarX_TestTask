using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

    const float m_reachDistance = 0.3f;

    private float m_speed;
    private int m_maxHP;
    private Vector3 m_translation;
    private Vector3 m_moveTarget;

    public void Init(int maxHP, float speed, Vector3 targetPosition)
    {
        m_maxHP = maxHP;
        m_speed = speed;
        m_moveTarget = targetPosition;
    }

    public void DecreaseHP(int damage)
    {
        m_maxHP -= damage;
        if (m_maxHP <= damage)
            Destroy(gameObject);
    }

	void Update () {
		if (m_moveTarget == null)
			return;
		
		if (Vector3.Distance (transform.position, m_moveTarget) <= m_reachDistance) {
			Destroy (gameObject);
			return;
		}

        m_translation = (m_moveTarget - transform.position) * m_speed;
		if (m_translation.magnitude > m_speed) {
			m_translation = m_translation.normalized * m_speed;
		}
		transform.Translate (m_translation * Time.deltaTime);

	}

    public Vector3 GetDirection()
    {
        return (m_moveTarget - transform.position).normalized;
    }

}
