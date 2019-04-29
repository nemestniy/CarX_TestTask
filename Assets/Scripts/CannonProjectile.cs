using UnityEngine;
using System.Collections;

public class CannonProjectile : MonoBehaviour {

    [SerializeField]
	private int m_damage = 10;

    [SerializeField]
    private float c_timeLive = 5;
    private float c_timer;

    private void Awake()
    {
        c_timer = c_timeLive;
    }

    void Update () {

        c_timer -= Time.deltaTime;

        if (c_timer <= 0)
            Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {
		var monster = other.gameObject.GetComponent<Monster>();
		if (monster == null)
			return;

        monster.DecreaseHP(m_damage);
		Destroy (gameObject);
	}
}
