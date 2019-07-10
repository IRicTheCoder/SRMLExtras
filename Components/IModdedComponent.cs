namespace SRMLExtras.Components
{
	public interface IModdedComponent
	{
		void SetOriginal(object original);

		void LoadFromOriginal(object original);
	}
}
