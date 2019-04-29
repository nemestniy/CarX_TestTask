using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    [SerializeField]
	private float m_interval = 3;

    [SerializeField]
	private Transform m_moveTarget;

	private float m_timer;

    private void Awake()
    {
        m_timer = m_interval;
    }

    void Update () {

        m_timer -= Time.deltaTime;

		if (m_timer <= 0) {
			var newMonster = GameObject.CreatePrimitive (PrimitiveType.Capsule);
			newMonster.transform.position = transform.position;
			var monsterBeh = newMonster.AddComponent<Monster> ();
			monsterBeh.ChangeMoveTarget(m_moveTarget);

			m_timer = m_interval;
		}
	}
}
