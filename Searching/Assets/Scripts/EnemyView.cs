using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] ThermalView thermalView;
    public bool isVisible;
    SkinnedMeshRenderer enemyMesh;
    
    // Update is called once per frame

    private void Start()
    {
        enemyMesh = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        if (thermalView.thermalEnabled == false)
        {
            enemyMesh.enabled = true;
            isVisible = true;
        }
        else
        {
            enemyMesh.enabled = false;
            isVisible = false;
        }
    }
}
