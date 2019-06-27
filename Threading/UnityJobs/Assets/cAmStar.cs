using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs;
using UnityEngine;


public class cAmStar : MonoBehaviour
{
    public GameObject startPrefab;
    public GameObject targetPrefab;
    public GameObject pathCubePrefab;
    public float visualiseSpeed = 0.1f;

    GameObject targetIndicator;
    GameObject startIndicator;

    public event Action OnFoundPath;
    public event Action OnBlockedPath;

    public Vector2 start;
    public Vector2 target;

    public Map map;
    
    void Start()
    {
        startIndicator = Instantiate(startPrefab);
        targetIndicator = Instantiate(targetPrefab);

        RandomlyPositionStartAndTarget();
    }

    public void DemoMode()
    {
        RandomlyPositionStartAndTarget();
        FindPath();
    }

    public List<Node> FindPath()
    {
        return FindPathCoroutine();
    }

    public List<Node> FindPath(Vector2 _start, Vector2 _target)
    {
        start = _start;
        target = _target;

        startIndicator.transform.position = new Vector3(start.x, 0, start.y);
        targetIndicator.transform.position = new Vector3(target.x, 0, target.y);

        return FindPath();
    }

    private List<Node> FindPathCoroutine()
    {
        OnBlockedPath?.Invoke();
        return null;
    }

    public void RandomlyPositionStartAndTarget()
    {
        start = map.FindUnblockedSpace();
        startIndicator.transform.position = new Vector3(start.x, 0, start.y);
        target = map.FindUnblockedSpace();
        targetIndicator.transform.position = new Vector3(target.x, 0, target.y);
    }


//    public void Update()
//    {
//        // Don't continuously update if we want to visualise manually
//        if (visualiseSpeed > 0)
//            return;
//
//        start.x = (int) startIndicator.transform.position.x;
//        start.y = (int) startIndicator.transform.position.z;
//
//        target.x = (int) targetIndicator.transform.position.x;
//        target.y = (int) targetIndicator.transform.position.z;
//
//        ClearMap();
////		RandomlyPositionStartAndTarget();
//        FindPath();
//    }


//    private void OnDrawGizmos()
//    {
//        foreach (Node node in open)
//        {
//            Gizmos.color = Color.red;
//            Gizmos.DrawCube( new Vector3(node.position.x, -0.45f, node.position.x), Vector3.one);
//        }
//    }
//        for (int x = 0; x < map.size.x; x++)
//        {
//            for (int y = 0; y < map.size.y; y++)
//            {
//                if (map.grid != null)
//                {
//                    if (map.grid[x, y].isBlocked)
//                    {
//                        Gizmos.color = Color.red;
//                        Gizmos.DrawCube(new Vector3(x, -0.45f, y), Vector3.one);
//                    }
//
//                    if (open.Contains(map.grid[x, y]))
//                    {
//                        Gizmos.color = Color.green;
//                        Gizmos.DrawCube(new Vector3(x, -0.45f, y), Vector3.one);
//                    }
//
//                    if (closed.Contains(map.grid[x, y]))
//                    {
//                        Gizmos.color = Color.gray;
//                        Gizmos.DrawCube(new Vector3(x, -0.45f, y), Vector3.one);
//                    }
//                }
//            }
//        }


//		// Scan the real world starting at 0,0,0 (to be able to place the grid add transform.position)
//		for (int x = 0; x < size.x; x++)
//		{
//			for (int y = 0; y < size.y; y++)
//			{
//				if (Physics.CheckBox(transform.position + new Vector3(x, 0, y), new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity))
//				{
//					// Something is there
//					grid[x, y].isBlocked = true;
//					Gizmos.color = Color.red;
//					Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), Vector3.one);
//				}
//			}
//		}
//    }
}