using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[System.Serializable]
public class PathOperation
{
    private List<PathNode> openNodes;
    private List<PathNode> closedNodes;

    private PathNode frontierNode;
    private GridNode startNode;
    private GridNode endNode;

    private PathRoute route;

    private bool foundEnd = false;

    public PathOperation(GridNode start, GridNode end)
    {
        openNodes = new List<PathNode>();
        closedNodes = new List<PathNode>();

        startNode = start;
        endNode = end;

        frontierNode = null;
        route = null;

        foundEnd = false;
    }
    public PathRoute GetRoute()
    {
        return route;
    }
    public bool StartOperation()
    {
        Debug.Log("starting operation");

        SetUpFirstNode();

        while (foundEnd == false)
        {
            DetermineFrontierNode();

            if (frontierNode == null)
            {
                return false;
            }

            EvaluateNode(frontierNode);

            //Debug.Log(openNodes.Count);
        }

        if (!DetermineRoute())
        {
            return false;
        }

        return true;
    }
    private void SetUpFirstNode()
    {
        //Debug.Log("Setting up start node.");

        PathNode startPathNode = new (startNode);
        startPathNode.SetHCost(0);
        startPathNode.SetParentNode(null, 0);

        if(startNode == endNode)
        {
            foundEnd = true;
            return;
        }

        EvaluateNode(startPathNode);
    }
    private void DetermineFrontierNode()
    {
        frontierNode = null;

        foreach(PathNode node in openNodes) 
        {
            if(frontierNode == null)
            {
                frontierNode = node;
                continue;
            }

            if(node.GetFCost() < frontierNode.GetFCost())
            {
                frontierNode = node;
                continue;
            }
        }

        if(frontierNode != null)
        {
            //Debug.Log("New frontier node is " + frontierNode.GetBaseNode().GetWorldPos());
        }
        else
        {
            Debug.LogError("ERROR - Could not get a new frontier node.");
        }
    }
    private bool DetermineRoute()
    {
        PathNode endPNode = GetNodeExists(endNode);

        if(endPNode == null)
        {
            Debug.LogError("ERROR - Could not find end node when trying to make route.");
            return false;
        }
        
        route = new PathRoute();

        PathNode currentNode = endPNode;

        while(currentNode.GetParentNode() != null)
        {
            route.AddPointToPath(currentNode.GetBaseNode());

            currentNode = currentNode.GetParentNode();
        }

        route.AddPointToPath(currentNode.GetBaseNode());

        route.InvertRoute();

        return true;
    }
    private void EvaluateNode(PathNode node)
    {
        if(node == null)
        {
            Debug.LogError("ERROR - Node to evaluate is null");
            return;
        }

        foreach(GridNode nNode in node.GetBaseNode().GetNeighbours())
        {
            PathNode npNode = GetNodeExists(nNode);

            if(npNode == null)
            {
                npNode = SetUpNewOpenNode(nNode, node);
                continue;
            }

            if (npNode.GetEvalState() == PathNodeEvalState.Closed)
            {
                continue;
            }

            float prospectiveGValue = GetDistanceNodeToNode(npNode, node) + node.GetGCost();

            if (prospectiveGValue < npNode.GetGCost())
            {
                npNode.SetParentNode(node, prospectiveGValue);
            }
        }

        openNodes.Remove(node);
        node.SetEvalState(PathNodeEvalState.Closed);
        closedNodes.Add(node);
    }
    private PathNode SetUpNewOpenNode(GridNode node,PathNode parent)
    {
        //Debug.Log("Opening node " + node.GetGridPos());

        PathNode pNode = new (node);
        
        pNode.SetHCost(GetHueristicValue(pNode));

        float newGCost = GetDistanceNodeToNode(pNode, parent) + parent.GetGCost();
        pNode.SetParentNode(parent, newGCost);

        openNodes.Add(pNode);

        if(node == endNode)
        {
            foundEnd = true;
        }

        return pNode;
    }
    private PathNode GetNodeExists(GridNode node)
    {
        foreach(PathNode pNode in openNodes)
        {
            if(pNode.GetBaseNode() == node)
            {
                return pNode;
            }
        }

        foreach(PathNode pNode in closedNodes)
        {
            if(pNode.GetBaseNode() == node) 
            {
                return pNode;
            }
        }

        return null;
    }
    private float GetDistanceNodeToNode(PathNode node1, PathNode node2)
    {
        float distance = (node1.GetBaseNode().GetWorldPos() - node2.GetBaseNode().GetWorldPos()).magnitude;

        return distance;
    }
    private float GetHueristicValue(PathNode node)
    {
        float distance = (endNode.GetWorldPos() - node.GetBaseNode().GetWorldPos()).magnitude;

        return distance;
    }
}
