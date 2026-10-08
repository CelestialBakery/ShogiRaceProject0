using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    

    public void Ready(List<GameObject> racerList)
    {
        for (int i = 0; i < racerList.Count; i++)
        {
            racerList[i].gameObject.SetActive(true);
            //Instantiate(racerList[i]);
        }
    }
}
