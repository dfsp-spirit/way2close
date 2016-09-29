using UnityEngine;
using System.Collections;

public abstract class LevelController : MonoBehaviour {

    public abstract bool GetLevelHasFixedDuration();
    public abstract float GetLevelDuration();
}
