using System.Runtime.Remoting.Lifetime;
using System.Runtime.Serialization.Formatters;

public class CivCar : Unit {
    public int LifeTime = 2;

    private void HandleTurn() {
        LifeTime--;
        if (LifeTime < 1) {
            
        }
    }
}