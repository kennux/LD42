using UnityEngine;
using UnityTK;

public class OxygenDamage : MonoBehaviour
{
    public float oxygenLevelDamageThreshold = 0.25f;
    public float oxygenDamageMin = 0.1f;
    public float oxygenDamageMax = 2.5f;

    public void Update()
    {
        float o = Ship.instance.oxygen.percentage;
        if (o < this.oxygenLevelDamageThreshold)
        {
            float dmg = 1f - (o / this.oxygenLevelDamageThreshold);
            dmg = Mathf.Lerp(this.oxygenDamageMin, this.oxygenDamageMax, dmg);

            this.GetComponent<HealthMechanic>().takeDamage.Fire(dmg * Time.deltaTime);
        }
    }
}