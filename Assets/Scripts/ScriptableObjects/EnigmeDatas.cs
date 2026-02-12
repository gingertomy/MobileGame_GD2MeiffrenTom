using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EnigmeDatas", menuName = "Scriptable Objects/EnigmeDatas")]
public class EnigmeDatas : ScriptableObject
{
    public bool[] LampActivated;
    public int[] lampNumber;


    public bool IsAllActivated()
    {
        return !LampActivated.Contains(false);

     


        //for (int i = 0; i < LampActivated.Length; i++) 
        //{ if (!LampActivated[i]) 
        //    { 
        //        return false; 
        //    } 
        //} 
        //return true;
    }

}
