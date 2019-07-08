using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SRML.Components.Plots
{
	/// <summary>
	/// A modded version of the garden UI
	/// </summary>
	public class ModdedGardenUI : GardenUI, IModdedComponent
	{
		public static List<PurchaseUI.Purchasable> moddedPurchasables = new List<PurchaseUI.Purchasable>();

		protected override GameObject CreatePurchaseUI()
		{
			List<PurchaseUI.Purchasable> purchasables = new List<PurchaseUI.Purchasable>();

			purchasables.Add(new PurchaseUI.Purchasable("m.upgrade.name.garden.soil", soil.icon, soil.img, "m.upgrade.desc.garden.soil", soil.cost, new PediaDirector.Id?(PediaDirector.Id.GARDEN), new UnityAction(this.UpgradeSoil), () => true, () => !activator.HasUpgrade(LandPlot.Upgrade.SOIL), null, null, null, null));
			purchasables.Add(new PurchaseUI.Purchasable("m.upgrade.name.garden.sprinkler", sprinkler.icon, sprinkler.img, "m.upgrade.desc.garden.sprinkler", sprinkler.cost, new PediaDirector.Id?(PediaDirector.Id.GARDEN), new UnityAction(this.UpgradeSprinkler), () => true, () => !activator.HasUpgrade(LandPlot.Upgrade.SPRINKLER), null, null, null, null));
			purchasables.Add(new PurchaseUI.Purchasable("m.upgrade.name.garden.scareslime", scareslime.icon, scareslime.img, "m.upgrade.desc.garden.scareslime", scareslime.cost, new PediaDirector.Id?(PediaDirector.Id.GARDEN), new UnityAction(this.UpgradeScareslime), () => true, () => !activator.HasUpgrade(LandPlot.Upgrade.SCARESLIME), null, null, null, null));
			purchasables.Add(new PurchaseUI.Purchasable("m.upgrade.name.garden.miracle_mix", miracleMix.icon, miracleMix.img, "m.upgrade.desc.garden.miracle_mix", miracleMix.cost, new PediaDirector.Id?(PediaDirector.Id.GARDEN), new UnityAction(this.UpgradeMiracleMix), () => SRSingleton<SceneContext>.Instance.ProgressDirector.GetProgress(ProgressDirector.ProgressType.OGDEN_REWARDS) >= 1, () => !activator.HasUpgrade(LandPlot.Upgrade.MIRACLE_MIX), null, null, null, null));
			purchasables.Add(new PurchaseUI.Purchasable("m.upgrade.name.garden.deluxe", deluxe.icon, deluxe.img, "m.upgrade.desc.garden.deluxe", deluxe.cost, new PediaDirector.Id?(PediaDirector.Id.GARDEN), new UnityAction(this.UpgradeDeluxe), () => SRSingleton<SceneContext>.Instance.ProgressDirector.GetProgress(ProgressDirector.ProgressType.OGDEN_REWARDS) >= 2, () => !activator.HasUpgrade(LandPlot.Upgrade.DELUXE_GARDEN), null, null, null, null));

			purchasables.AddRange(moddedPurchasables);

			purchasables.Add(new PurchaseUI.Purchasable(MessageUtil.Qualify("ui", "b.clear_crop"), clearCrop.icon, clearCrop.img, MessageUtil.Qualify("ui", "m.desc.clear_crop"), clearCrop.cost, null, new UnityAction(this.ClearCrop), () => activator.HasAttached(), () => true, null, null, null, null));
			purchasables.Add(new PurchaseUI.Purchasable(MessageUtil.Qualify("ui", "l.demolish_plot"), demolish.icon, demolish.img, MessageUtil.Qualify("ui", "m.desc.demolish_plot"), demolish.cost, null, new UnityAction(this.Demolish), () => true, () => true, "b.demolish", null, null, null));

			return SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(titleIcon, "t.garden", purchasables.ToArray(), false, new PurchaseUI.OnClose(Close), false);
		}

		/// <summary>
		/// Loads information from the original component
		/// </summary>
		public void LoadFromOriginal(object original)
		{
			if (!(original is GardenUI))
				return;

			GardenUI gardenUI = (GardenUI)original;

			soil = gardenUI.soil;
			sprinkler = gardenUI.sprinkler;
			scareslime = gardenUI.scareslime;
			miracleMix = gardenUI.miracleMix;
			deluxe = gardenUI.deluxe;
			clearCrop = gardenUI.clearCrop;
			demolish = gardenUI.demolish;
			titleIcon = gardenUI.titleIcon;
			plantButtonPanelObject = gardenUI.plantButtonPanelObject;
		}
	}
}
