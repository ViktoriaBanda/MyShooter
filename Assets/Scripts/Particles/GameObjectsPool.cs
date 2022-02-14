using System.Collections.Generic;
using Pool;
using UnityEngine;

public class GameObjectsPool 
{
    private Dictionary<GameObject, GameObjectPool> _pooledObjects = new();

    public GameObjectsPool(int poolSize, params GameObject[] gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
            var objectPool = new GameObjectPool(gameObject, poolSize);
            _pooledObjects[gameObject] = objectPool;
        }
    }

    public GameObject Get(GameObject prefab)
    {
        return _pooledObjects[prefab].Take();
    }
    
    public void Release(GameObject prefab, GameObject gameObject)
    {
        _pooledObjects[prefab].Release(gameObject);
    }
}