using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;



//Base 75 Reimplimentation of the Unity tag system to allow for multiple tags on the same game object.

public class MultiTags : MonoBehaviour {


	[Tooltip("This is the list of current Tags on this GameObject")]
	public List<MT> localTagList = new List<MT>();


	public void Awake()
	{

		//This adds the unity tag to the MultiTag List when the object becomes awake.
		//You can Comment out this Line to not use Unity Tags
		gameObject.AddTag (gameObject.tag);
	
	}


	public static GameObject FindWithMultiTag(string tag)
	{


		MultiTags[] tempMT = GameObject.FindObjectsOfType(typeof(MultiTags)) as MultiTags[];

	 	
		foreach (MultiTags itemMT in tempMT) {

			foreach (var itemtag in itemMT.localTagList) {
				
				if (string.Equals(itemtag.Name,tag,StringComparison.CurrentCultureIgnoreCase))
					{
						return itemMT.gameObject;
					}
				}
			
			}


		return null;
		
	}


	public static GameObject[] FindGameObjectsWithMultiTag(string tag)
	{
		
			
		MultiTags[] tempMT = GameObject.FindObjectsOfType(typeof(MultiTags)) as MultiTags[];

		List<GameObject> tempGOList = new List<GameObject> ();
		
		foreach (MultiTags itemMT in tempMT) {
			
			foreach (var itemtag in itemMT.localTagList) {
				
				if (string.Equals(itemtag.Name,tag,StringComparison.CurrentCultureIgnoreCase))
				{
					tempGOList.Add(itemMT.gameObject);
				}
			}
			
		}


		if (tempGOList.Count > 0) 
		{
				
				return tempGOList.ToArray();
			} 
		else {
				
		
			return null;
		}


	}

	public static int FindGameObjectsWithMultiTagCount(string tag)
	{
		
		
		MultiTags[] tempMT = GameObject.FindObjectsOfType(typeof(MultiTags)) as MultiTags[];
		
		List<GameObject> tempGOList = new List<GameObject> ();
		
		foreach (MultiTags itemMT in tempMT) {
			
			foreach (var itemtag in itemMT.localTagList) {
				
				if (string.Equals(itemtag.Name,tag,StringComparison.CurrentCultureIgnoreCase))
				{
					tempGOList.Add(itemMT.gameObject);
				}
			}
            
        }
        
        
		return tempGOList.Count; 
    }
	
}






public static class MultiTagsHelperMethods
{

	//HAS TAG GameObject Extention
	public static bool HasTag (this GameObject go, string tagToCheck)
	{
		
		MultiTags CurrentGameComponent = go.GetComponent<MultiTags> ();
		
		if (CurrentGameComponent == null) 
		{

			return false;
			
		}
		
		foreach (var item in CurrentGameComponent.localTagList) {
			
			//item.Name.ToLower() == tagToCheck.ToLower()
			if (string.Equals(item.Name,tagToCheck,StringComparison.CurrentCultureIgnoreCase))
			{
				return true;
			}
		}
		return false;
	}



	//ADD TAG GameObject Extention
	public static void AddTag (this GameObject go, string newTag)
	{

		MultiTags CurrentGameComponent = go.GetComponent<MultiTags> ();
		
		if (CurrentGameComponent == null) 
		{
			go.AddComponent<MultiTags>();
			CurrentGameComponent = go.GetComponent<MultiTags> ();
			
		}
		
		if (!HasTagPrivate (CurrentGameComponent,newTag)) {
			
			MT newItem = new MT();
			newItem.Name = newTag;
			
		 CurrentGameComponent.localTagList.Add(newItem);
			
		}
		
	}


	//REMOVE TAG GameObject Extention
	public static void RemoveTag (this GameObject go, string tag)
	{
		MultiTags CurrentGameComponent = go.GetComponent<MultiTags> ();
		
		if (CurrentGameComponent == null) 
		{
			return;
			
		}
		
		MT tempItem = GetTagItem (CurrentGameComponent,tag);
		
		if (tempItem == null) {
			
			return;
		}
		
		
		CurrentGameComponent.localTagList.Remove(tempItem);
		
	}

	//HAS TAG Private
	private static bool HasTagPrivate (MultiTags go, string tagToCheck)
	{
		
		
		foreach (var item in go.localTagList) {
			
			//item.Name.ToLower() == tagToCheck.ToLower()
			if (string.Equals(item.Name,tagToCheck,StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }



	//Private GetTagItem
	private static MT GetTagItem (MultiTags CGC, string tagToCheck)
	{
		
		foreach (var item in  CGC.localTagList) {
			
			
			if (string.Equals(item.Name,tagToCheck,StringComparison.CurrentCultureIgnoreCase))
			{
				
				return item;
				
			}
			
		}
		
		return null;
		
	}






}


[System.Serializable] 
public class MT
{
	public string Name;
	public byte ID;
	
}


