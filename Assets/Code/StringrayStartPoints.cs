using UnityEngine;

public class StringrayStartPoints : MonoBehaviour
{
    public static Transform[] startpoints;
	void Awake()
	{
		startpoints = new Transform[transform.childCount];
		for(int i = 0; i < startpoints.Length; i++)
        {
			startpoints[i] = transform.GetChild(i);
        }
        
	}
}
