using UnityEngine;

public class SkipperDestroy : MonoBehaviour
{
    [SerializeField] GameObject Skipper;
   public void DestroySkipper()
    {
        if(Skipper != null)
        Destroy(Skipper);
    }
}
