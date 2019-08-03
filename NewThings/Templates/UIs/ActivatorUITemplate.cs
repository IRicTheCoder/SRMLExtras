namespace SRMLExtras.Templates
{
	/// <summary>
	/// A template to create new activator UIs
	/// </summary>
	public class ActivatorUITemplate : ModPrefab<ActivatorUITemplate>
	{
		// The UI component
		protected Create<BaseUI> baseUI;

		/// <summary>
		/// Template to create new activator UIs
		/// </summary>
		/// <param name="name">The name of the object (prefixes are recommended, but not needed)</param>
		/// <param name="baseUI">The UI component to add</param>
		public ActivatorUITemplate(string name, Create<BaseUI> baseUI) : base(name)
		{
			this.baseUI = baseUI;
		}

		/// <summary>
		/// Creates the object of the template (To get the prefab version use .ToPrefab() after calling this)
		/// </summary>
		public override ActivatorUITemplate Create()
		{
			// Create main object
			mainObject.AddComponents(
				baseUI,
				new Create<UIInputLocker>((locker) => locker.lockEvenSpecialScenes = false)
			);

			return this;
		}
	}
}
