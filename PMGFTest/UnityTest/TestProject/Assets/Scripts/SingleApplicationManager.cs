using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using PMFG.PMGUnity.Managers;

//this is the main program application, manages other managers


//-----------------------------------------------------------------------// unity singleton for gameManager
public class SingleApplicationManager : MonoBehaviour
{
    protected SingleApplicationManager() { }
    private static SingleApplicationManager instance = null;
    public static SingleApplicationManager Instance
    {
        get
        {
            if (SingleApplicationManager.instance == null)
            {
                // dont destroy on load lets the game manager survive when switching scenes
                DontDestroyOnLoad(SingleApplicationManager.instance);
                SingleApplicationManager.instance = new SingleApplicationManager();
            }
            return SingleApplicationManager.instance;
        }

    }
    //-----------------------------------------------------------------------//
    public static SingleApplicationManager InstanceOBJ { get; private set; }

    private void Awake()
    {
        if (InstanceOBJ != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        InstanceOBJ = this;
        DontDestroyOnLoad(gameObject);
    }
    // might not be needed for now
    public void OnApplicationQuit()
    {
        SingleApplicationManager.instance = null;
    }
    //-----------------------------------------------------------------------//*/
    // runs once in the beginning(Awake() is before this however)
    public void Start()
    {
        SingleMenuManager.Instance.Setup(true);
        SingleInputManager.Instance.Setup(false);
        SingleMapManager.Instance.Setup();
    }
    //runs all the time
    public void Update()
    {
        SingleInputManager.Instance.InputUpdate();
    }
    //-----------------------------------------------------------------------//
}
