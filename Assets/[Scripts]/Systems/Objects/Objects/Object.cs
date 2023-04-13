using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Object
{
    #region Event Dispatchers
    public delegate void ObjectEvent();
    public ObjectEvent OnObjectMoved;
    public ObjectEvent OnObjectRemoved;
    #endregion

    public Vector3 worldPos = Vector3.zero;
    public Vector2Int gridPos = Vector2Int.zero;
    public ObjectTypeFilters objType = ObjectTypeFilters.None;

    protected int direction = 1;

    public GridNode currentGridNode = null;

    public ObjectData data = null;
    public GameObject objectOBJ = null;

    public EntityList entityID = EntityList.NULL;
    public string objectName = "default name";

    #region Init Functions
    public Object(ObjectData data)
    {
        this.data = data;

        SetUpObject();

        if(data != null)
        {
            entityID = data.entityModel;
            objectName = data.ObjectName;
        }
    }
    protected virtual void SetUpObject()
    {
        objType = ObjectTypeFilters.Object;
    }
    protected virtual void ConnectEvents()
    {
        GameController.OnUpdate += Update;
        GameController.OnFixedUpdate += FixedUpdate;
        GameController.OnLateUpdate += LateUpdate;
    }
    protected virtual void DisconnectEvents()
    {
        GameController.OnUpdate -= Update;
        GameController.OnFixedUpdate -= FixedUpdate;
        GameController.OnLateUpdate -= LateUpdate;
    }
    #endregion

    #region Update Functions
    protected virtual void Update(float delta) { }
    protected virtual void FixedUpdate(float fixedDelta) { }
    protected virtual void LateUpdate(float delta) { }
    #endregion

    #region Movement
    public void PlaceObjectAtGridPos(Vector2Int gridPos) 
    {
        LeaveGridNode();

        this.gridPos = gridPos;

        currentGridNode = GridManager.GetGridNode(gridPos.x, gridPos.y);

        if(!GridManager.GetWorldPosFromGridPos(gridPos, out this.worldPos))
        {

        }

        AddToGridNode();

        if(objectOBJ != null)
        {
            MoveObjToPosition();
        }
    }
    #endregion

    #region Visuals
    public virtual void CreateVisuals(bool remake = false)
    {
        if(objectOBJ != null)
        {
            if(remake == true)
            {
                DestroyVisuals();
            }
            else
            {
                //we skip as we already have visuals
                return;
            }
        }

        GameObject model = EntityModelDataBase.GetModel(entityID);

        if(model == null)
        {
            Debug.LogError("ERROR - Could not make object visuals as the model returned null.");
            return;
        }

        objectOBJ = GameObject.Instantiate(model);

        if(objectOBJ == null)
        {
            Debug.LogError("ERROR - Could not create visuals as object visuals did not instantiate.");
            return;
        }

        ObjectManager.ConnectParent(objectOBJ);
        objectOBJ.name = objectName;

        MoveObjToPosition();
    }
    public virtual void DestroyVisuals() 
    {
        if(objectOBJ == null)
        {
            return;
        }

        GameObject.Destroy(objectOBJ);
        objectOBJ = null;
    }
    public virtual void SetOBJDirection()
    {
        if(objectOBJ == null)
        {
            return;
        }

        SpriteRenderer sr = objectOBJ.GetComponent<SpriteRenderer>();

        if(sr == null)
        {
            Debug.Log("Could not get the sprite renderer.");
            return;
        }

        sr.flipX = direction < 0 ? true : false;
    }
    protected void MoveObjToPosition()
    {
        if(objectOBJ == null)
        {
            Debug.LogError("ERROR - Could not move object visuals to position as the object visuals is null.");
            return;
        }

        objectOBJ.transform.position = worldPos;
    }
    public void SetAsFollowTarget()
    {
        if(objectOBJ == null)
        {
            Debug.LogError("ERROR - Could not set follow target as object obj is null.");
            return;
        }

        CameraFollowScript.SetFollowTarget(objectOBJ.transform);
    }
    #endregion

    #region Lifecycle
    public virtual void DestroyObject()
    {
        DisconnectEvents();

        DestroyVisuals();
        OnObjectRemoved?.Invoke();
    }
    #endregion

    #region Nodes
    private void LeaveGridNode()
    {
        if (currentGridNode != null)
        {
            currentGridNode.RemoveObjectFromNode(this);
        }
    }
    private void AddToGridNode()
    {
        if (currentGridNode != null)
        {
            currentGridNode.AddObjectToNode(this);
        }
    }
    #endregion

    #region Input
    #region Mouse Inputs
    public virtual void OnMouseDown(int button)
    {

    }
    public virtual void OnMouseUp(int button)
    {

    }
    public virtual void OnMouseHeld(int button)
    {

    }
    #endregion

    #region Mouse Movement
    public virtual void OnMouseEnter()
    {

    }
    public virtual void OnMouseExit()
    {

    }
    public virtual void OnMouseStay()
    {

    }
    #endregion
    #endregion
}
