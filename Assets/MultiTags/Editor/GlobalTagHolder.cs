using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class GlobalTagHolder : ScriptableObject {


	public List<MT> GlobalTagList = new List<MT> ();

	private static GlobalTagHolder _tagHolder;
	public static GlobalTagHolder TagHolder
	{
		get
		{
			if(_tagHolder == null)
			{
				_tagHolder = GetOrCreate<GlobalTagHolder>(Name) as GlobalTagHolder;
			}
			return _tagHolder;
		}
	}

	#region Load,Save,Check if Exists
	protected const string DataPath =  "Assets/MultiTags/";



	public static string Name { 
		get{
			return "GlobalMultiTagHolder.asset";

		}
	}

	/// <summary>
	/// Save the current settings.
	/// </summary>
	public void Save()
	{
		var setting = GetAsset(DataPath + Name) as ScriptableObject;
//		Debug.Log (setting + DataPath + Name);
		if (setting) // See if this was the initial save.
		{
		//	PrefabUtility.ReplacePrefab(setting, this, ReplacePrefabOptions.ReplaceNameBased); // We do this to keep references.
			EditorUtility.CopySerialized(this,setting);
		}
		else
		{
			CreateAsset(DataPath + Name);
		}
		
		AssetDatabase.SaveAssets();
	}



	/// <summary>
	/// Creates an asset version of the current asset.
	/// </summary>
	/// <returns></returns>
	protected void CreateAsset(string path)
	{
		CreateAsset(this, path);
	}
	
	/// <summary>
	/// Creates and saves the new instance or gets the asset.
	/// </summary>
	/// <returns></returns>
	public static ScriptableObject GetOrCreate<T>(string name)
	{
		var settings = GetAsset(DataPath + name) as ScriptableObject;
		if (settings) return settings; // Exit out early if settings exists already.
		
		settings = CreateEmptyAsset<T>(); // Create a new one if it doesnt
		return settings; // Return that
	}
	
	/// <summary>
	/// Creates an empty settings instance, this is not saved.
	/// </summary>
	/// <returns></returns>
	protected static ScriptableObject CreateEmptyAsset<T>() 
	{
		var settings = ScriptableObject.CreateInstance(typeof(T));
		return settings;
	}

	/// <summary>
	/// Will return true if asset exists at path.
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	public static bool AssetExistsAtPath(string path)
	{
		return AssetDatabase.LoadAssetAtPath(path, typeof (Object)) != null;
	}
	/// <summary>
	/// Get an asset at path.
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	public static Object GetAssetAtPath(string path)
	{
		return AssetDatabase.LoadAssetAtPath(path, typeof (Object));
	}


	/// <summary>
	/// Get asset if they exist
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public static GlobalTagHolder GetAsset(string path)
	{
		var temp = GetAssetAtPath(path) as GlobalTagHolder;
		return temp;
	}
	/// <summary>
	/// Get asset if it exists.
	/// </summary>
	/// <returns></returns>
	public static Object GetAsset(bool returnObject, string path)
	{
		return GetAsset(path);
	}


	public static Object CreateAsset(Object objectToCreate, string path)
	{
		Debug.Log("Creating Asset");
		//CreateFolder(path);
		Directory.CreateDirectory (Path.GetDirectoryName(path));
		AssetDatabase.CreateAsset(objectToCreate, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		return AssetDatabase.LoadAssetAtPath(path, typeof (Object));
	}
	#endregion
}






