using UnityEngine;
using System.Collections;

public class GuidedProjectile : MonoBehaviour {

    [SerializeField]
	private Transform m_target;

    [SerializeField]
    private float m_speed = 0.2f;

    [SerializeField]
    private int m_damage = 10;

	void Update () {
		if (m_target == null) {
			Destroy (gameObject);
			return;
		}

		var translation = m_target.position - transform.position;
		if (translation.magnitude > m_speed) {
			translation = translation.normalized * m_speed;
		}
		transform.Translate (translation);
	}

	void OnTriggerEnter(Collider other) {
		var monster = other.gameObject.GetComponent<Monster> ();
		if (monster == null)
			return;

        monster.DecreaseHP(m_damage);
		Destroy (gameObject);
	}

    public void ChangeTarget(Transform target)
    {
        m_target = target;
    }
}
