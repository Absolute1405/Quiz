using System.Collections;

public interface ILevelLoader 
{
    public IEnumerator LoadNew(float delay);
    public string CurrentAnswer { get; }
}
