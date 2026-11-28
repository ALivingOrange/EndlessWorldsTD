/***************************************************************
*file: TowerFactory.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/14/2026
*
*purpose: Deals with the construction of the towers.
*         Takes in the data of three tower parts and forms them into a singular tower.
*
****************************************************************/
using Unity.VisualScripting;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [Header("Testing Fields")]
    public TowerBaseData towerBase;
    public TowerCoreData towerCore;
    public TowerHeadData towerHead;
    [Header("Testing Options")]
    [SerializeField] private bool spawnTestTower = false;
    // function: CreateTower
    // Purpose: This is the function that'll actually make the tower units.
    //          We make the TowerBaseManager GameObject which takes the TowerBaseData parameter.
    //          After which we also make a core and a head with the TowerCoreData and TowerHeadData objects
    //          We use the TowerBaseManager's AttachCore/AttachHead function to... attach the tower parts, returning the TowerBaseObject when everything's finished.
    public TowerBaseManager CreateTower(TowerBaseData baseData, TowerCoreData coreData, TowerHeadData headData, Vector3 spawnPosition)
    {
        GameObject baseObject = Instantiate(baseData.basePrefab, spawnPosition, Quaternion.identity);

        TowerBaseManager baseManager = baseObject.GetComponent<TowerBaseManager>();
        baseManager.baseData = baseData;

        GameObject coreObject = Instantiate(coreData.corePrefab);
        TowerCoreManager coreManager = coreObject.GetComponent<TowerCoreManager>();
        coreManager.coreData = coreData;
        baseManager.AttachCore(coreManager);

        GameObject headObject = Instantiate(headData.headPrefab);
        TowerHeadManager headManager = headObject.GetComponent<TowerHeadManager>();
        headManager.headData = headData;
        baseManager.AttachHead(headManager);

        return baseManager;
    }

    // function: Start
    // Purpose: Called on the frame when this script is enabled.
    //          As of 11/14/2026, acts as a test for TowerCreate.
// function: Start
// purpose: Spawns a test tower only when testing is enabled.
    void Start()
    {
        if (spawnTestTower)
        {
            CreateTower(towerBase, towerCore, towerHead, Vector3.zero);
        }
}   

    // Update is called once per frame
    void Update()
    {
        
    }
}
