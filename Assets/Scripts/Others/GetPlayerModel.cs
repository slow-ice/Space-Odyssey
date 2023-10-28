using Assets.Architecture;
using Assets.Scripts.Model.Player;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 获取有关玩家的数据，用于UI数据
// IController 为qFramework 的数据交互架构
public class GetPlayerModel : Singleton<GetPlayerModel>, IController
{
    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }

    public PlayerModel pm;

    [Header("public just for debug")]
    public float hp;
    public float mp;

    // Start is called before the first frame update
    void Start()
    {
        pm = this.GetModel<PlayerModel>();
    }

    // Update is called once per frame
    void Update()
    {
        hp = (float)pm.Health;
        mp = (float)pm.Energy;
    }
}
