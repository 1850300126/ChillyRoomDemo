using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// /// <summary>
// /// ��ͨ�� ���� ���������
// /// </summary>
// public class ObjectPoolData
// {
//     public ObjectPoolData(object obj)
//     {
//         PushObj(obj);
//     }
//     // ��������
//     public Queue<object> poolQueue = new Queue<object>();
//     /// <summary>
//     /// ������Ž������
//     /// </summary>
//     public void PushObj(object obj)
//     {
//         poolQueue.Enqueue(obj);
//     }

//     /// <summary>
//     /// �Ӷ�����л�ȡ����
//     /// </summary>
//     /// <returns></returns>
//     public object GetObj()
//     {
//         return poolQueue.Dequeue();
//     }
// }
// /// <summary>
// /// GameObject���������
// /// </summary>
// public class GameObjectPoolData
// {
//     // ������� ���ڵ�
//     public GameObject fatherObj;
//     // ��������
//     public Queue<GameObject> poolQueue;

//     public GameObjectPoolData(GameObject obj, GameObject poolRootObj)
//     {
//         // �������ڵ� �����õ�����ظ��ڵ��·�
//         fatherObj = new GameObject(obj.name);
//         fatherObj.transform.SetParent(poolRootObj.transform);
//         poolQueue = new Queue<GameObject>();
//         // ���״δ���ʱ�� ��Ҫ����Ķ��� �Ž�����
//         PushObj(obj);
//     }


//     /// <summary>
//     /// ������Ž������
//     /// </summary>
//     public void PushObj(GameObject obj)
//     {
//         // ���������
//         poolQueue.Enqueue(obj);
//         // ���ø�����
//         obj.transform.SetParent(fatherObj.transform);
//         // ��������
//         obj.SetActive(false);
//     }

//     /// <summary>
//     /// �Ӷ�����л�ȡ����
//     /// </summary>
//     /// <returns></returns>
//     public GameObject GetObj(Transform parent = null)
//     {
//         GameObject obj = poolQueue.Dequeue();

//         // ��ʾ����
//         obj.SetActive(true);
//         // ���ø�����
//         obj.transform.SetParent(parent);
//         if (parent == null)
//         {
//             // �ع�Ĭ�ϳ���
//             UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(obj, UnityEngine.SceneManagement.SceneManager.GetActiveScene());
//         }

//         return obj;
//     }
// }
public class PoolSystem : MonoBehaviour
{   
//     public static PoolSystem instance;
//     // ���ڵ�
//     [SerializeField]
//     private GameObject poolRootObj;
//     /// <summary>
//     /// GameObject��������
//     /// </summary>
//     public Dictionary<string, GameObjectPoolData> gameObjectPoolDic = new Dictionary<string, GameObjectPoolData>();
//     /// <summary>
//     /// ��ͨ�� ��������
//     /// </summary>
//     public Dictionary<string, ObjectPoolData> objectPoolDic = new Dictionary<string, ObjectPoolData>();

//     private void Awake()
//     {
//         instance = this;
//     }

//     #region GameObject������ز���

//     /// <summary>
//     /// ��ȡGameObject,�������û���򷵻�Null
//     /// </summary>
//     public GameObject GetGameObject(string assetName, Transform parent = null)
//     {
//         GameObject obj = null;
//         // �����û����һ��
//         if (gameObjectPoolDic.TryGetValue(assetName, out GameObjectPoolData poolData) && poolData.poolQueue.Count > 0)
//         {
//             obj = poolData.GetObj(parent);
//         }
//         return obj;
//     }

//     /// <summary>
//     /// GameObject�Ž������
//     /// </summary>
//     public void PushGameObject(GameObject obj)
//     {
//         string name = obj.name;
//         // ������û����һ��
//         if (gameObjectPoolDic.TryGetValue(name, out GameObjectPoolData poolData))
//         {
//             poolData.PushObj(obj);
//         }
//         else
//         {
//             gameObjectPoolDic.Add(name, new GameObjectPoolData(obj, poolRootObj));
//         }
//     }

//     #endregion

//     #region ��ͨ������ز���
//     /// <summary>
//     /// ��ȡ��ͨ����
//     /// </summary>
//     public T GetObject<T>() where T : class, new()
//     {
//         T obj;
//         if (CheckObjectCache<T>())
//         {
//             string name = typeof(T).FullName;
//             obj = (T)objectPoolDic[name].GetObj();
//             return obj;
//         }
//         else
//         {
//             return new T();
//         }
//     }

//     /// <summary>
//     /// GameObject�Ž������
//     /// </summary>
//     /// <param name="obj"></param>
//     public void PushObject(object obj)
//     {
//         string name = obj.GetType().FullName;
//         // ������û����һ��
//         if (objectPoolDic.ContainsKey(name))
//         {
//             objectPoolDic[name].PushObj(obj);
//         }
//         else
//         {
//             objectPoolDic.Add(name, new ObjectPoolData(obj));
//         }
//     }

//     private bool CheckObjectCache<T>()
//     {
//         string name = typeof(T).FullName;
//         return objectPoolDic.ContainsKey(name) && objectPoolDic[name].poolQueue.Count > 0;
//     }
//     #endregion


//     #region ɾ��
//     /// <summary>
//     /// ɾ��ȫ��
//     /// </summary>
//     /// <param name="clearGameObject">�Ƿ�ɾ����Ϸ����</param>
//     /// <param name="clearCObject">�Ƿ�ɾ����ͨC#����</param>
//     public void Clear(bool clearGameObject = true, bool clearCObject = true)
//     {
//         if (clearGameObject)
//         {
//             for (int i = 0; i < poolRootObj.transform.childCount; i++)
//             {
//                 Destroy(poolRootObj.transform.GetChild(i).gameObject);
//             }
//             gameObjectPoolDic.Clear();
//         }

//         if (clearCObject)
//         {
//             objectPoolDic.Clear();
//         }
//     }

//     public void ClearAllGameObject()
//     {
//         Clear(true, false);
//     }
//     public void ClearGameObject(string prefabName)
//     {
//         GameObject go = poolRootObj.transform.Find(prefabName).gameObject;
//         if (ReferenceEquals(go, null) == false)
//         {
//             Destroy(go);
//             gameObjectPoolDic.Remove(prefabName);

//         }

//     }
//     public void ClearGameObject(GameObject prefab)
//     {
//         ClearGameObject(prefab.name);
//     }

//     public void ClearAllObject()
//     {
//         Clear(false, true);
//     }
//     public void ClearObject<T>()
//     {
//         objectPoolDic.Remove(typeof(T).FullName);
//     }
//     public void ClearObject(Type type)
//     {
//         objectPoolDic.Remove(type.FullName);
//     }
//     #endregion


    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public Pool()
        {

        }
        public Pool(string tag, GameObject prefab, int size)
        {
            this.tag = tag;
            this.prefab = prefab;
            this.size = size;
        }
    }

    [SerializeField]
    public List<Pool> pools = new List<Pool>();

    Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
    Dictionary<string, GameObject> pools_dictionary = new Dictionary<string, GameObject>();

    public static PoolSystem instance;    //����ģʽ�����ڷ��ʶ����

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// �Ӷ�����л�ȡ����
    /// </summary>
    /// <param name="tag">param[0]</param>
    /// <param name="positon">param[1]</param>
    /// <param name="rotation">param[2]</param>
    /// <returns>������Ʒ</returns>
    public GameObject PushFromPool(string tag, Vector3 postion, Quaternion rotation)     //�Ӷ�����л�ȡ����ķ���
    {
        if (!poolDictionary.ContainsKey(tag))  //���������ֵ��в���������Ķ����
        {
            // tag does not exist
            Debug.Log("Pool: " + tag + " does not exist");
            return null;
        }
        GameObject objectToSpawn;
        // poolDictionary[tag]
        if (poolDictionary[tag].Count <= 0)
        {
            // pools_dictionary[tag]
            objectToSpawn = Instantiate(pools_dictionary[(string)tag], transform);
        }
        else
        {
            // poolDictionary[tag]
            objectToSpawn = poolDictionary[(string)tag].Dequeue();//���ӣ��Ӷ�����л�ȡ����Ķ���
        }


        objectToSpawn.transform.position = postion;  //���û�ȡ���Ķ����λ��
        objectToSpawn.transform.rotation = rotation; //���ö������ת
        objectToSpawn.SetActive(true);                //�������������Ϊ����

        //poolDictionary[(string)param[0]].Enqueue(objectToSpawn);     //�ٴ���ӣ������ظ�ʹ�ã������Ҫ�Ķ�����������������ڶ�����������ڿ�����������
        //�����ظ�ʹ�þͲ���һֱ���ɺ����Ķ��󣬽�Լ�˴�������
        return objectToSpawn;  //���ض���
    }

    /// <summary>
    /// �Ӷ�����л�ȡ����,Ȼ���time�����
    /// </summary>
    /// <param name="tag">param[0]</param>
    /// <param name="positon">param[1]</param>
    /// <param name="rotation">param[2]</param>
    /// <param name="time">param[3]</param>
    /// <returns>������Ʒ</returns>
    public GameObject PushFromPoolAndDistoryByTime(string tag, Vector3 postion, Quaternion rotation, float time)
    {
        GameObject go = PushFromPool(tag, postion, rotation);
        StartCoroutine(PushFromPoolDistroy(tag, go, time));
        return go;
    }

    private IEnumerator PushFromPoolDistroy(string tag, GameObject objectToSpawn, float time)
    {
        yield return new WaitForSeconds(time);
        PullFromPool(tag, objectToSpawn);
    }

    /// <summary>
    /// ж�ض���ķ���
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="objectToSpawn"></param>
    public void PullFromPool(string tag, GameObject objectToSpawn)
    {
        objectToSpawn.SetActive(false);
        if (!poolDictionary[tag].Contains(objectToSpawn))
            poolDictionary[tag].Enqueue(objectToSpawn);

    }

    /// <summary>
    /// ��Ӷ����(��ǩ��GameObject������)
    /// </summary>
    public object AddPool(string targetTag, GameObject targetObj, int tagetSize)
    {
        // �ж��Ƿ�����
        foreach(Pool _pool in pools)
        {
            if(_pool.tag.Contains(targetTag))
            {
                return null;
            }
        }
        Pool pool = new Pool(targetTag, targetObj, tagetSize);

        pools.Add(pool);

        UpdateDictionary(pool);

        return null;
    }

    /// <summary>
    /// ���¶�����ֵ������������� poolDictionary & pools_dictionary
    /// </summary>
    public void UpdateDictionary(Pool pool)
    {
        Queue<GameObject> objectPool = new Queue<GameObject>();     //Ϊÿ������ش�������
        for (int i = 0; i < pool.size; i++)
        {
            GameObject obj = Instantiate(pool.prefab, transform);
            obj.SetActive(false);   //���ض�����еĶ���
            objectPool.Enqueue(obj);//���������
        }
        // ͨ��tag����Queue
        poolDictionary.Add(pool.tag, objectPool);   //��ӵ��ֵ�����ͨ��tag�����ٷ��ʶ����
                                                    // ͨ��tag����prefab
        pools_dictionary.Add(pool.tag, pool.prefab);
    }
}
