using System.Collections.Generic;
using UnityEngine;

public class ParticlesPool 
{
    private List<GameObject> _notUsedItems;
    private List<GameObject> _usedItems;
    private GameObject _itemPrefab;

    public ParticlesPool(GameObject itemPrefab,int _poolSize)
    {
        _itemPrefab = itemPrefab;
        _notUsedItems = new List<GameObject>();
        _usedItems = new List<GameObject>();

        for (var index = 0; index < _poolSize; index++)
        {
            var newItem = Object.Instantiate(itemPrefab);
            newItem.SetActive(false);
            _notUsedItems.Add(newItem);
        }
    }

    public GameObject Take() 
    {
        if (_notUsedItems.Count == 0)
        {
            AddPoolElements();
        }

        var itemToSpawn = _notUsedItems[_notUsedItems.Count - 1];
        _notUsedItems.RemoveAt(_notUsedItems.Count - 1);
        
        _usedItems.Add(itemToSpawn);

        itemToSpawn.SetActive(true);
        return itemToSpawn;
    }
    
    public void Release(GameObject item)
    {
        item.SetActive(false);
        //item.transform.position = itemsPosition;
        _usedItems.Remove(item);
        _notUsedItems.Add(item);
    }
    
    public void ReleaseAll()
    {
        for (int i = 0; i < _usedItems.Count; i++)
        {
            _usedItems[i].gameObject.SetActive(false);
            //_usedItems[i].transform.position = itemsPosition;
        }

        _notUsedItems.AddRange(_usedItems);
        _usedItems.Clear();
    }

    private void AddPoolElements()
    {
        var newItem = Object.Instantiate(_itemPrefab);
        newItem.SetActive(false);
        _notUsedItems.Add(newItem);
    }
}
