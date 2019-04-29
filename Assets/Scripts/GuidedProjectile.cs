using UnityEngine;
using System.Collections;

public class GuidedProjectile : MonoBehaviour {

    [SerializeField]
    private float m_speed = 0.2f;

    [SerializeField]
    private int m_damage = 10;

    private Transform m_target;

    void Update () {
		if (m_target == null) {
			Destroy (gameObject);
			return;
		}

		var translation = (m_target.position - transform.position) * m_speed;
		if (translation.magnitude > m_speed) {
			translation = translation.normalized * m_speed;
		}
		transform.Translate (translation * Time.deltaTime);
	}

    public void Init(Transform target)
    {
        m_target = target;
    }

	void OnTriggerEnter(Collider other) {
		var monster = other.gameObject.GetComponent<Monster> ();
		if (monster == null)
			return;

        monster.DecreaseHP(m_damage);
		Destroy (gameObject);
	}
}
