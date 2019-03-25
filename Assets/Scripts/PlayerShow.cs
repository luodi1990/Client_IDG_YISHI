﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IDG;
using IDG.FSClient;
public class PlayerShow : NetObjectView<PlayerData> {
    // public NetInfo net;
   // public int clientId = -1;
    public Animator anim;
    private new void Start()
    {
        base.Start();
       // data.ClientId = clientId;
        anim.SetInteger("WeaponType", 1);
    }
    private new void Update()
    {
        base.Update();
        anim.SetFloat("Speed", ((PlayerData)data).move_dir.magnitude.ToFloat());
    }
    //float last;



    // Use this for initialization


    // Update is called once per frame
    //void Update () {

    //}
}
public abstract class HealthData : NetData
{
    protected Fixed _m_Hp=new Fixed(100);
    protected bool isDead=false;
    public virtual void GetHurt(Fixed atk)
    {
        if (!isDead)
        {
            _m_Hp -= atk;
            Debug.Log(this.name + " GetHurt "+atk+" Hp:"+_m_Hp);
            if (_m_Hp <= 0)
            {
                Die();
            }
        }
        
    }
    protected virtual void Die()
    {
        isDead = true;
        Debug.Log(this.name + "dead!!!");
    }
}
public class PlayerData: HealthData
{
    public Fixed move_speed = new Fixed(3);
    public Fixed2 move_dir { get; private set; }
    public SkillSystem skillList;
    public override void Start()
    {
       
        this.tag = "Player";
        skillList= AddCommponent<SkillSystem>();
        Shap = new CircleShap(new Fixed(0.25f), 8);
        rigibody.useCheck=true;
        
     
        if (IsLocalPlayer)
        {
            client.localPlayer = this;
            Debug.Log("client.localPlayer");
        }

    }
    protected override void FrameUpdate()
    {
     
       //     Debug.LogError("move"+Input.GetKey(IDG.KeyNum.MoveKey) );
         move_dir = Input.GetKey(IDG.KeyNum.MoveKey) ? Input.GetJoyStickDirection(IDG.KeyNum.MoveKey):Fixed2.zero;
   //     Debug.LogError("move"+move);
        transform.Position += move_dir * deltaTime* move_speed;
        if (move_dir.x != 0 || move_dir.y != 0)
        {
            
            transform.Rotation = Fixed.RotationLerp(transform.Rotation, move_dir.ToRotation(),new Fixed(0.5f));
        }
      
  
    }

    public override string PrefabPath()
    {
        return "Prefabs/Player";
    }
}

