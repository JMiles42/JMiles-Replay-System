using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JMiles42.IO.Generic {
	public static class SavingLoading {
		public const string FILE_EXT = ".FoCsdat";

		[System.Obsolete("Use the one where you supply the defualt data. defualt(T) is not reliable")]
		public static void LoadGameDataUnityFile<T>(string name, out T data, string exten = FILE_EXT) {
			//Does space magic if file exists
			if (File.Exists(Application.persistentDataPath + "/" + name + exten))//Does the file exist
			{
				//Space Magic
				var bf = new BinaryFormatter();
				var filepath = Application.persistentDataPath + "/" + name + exten;
				var file = File.Open(
									 filepath,//Name file
									 FileMode.Open);
				Debug.Log("Loaded : " + filepath);
				data = (T) bf.Deserialize(file);
				file.Close();//End file
			}
			else
				data = default (T);
		}

		public static void LoadGameDataUnityFile<T>(string name, out T data, T defualtData, bool saveIfNotExist = true, string exten = FILE_EXT) {
			//Does space magic if file exists
			if (File.Exists(Application.persistentDataPath + "/" + name + exten))//Does the file exist
			{
				//Space Magic
				var bf = new BinaryFormatter();
				var filepath = Application.persistentDataPath + "/" + name + exten;
				var file = File.Open(
									 filepath,//Name file
									 FileMode.Open);
				Debug.Log("Loaded : " + filepath);
				data = (T) bf.Deserialize(file);
				file.Close();//End file
			}
			else {
				data = defualtData;
				if (saveIfNotExist)
					SaveGameDataUnityFile(name, defualtData);
			}
		}

		public static void SaveGameDataUnityFile<T>(string name, T data, string exten = FILE_EXT) {
			//Space Magic
			var bf = new BinaryFormatter();
			var filepath = Application.persistentDataPath + "/" + name + exten;
			var file = File.Open(
								 filepath,//Name file
								 FileMode.OpenOrCreate//How to open file
								);
			Debug.Log("Saved : " + filepath);
			bf.Serialize(file, data);//Magic happens
			file.Close();//End file
		}

		public static void LoadGameDataFilepath<T>(string filepath, out T data, T defualtData, bool saveIfNotExist = true) {
			//Does space magic if file exists
			if (File.Exists(filepath))//Does the file exist
			{
				//Space Magic
				var bf = new BinaryFormatter();
				var file = File.Open(
									 filepath,//Name file
									 FileMode.Open);
				Debug.Log("Loaded : " + filepath);
				data = (T) bf.Deserialize(file);
				file.Close();//End file
			}
			else {
				data = defualtData;
				if (saveIfNotExist)
					SaveGameDataFilepath(filepath, defualtData);
			}
		}

		public static void SaveGameDataFilepath<T>(string filepath, T data) {
			//Space Magic
			var bf = new BinaryFormatter();
			var file = File.Open(
								 filepath,//Name file
								 FileMode.OpenOrCreate//How to open file
								);
			Debug.Log("Saved : " + filepath);
			bf.Serialize(file, data);//Magic happens
			file.Close();//End file
		}
	}
}