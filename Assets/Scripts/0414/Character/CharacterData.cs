using UnityEngine;

public class CharacterData {
	public string Id { get; set; }
	public string Name { get; set; }
	public string Job { get; set; }
	public int Attack { get; set; }
	public int MagicAttack { get; set; }
	public int Defence { get; set; }
	public string Icon { get; set; }
	
	// CharacterTableManager에서 읽어와야 할 값들
	public string JobString => DataTableManager.StringTable.Get(Job);
	public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");
	public string AbilityText => $"{DataTableManager.StringTable.Get("Attack")} : {Attack}\n" +
	                             $"{DataTableManager.StringTable.Get("MagicAttack")} : {MagicAttack}\n" +
	                             $"{DataTableManager.StringTable.Get("Defence")} : {Defence}";
}