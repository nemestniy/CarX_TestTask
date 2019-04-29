using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    [SerializeField]
	private float m_interval = 3;


    [Header("Monster Properties:")]
    [SerializeField]
    private PrimitiveType m_monsterType;

    [SerializeField]
    private Transform m_moveTarget;

    [SerializeField]
    private float m_speed;

    [SerializeField]
    private int m_maxHP;


	private float m_timer;

    private void Awake()
    {
        m_timer = m_interval;
    }

    void Update () {

        m_timer -= Time.deltaTime;

		if (m_timer <= 0) {

            CreateMonster(m_monsterType, m_maxHP, m_speed, m_moveTarget);
			m_timer = m_interval;
		}
	}

    private void CreateMonster(PrimitiveType monsterType, int HP, float speed, Transform moveTarget)
    {
        var newMonster = GameObject.CreatePrimitive(monsterType);
        newMonster.transform.position = transform.position;
        var monsterBeh = newMonster.AddComponent<Monster>();
        monsterBeh.Init(HP, speed, moveTarget.position);
    }
}
