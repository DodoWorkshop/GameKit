using System.Collections;

public interface ICommandResolver
{
    public void ResolveWithCoroutine(IEnumerator coroutine);

    public void Resolve();
}
