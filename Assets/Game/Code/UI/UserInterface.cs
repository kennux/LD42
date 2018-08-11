using UnityEngine;
using UnityTK;
using UnityEngine.SceneManagement;

public class UserInterface : SingletonBehaviour<UserInterface>
{
    public Ship ship
    {
        get { return Ship.instance; }
    }

    public float travelProgress
    {
        get { return Game.instance.travelProgress; }
    }

    public float invTravelProgress
    {
        get { return 1f - this.travelProgress; }
    }

    public float wallOfDeathProgress
    {
        get { return Game.instance.wallOfDeathProgress; }
    }

    public float invWallOfDeathProgress
    {
        get { return 1f - this.wallOfDeathProgress; }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}