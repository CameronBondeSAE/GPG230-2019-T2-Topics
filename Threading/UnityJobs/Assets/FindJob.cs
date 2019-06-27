// Unity Job

using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public struct FindJob : IJob
{
    public Vector2 start;
    public Vector2 target;

    public List<Node> open;
    public List<Node> closed;
    public List<Node> finalPath;

    public Node current;
    public Map map;

    public void ClearMap()
    {
        finalPath.Clear();
        open.Clear();
        closed.Clear();

        foreach (Node node in map.grid)
        {
//            if (node.debugGO != null)
//            {
                node.Reset();
//            }
        }
    }
    
    private Node FindLowestFCost()
    {
        // Find next lowest fCost
        int lowestFCost = int.MaxValue;
        Node lowestFCostNode = null;

        foreach (Node node in open)
        {
            if (node.fCost < lowestFCost)
            {
                lowestFCost = node.fCost;
                lowestFCostNode = node;
            }
        }

        return lowestFCostNode;
    }

    private bool CheckReachedTarget()
    {
        // Reached end
        if (current.position == target)
        {
            while (current.parent != null)
            {
                finalPath.Add(current);
                current = current.parent;
            }

            // Because it get added from the END back to the start
            finalPath.Reverse();

            return true;
        }

        return false;
    }

    public void Execute()
    {
        // Debug
        ClearMap();


        float xCheck = 0;
        float yCheck = 0;
        int fCost;
        int gCost;
        int hCost;
        Node nodeToCheck;

        current = map.grid[(int) start.x, (int) start.y];
        open.Add(current); // Initial starting point

        // Loop until end found
        while (open.Count > 0)
        {
            current = FindLowestFCost();

            // Node is closed
            open.Remove(current);

            // TODO: Check shouldn't need the contains check
            if (!closed.Contains(current))
                closed.Add(current);


            if (CheckReachedTarget())
            {
//                    OnFoundPath?.Invoke();
//                    return finalPath;
            }


            // Neighbours recalc
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    // Same as current so bail
                    if (x == 0 && y == 0)
                        continue;

                    xCheck = current.position.x + x;
                    yCheck = current.position.y + y;

                    // Bail if out of bounds or the current node, or in the closed list
                    if (xCheck < 0 || yCheck < 0 || xCheck >= map.size.x || yCheck >= map.size.y)
                        continue;

                    nodeToCheck = map.grid[(int) xCheck, (int) yCheck];
                    // Bail if node used or blocked
                    if (closed.Contains(nodeToCheck) || nodeToCheck.isBlocked)
                        continue;

                    // Note: Multiply by ten to maintain ints for distances
                    hCost = (int) (10 * Vector2.Distance(
                                       nodeToCheck.position,
                                       target));
                    gCost = current.gCost + (int) (10f * Vector2.Distance(
                                                       current.position,
                                                       nodeToCheck.position));

                    // fCost
                    fCost = hCost + gCost;

                    // Bail if the existing fCost is lower
                    if (nodeToCheck.fCost != 0 && fCost > nodeToCheck.fCost)
                        continue;

                    // All good, so record new values (don't do it WHILE you're calculating the f,g,h costs because they rely on previous results)
                    nodeToCheck.hCost = hCost;
                    nodeToCheck.gCost = gCost;
                    nodeToCheck.fCost = fCost;

                    // Debug
//                    if (nodeToCheck.debugGO.GetComponentInChildren<TextMesh>() != null)
//                        nodeToCheck.debugGO.GetComponentInChildren<TextMesh>().text =
//                            nodeToCheck.gCost + ":" + nodeToCheck.hCost + "\n" + nodeToCheck.fCost;

                    nodeToCheck.parent = current;

                    Debug.DrawLine(new Vector3(nodeToCheck.position.x, 0, nodeToCheck.position.y),
                        new Vector3(nodeToCheck.position.x, 10f, nodeToCheck.position.y), Color.magenta,
                        0.1f, false);

                    // TODO: Shouldn't need the contains check
                    if (!open.Contains(nodeToCheck))
                        open.Add(nodeToCheck);
                }
            }
        }
    }
}