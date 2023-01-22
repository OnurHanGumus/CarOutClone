using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using Enums;

public class PoolManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private GameObject particlePrefab2;
    [SerializeField] private GameObject particlePrefab3;
    [SerializeField] private GameObject particlePrefab4;

    [SerializeField] private Dictionary<PoolEnums, List<GameObject>> poolDictionary;


    [SerializeField] private int amountParticle = 2;



    #endregion
    #region Private Variables
    private int _levelId = 0;
    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _levelId = LevelSignals.Instance.onGetCurrentModdedLevel();
        poolDictionary = new Dictionary<PoolEnums, List<GameObject>>();

        InitializePool(PoolEnums.Particle, particlePrefab, amountParticle);
        InitializePool(PoolEnums.Particle2, particlePrefab2, amountParticle);
        InitializePool(PoolEnums.Particle3, particlePrefab3, amountParticle);
        InitializePool(PoolEnums.Particle4, particlePrefab4, amountParticle);
    }



    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PoolSignals.Instance.onGetPoolManagerObj += OnGetPoolManagerObj;
        PoolSignals.Instance.onGetObject += OnGetObject;
        PoolSignals.Instance.onGetObjectOnPosition += OnGetObjectWithFix;
        CoreGameSignals.Instance.onRestartLevel += OnReset;

    }

    private void UnsubscribeEvents()
    {
        PoolSignals.Instance.onGetPoolManagerObj -= OnGetPoolManagerObj;
        PoolSignals.Instance.onGetObject -= OnGetObject;
        PoolSignals.Instance.onGetObjectOnPosition -= OnGetObjectWithFix;
        CoreGameSignals.Instance.onRestartLevel -= OnReset;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void InitializePool(PoolEnums type, GameObject prefab, int size)
    {
        List<GameObject> tempList = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < size; i++)
        {
            tmp = Instantiate(prefab, transform);
            tmp.SetActive(false);
            tempList.Add(tmp);
        }
        poolDictionary.Add(type, tempList);
    }

    public GameObject OnGetObject(PoolEnums type)
    {
        for (int i = 0; i < poolDictionary[type].Count; i++)
        {
            if (!poolDictionary[type][i].activeInHierarchy)
            {
                return poolDictionary[type][i];
            }
        }
        return null;
    }

    public GameObject OnGetObjectWithFix(PoolEnums type, Vector3 position)
    {
        for (int i = 0; i < poolDictionary[type].Count; i++)
        {
            if (!poolDictionary[type][i].activeInHierarchy)
            {
                poolDictionary[type][i].transform.position = position;
                poolDictionary[type][i].gameObject.SetActive(true);

                return poolDictionary[type][i];
            }
        }
        return null;
    }

    public Transform OnGetPoolManagerObj()
    {
        return transform;
    }


    private void OnReset()
    {
        //reset
        ResetPool(PoolEnums.Particle);
        ResetPool(PoolEnums.Particle2);
        ResetPool(PoolEnums.Particle3);
        ResetPool(PoolEnums.Particle4);
    }

    private void ResetPool(PoolEnums type)
    {
        foreach (var i in poolDictionary[type])
        {
            i.SetActive(false);
        }
    }
}
