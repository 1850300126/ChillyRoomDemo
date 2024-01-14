using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using EasyUpdateDemoSDK;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
public class GameLoader : MonoBehaviour
{   
    public static GameLoader instance;
    private AsyncOperationHandle<SceneInstance> current_scene_handle;
    private void Awake()
    {   
        instance = this;
        
        DontDestroyOnLoad(this);

        // LoadMainScene();
    }
    public EnemyManager enemyManager;
    public PlayerManager playerManager;
    public WeaponManager weaponManager;
    public GameLogicManager gameLogicManager;

    public GameObject mainSceneGrid;
    public CinemachineVirtualCamera specialVM;

    public void LoadMoudle()
    {   
        Time.timeScale = 1f;

        weaponManager = LoadMoudle("Assets/Prefab/System/WeaponManager.prefab").GetComponent<WeaponManager>();
        weaponManager.OnLoaded();

        playerManager = LoadMoudle("Assets/Prefab/System/PlayerManager.prefab").GetComponent<PlayerManager>();  
        playerManager.OnLoaded();

        enemyManager = LoadMoudle("Assets/Prefab/System/EnemyManager.prefab").GetComponent<EnemyManager>();
        enemyManager.OnLoaded();

        gameLogicManager = LoadMoudle("Assets/Prefab/System/GameLogicManager.prefab").GetComponent<GameLogicManager>();
        gameLogicManager.OnLoaded();

        GameUIManager.Instance.gameUI.SetActive(true);
    }

    public void LoadMainScene()
    {
        LoadStage("Assets/Scenes/MainMenu.unity", LoadMoudle);
    }
    public void LoadStage(string path, UnityAction unityAction)
    {
        if (path.EndsWith(".unity") == true)
        
        StartCoroutine(LoadStageFromScene(path, unityAction));
    }
    public IEnumerator LoadStageFromScene(string prefab_path, UnityAction on_load_finish)
    {
        current_scene_handle = Addressables.LoadSceneAsync(prefab_path, LoadSceneMode.Additive);

        SceneInstance scene_instance = current_scene_handle.WaitForCompletion();

        yield return scene_instance.ActivateAsync();

        SceneManager.SetActiveScene(scene_instance.Scene);

        yield return null;

        on_load_finish?.Invoke();
    }

    public void StartGame()
    {

        GameUIManager.Instance.GameStartUILogic();

        mainSceneGrid.GetComponentInChildren<Animator>().enabled = true;
        

        DOVirtual.Float
        (
            5f, 3f, 1f, (targetValue) =>
            {
                specialVM.m_Lens.OrthographicSize = targetValue;
            }
        );
        DOVirtual.Float
        (
            3f, 0f, 1f, (targetValue) =>
            {
                specialVM.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, targetValue, 0);
            }
        ).OnComplete( () => specialVM.gameObject.SetActive(false));

        DOVirtual.Float
        (
            0f, 1f, 1f, (targetValue) =>
            {
                if(targetValue >= 1)
                    Destroy(mainSceneGrid);
            }
        ).OnComplete(LoadMainScene);
    }

    public GameObject LoadMoudle(string path)
    {
        GameObject go = Addressables.LoadAssetAsync<GameObject>(path).WaitForCompletion();
        GameObject game_obj = Instantiate(go, this.gameObject.transform);       
        return game_obj;
    }

    public void GameReStart()
    {
        DestroyMoudle();

        LoadMoudle();
    }

    public void DestroyMoudle()
    {
        playerManager.DestroySelf();
        enemyManager.DestroySelf();
        weaponManager.DestroySelf();
        gameLogicManager.DestroySelf();
    }
}
