using UnityEngine;
using System.Collections;

public partial class EnemyBehavior : MonoBehaviour {

    static private WayPointSystem sWayPoints = null;
    static private EnemySpawnSystem sEnemySystem = null;
    static public void InitializeEnemySystem(EnemySpawnSystem s, WayPointSystem w) { sEnemySystem = s; sWayPoints = w; }

    private const float kSpeed = 20f;
    private int mWayPointIndex = 0;

    private const float kTurnRate = 0.03f/60f;
    private int mNumHit = 0;
    private const int kHitsToDestroy = 4;
    private const float kEnemyEnergyLost = 0.8f;
		
	void Start () {
        mWayPointIndex = sWayPoints.GetInitWayIndex();
	}
	
	void Update () {
       sWayPoints.CheckNextWayPoint(transform.position, ref mWayPointIndex);
       PointAtPosition(sWayPoints.WayPoint(mWayPointIndex), kTurnRate);
       transform.position += (kSpeed * Time.smoothDeltaTime) * transform.up;
    }

    private void PointAtPosition(Vector3 p, float r)
    {
        Vector3 v = p - transform.position;
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
    }

    #region Trigger into chase or die
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerCheck(collision.gameObject);
    }

    private void TriggerCheck(GameObject g)
    {
        if (g.name == "Hero")
        {
            ThisEnemyIsHit();

        } else if (g.name == "Egg(Clone)")
        {
            mNumHit++;
            if (mNumHit < kHitsToDestroy)
            {
                Color c = GetComponent<Renderer>().material.color;
                c.a = c.a * kEnemyEnergyLost;
                GetComponent<Renderer>().material.color = c;
            } else
            {
                ThisEnemyIsHit();
            }
        }
    }

    private void ThisEnemyIsHit()
    {
        sEnemySystem.OneEnemyDestroyed();
        Destroy(gameObject);
    }
    #endregion
}
