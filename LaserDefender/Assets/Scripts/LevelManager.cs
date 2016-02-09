using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		Debug.Log("Level load requested for: " + name);
		Application.LoadLevel(name);
	}
	
	public void Quit(){
		Debug.Log("Quit requested");
		Application.Quit();
	}

	public void LoadNextLevel(){
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	
}
