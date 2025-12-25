namespace Platformer.New
{
    public interface IDashable
    {
        void Dash(float h, float v);
        
        bool IsDashing { get; }
    }
}