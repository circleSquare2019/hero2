using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroBehavior : MonoBehaviour {
    
    public EggSpawnSystem mEggSystem = null;
    private const float kHeroRotateSpeed = 90f/2f; 
    private const float HeroStartSpeed = 20f;  
    private float mHeroSpeed = HeroStartSpeed;
    
    private bool Mouse = true;
    private int HeroTouchedEnemy = 0;
    private void TouchedEnemy() { HeroTouchedEnemy++; }
    public string GetHeroState() { return "ControlMode: " + (Mouse?"Mouse":"Key") + 
                                          " TouchedEnemy: " + HeroTouchedEnemy + "   " 
                                            + mEggSystem.EggSystemStatus(); }

    private void Awake()
    {
        Debug.Assert(mEggSystem != null);
        EggBehavior.InitializeEggSystem(mEggSystem);
    }

    void Start ()
    { 
    }
	
	void Update () {
        UpdateMotion();
        ProcessEggSpwan();
    }

    private int EggsOnScreen() { return mEggSystem.GetEggCount();  }

    private void UpdateMotion()
    {
        if (Input.GetKeyDown(KeyCode.M)) Mouse = !Mouse;

        transform.Rotate(Vector3.forward, -1f * Input.GetAxis("Horizontal") *
                                    (kHeroRotateSpeed * Time.smoothDeltaTime));
        if (Mouse){
            Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            p.z = 0f;
            transform.position = p;
        }else{
            mHeroSpeed += Input.GetAxis("Vertical");
            transform.position += transform.up * (mHeroSpeed * Time.smoothDeltaTime);
        }
    }

    private void ProcessEggSpwan()
    {
        if (mEggSystem.CanSpawn())
        {
            if (Input.GetKey("space"))
                mEggSystem.SpawnAnEgg(transform.position, transform.up);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hero touched");
        if (collision.gameObject.name == "Enemy(Clone)")
            TouchedEnemy();
    }
}