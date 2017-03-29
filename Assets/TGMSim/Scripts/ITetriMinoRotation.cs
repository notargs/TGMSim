namespace TGMSim.Scripts
{
    public interface ITetriMinoRotation
    {

        int Id { get;}

        ITetriMinoRotation RotateRight();
        ITetriMinoRotation RotateLeft();
    }
}