using UnityEngine;

/*
Coal Spirit Behavior:
If not aggro (enemy is not within 10) then does something cute (bounces in place).
If aggro go toward enemy!!!
Unless cliff (to add)
*/
public class CoalSpirit : MonoBehaviour
{
    [SerializeField]
    private float _detectionRange;
    [SerializeField]
    private float _forgettingRange;
    [SerializeField]
    private float _moveSpeed = 3f;
    private Transform _enemyInRange;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyInRange != null)
        {
            float distance = Vector3.Distance(transform.position, _enemyInRange.position);
            if (distance >= _forgettingRange) { _enemyInRange = null; return; }

            // Move toward enemy
            MoveTowardsPlayer();

            return;
        }

        // To Do: Maybe have separate layer for different targets
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            _detectionRange
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.tag == "Player")
            {
                _enemyInRange = hit.transform;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_enemyInRange == null || !PlayerTies.Instance.TeleportationTieCausingAggro)
        {
            return;
        }

        Debug.Log("On trigger enter " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStatus>().DealDamage(5);
        }
    }

    private void MoveTowardsPlayer()
    {
        if (!PlayerTies.Instance.TeleportationTieCausingAggro) { return; }
        transform.position = Vector3.MoveTowards(
            transform.position,
            _enemyInRange.position,
            _moveSpeed * Time.deltaTime
        );
    }
}
