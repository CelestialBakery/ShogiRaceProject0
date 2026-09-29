using UnityEngine;

public class ReturnObjectPool : MonoBehaviour
{
    private ObjectPoolManager m_poolManager;
    private int m_poolIndex;
    private bool m_isPrimed;

    public void InjectPoolManager(ObjectPoolManager poolManager, int poolIndex)
    {
        m_poolManager = poolManager;
        m_poolIndex = poolIndex;
        m_isPrimed = true;
    }

    public void ReturnToPool()
    {
        if (!m_isPrimed) return;
        m_poolManager.ReturnToPool(m_poolIndex);
    }
}
