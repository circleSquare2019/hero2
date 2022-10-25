using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointSystem : MonoBehaviour
{
    private string[] Name = {
            "A", "B", "C",
            "D", "E", "F"};
    private GameObject[] WayPoints;
    private const float kWayPointTouchDistance = 25f;
    private bool mPointsInSequence = true;

    void Awake()
    {
        WayPoints = new GameObject[Name.Length];
        int i = 0;
        foreach (string s in Name)
        {
            WayPoints[i] = GameObject.Find(Name[i]);
            Debug.Assert(WayPoints[i] != null);
            i++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleVisibility();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleSequenceOrder();
        }
    }

    private void ToggleVisibility()
    {
        foreach (GameObject g in WayPoints)
            g.SetActive(!g.activeSelf);
    }

    private void ToggleSequenceOrder()
    {
        mPointsInSequence = !mPointsInSequence;
    }
    public bool WayPointInSequence() { return mPointsInSequence; }

    public void CheckNextWayPoint(Vector3 p, ref int index)
    {
        if (Vector3.Distance(p, WayPoints[index].transform.position) < kWayPointTouchDistance)
        {
            if (mPointsInSequence)
            {
                index++;
                if (index >= 6)
                    index = 0;
            } else
            {
                index = Random.Range(0, 5);
            }
        }
    }

    public int GetInitWayIndex()
    {
        return Random.Range(0, WayPoints.Length);
    }

    public Vector3 WayPoint(int index) 
    {
        return WayPoints[index].transform.position;
    }

    public string GetWayPointState() { return "FlightMode: " + (WayPointInSequence() ? "WayPoints" : "Random   "); }
}
