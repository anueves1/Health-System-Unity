using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Item
{
    public GameObject Prefab => m_Prefab;
    public int Amount => m_Amount;
    public float YOffset => m_YOffset;

    [SerializeField]
    private GameObject m_Prefab;

    [SerializeField]
    private int m_Amount;

    [SerializeField]
    private float m_YOffset;
}