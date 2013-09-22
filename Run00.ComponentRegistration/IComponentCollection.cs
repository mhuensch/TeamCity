namespace Run00.ComponentRegistration
{
	public interface IComponentCollection
	{
		IComponent<T> AddFor<T>() where T : class;
	}
}