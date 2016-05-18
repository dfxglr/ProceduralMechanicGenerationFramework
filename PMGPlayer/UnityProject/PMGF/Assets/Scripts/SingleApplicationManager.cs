using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using PMGF.PMGUnity.Managers;
using PMGF.PMGGameInstance;


//this is the main program application, manages other managers


//-----------------------------------------------------------------------// unity singleton for gameManager
public class SingleApplicationManager : MonoBehaviour
{
    //variables

    //current instances set
    PMGGenomeSet testset = new PMGGenomeSet();

    //current instance
    PMGSingleGameInstance ThisForNow;


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
        //game instance
        //ThisForNow = new PMGSingleGameInstance 

        SingleMenuManager.Instance.Setup(true);
        SingleInputManager.Instance.Setup(false);
        SingleMapManager.Instance.Setup();
    }
    //runs all the time
    public void Update()
    {
        //input for menu
        SingleInputManager.Instance.InputMenuUpdate();
    }
    //-----------------------------------------------------------------------//
}
