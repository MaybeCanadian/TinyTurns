using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Objects/Object")]
public class ObjectData : ScriptableObject
{
    public string ObjectName = "Name";

    public EntityList entityModel = EntityList.NULL;
}
