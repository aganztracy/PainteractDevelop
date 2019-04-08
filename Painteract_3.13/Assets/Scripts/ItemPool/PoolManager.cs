using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//为了方便后期识别和管理，用枚举来标记对象在池中的状态
//对象的池状态
public enum PoolItemState
{
    Work,
    Idle
} 

//管理对象池，原理也很简单，就是用一个容器，再来统一的管理所有的对象池，用管理器来创建或销毁对象池，以及对池的子对象进行的操作。
public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    //Dictionary查找、添加、删除 速度较快，不过内存占用相对较多
    Dictionary<int, PoolItemBase> itemPrefabDic;
    Dictionary<int, Transform> workParentDic;
    Dictionary<int, Transform> idleParentDic;
    Dictionary<int, List<PoolItemBase>> workListDic;
    Dictionary<int, List<PoolItemBase>> idleListDic;
    void Awake()
    {
        instance = this;
        itemPrefabDic = new Dictionary<int, PoolItemBase>();
        workParentDic = new Dictionary<int, Transform>();
        idleParentDic = new Dictionary<int, Transform>();
        workListDic = new Dictionary<int, List<PoolItemBase>>();
        idleListDic = new Dictionary<int, List<PoolItemBase>>();
    }
    //注册一个对象
    public int InitNewPool(PoolItemBase poolItem, Transform parentTrans = null)
    {
        //判断是否已注册
        if (itemPrefabDic.ContainsValue(poolItem))
        {
            Debug.Log("<color=red> 注册的 poolIndex 已存在   </color>" + poolItem);
            return -1;
        }
        //生成对象池Index，在销毁时某个对象池，长度会缩减，获取的值将会重复，所以不能用itemPrefabDic.Count
        //int poolIndex = itemPrefabDic.Count;
        int poolIndex = GetNewPoolIndex();
        //记录PoolItemBase
        itemPrefabDic.Add(poolIndex, poolItem);
        //设置子对象的父物体
        if (parentTrans == null)
        {
            //如果未指定父物体，生成
            parentTrans = new GameObject(poolItem.name + "_WorkParent_CanDestroy").transform;
            parentTrans.parent = transform;
        }
        workParentDic.Add(poolIndex, parentTrans);
        Transform idleParentTrans = new GameObject(poolItem.name + "_IdleParent").transform;
        idleParentTrans.gameObject.SetActive(false);
        idleParentTrans.parent = transform;
        idleParentDic.Add(poolIndex, idleParentTrans);
        //用List保存子对象
        workListDic.Add(poolIndex, new List<PoolItemBase>());
        idleListDic.Add(poolIndex, new List<PoolItemBase>());
        //返回新注册对象池的Index
        return poolIndex;
    }
    //获取池对象
    public PoolItemBase GetOneItem(int poolIndex)
    {
        //判断是否在池内
        if (!itemPrefabDic.ContainsKey(poolIndex))
        {
            Debug.Log("<color=red> 获取的 poolIndex 不存在  </color>" + poolIndex);
            return null;
        }
        PoolItemBase tmpGetItem = null;
        if (idleListDic[poolIndex].Count > 0)
        {
            //如果有多余Item
            tmpGetItem = idleListDic[poolIndex][0];
            tmpGetItem.transform.SetParent(workParentDic[poolIndex]);
            idleListDic[poolIndex].RemoveAt(0);
        }
        else
        {
            //如果没有，加载
            tmpGetItem = Instantiate(itemPrefabDic[poolIndex], transform.position, Quaternion.identity, workParentDic[poolIndex]);
        }
        workListDic[poolIndex].Add(tmpGetItem);
        //初始化
        tmpGetItem.InitPool(poolIndex);
        //
        return tmpGetItem;
    }
    //回收池对象
    public void DestroyOneItem(int poolIndex, PoolItemBase item)
    {
        if (!itemPrefabDic.ContainsKey(poolIndex))
        {
            Debug.Log("<color=red> 回收的 poolIndex 不存在   </color>" + poolIndex);
            return;
        }
        //将工作的Item回收
        if (!idleListDic[poolIndex].Contains(item))
            idleListDic[poolIndex].Add(item);
        if (workListDic[poolIndex].Contains(item))
            workListDic[poolIndex].Remove(item);
        item.transform.SetParent(idleParentDic[poolIndex]);
        //重置
        item.ResetPool();
    }
    //清空对象池
    public void ClearPool(int poolIndex)
    {
        if (!itemPrefabDic.ContainsKey(poolIndex))
        {
            Debug.Log("<color=red> 回收的 pool 不存在   </color>" + poolIndex);
            return;
        }
        //遍历，回收
        for (int i = 0; i < workListDic[poolIndex].Count; i++)
        {
            DestroyOneItem(poolIndex, workListDic[poolIndex][i]);
        }
        workListDic[poolIndex].Clear();
    }
    //销毁一个对象池
    public void DestroyPool(int poolIndex)
    {
        if (!itemPrefabDic.ContainsKey(poolIndex))
        {
            Debug.Log("<color=red> 销毁的的 pool 不存在   </color>" + poolIndex);
            return;
        }
        //销毁/清空
        if (workParentDic[poolIndex].name.EndsWith("CanDestroy", System.StringComparison.Ordinal))
            Destroy(workParentDic[poolIndex].gameObject);
        else
        {
            ClearPool(poolIndex);
        }
        Destroy(idleParentDic[poolIndex].gameObject);
        //数据移除
        itemPrefabDic.Remove(poolIndex);
        workParentDic.Remove(poolIndex);
        idleParentDic.Remove(poolIndex);
        workListDic.Remove(poolIndex);
        idleListDic.Remove(poolIndex);
    }
    int indexNum = 0;
    int GetNewPoolIndex()
    {
        return indexNum++;
    }
}
