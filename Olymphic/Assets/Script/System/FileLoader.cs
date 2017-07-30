using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileLoader : Singleton<FileLoader>
{
	protected FileLoader () {}	// Singletone use.

	public string LoadTextAssetToResources(string fileName)
	{
		string path = string.Format("Data/{0}",fileName);

		//Load texture from disk
		TextAsset bindata= Resources.Load(path) as TextAsset;
		Debug.Log(bindata.text);
		return bindata.text;
	}
}
