using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public string prefabPath;
    public PlayerContro playerContro;

    private void Awake()
    {
        instance = this;
    }

    public void OnLoaded()
    {
        GameObject go = Addressables.LoadAssetAsync<GameObject>(prefabPath).WaitForCompletion();          
        GameObject game_obj = Instantiate(go, this.gameObject.transform);
        playerContro = game_obj.GetComponent<PlayerContro>();  
        playerContro.Init();
    }

    public Transform GetPlayerTransform()
    {
        return playerContro.transform;
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
