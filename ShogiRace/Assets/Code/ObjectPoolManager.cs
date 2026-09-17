using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] protected GameObject m_prefab;
    [SerializeField] protected int m_poolCount;

    protected List<GameObject> m_objectPool;
    protected List<int> m_inUseQue;

    private Transform m_transform;
}
