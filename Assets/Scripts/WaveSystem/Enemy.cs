﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyTypeData{ 
	Normal,
	Small,
	Fast,
	Strong
}

public enum SizeModificator{
	Normal = 10,
	Small = 5,
	Big = 15
}

public struct EnemyData{
	public int health;
	public float speed;
	public int carryMetal;
	public float Damage;

	public EnemyData(int health, float speed, int metal, float damage){
		this.health = health;
		this.speed = speed;
		this.carryMetal = metal;
		this.Damage = damage;
	}
}

[System.Serializable]
public class Enemy  {
	public EnemyTypeData type;
	public SizeModificator size = SizeModificator.Normal;
	public int amount;
	public GameObject EnemyPrefab;

	public EnemyData Data{
		get{ 
			switch (type) {
			case EnemyTypeData.Normal:
				return new EnemyData(5	,2.5f	,1		,2);
			case EnemyTypeData.Small:
				return new EnemyData(2	,3f		,1		,1);
			case EnemyTypeData.Fast:
				return new EnemyData(3	,4f		,3		,2);
			case EnemyTypeData.Strong:
				return new EnemyData(20	,1f		,5		,10);
			default:
				return new EnemyData(1	,1f		,1 		,1);
			}
		}
	}
}
