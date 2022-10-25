using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager sTheGlobalBehavior = null;

    public Text mGameStateEcho = null; 
    public HeroBehavior mHero = null;
    public WayPointSystem mWayPoints = null;
    private EnemySpawnSystem mEnemySystem = null;

    private CameraSupport mMainCamera;
    
    void Start () {
        GameManager.sTheGlobalBehavior = this; 

        Debug.Assert(mWayPoints != null);
        Debug.Assert(mHero != null);

        mMainCamera = Camera.main.GetComponent<CameraSupport>();
        Debug.Assert(mMainCamera != null);

        Bounds b = mMainCamera.GetWorldBound();

        mEnemySystem = new EnemySpawnSystem(b.min, b.max);
        EnemyBehavior.InitializeEnemySystem(mEnemySystem, mWayPoints);
        mEnemySystem.GenerateEnemy(); 
    }
    
	void Update () {
        EchoGameState(); 

        if (Input.GetKey(KeyCode.Q))
            Application.Quit();
    }


    #region Bound Support
    public CameraSupport.WorldBoundStatus CollideWorldBound(Bounds b) { return mMainCamera.CollideWorldBound(b); }
    #endregion 

    private void EchoGameState()
    {
        mGameStateEcho.text =  mWayPoints.GetWayPointState() + "  " + 
                               mHero.GetHeroState() + "  " + 
                               mEnemySystem.GetEnemyState();
    }
}