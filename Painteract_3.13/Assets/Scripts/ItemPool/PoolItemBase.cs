//因为是对池对象的统一管理，给其添加一个父类，关于继承和多态，能搜到各种详细的资料~~
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//池对象父类
public abstract class PoolItemBase : MonoBehaviour
{
    protected PoolItemState itemState;
    protected int poolIndex;
    //初始化
    public void InitPool(int poolInde)
    {
        itemState = PoolItemState.Work;
        poolIndex = poolInde;
        gameObject.name = "item_" + poolIndex;
        Init();
    }
    //重置，销毁时调用
    public void ResetPool()
    {
        itemState = PoolItemState.Idle;
        transform.localPosition = Vector3.zero;
        Reset();
    }
    public abstract void Init();
    public abstract void Reset();
}
//到这里对象池就算写完咯，关于其中的数据结构，需要根据实际项目的数据量及数据操作方式，选择合适的方案，如果数据比较多，字典就不是最佳选择了···
