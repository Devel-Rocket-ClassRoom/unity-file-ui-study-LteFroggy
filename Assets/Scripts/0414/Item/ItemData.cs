using UnityEngine;

/* Item 데이터를 전달하기 위한 데이터 모델 */
public class ItemData {
	public string Id { get; set; }
	public ItemTypes Type { get; set; }
	public string Name { get; set; }
	public string Desc { get; set; }
	public int Value { get; set; }
	public int Cost { get; set; }
	public string Icon { get; set; }
	
	public override string ToString() {
		return $"{Id} / {Type} / {Name} / {Desc} / {Value} / {Cost} / {Icon}";
	}
	
	public string StringName => DataTableManager.StringTable.Get(Name);
	public string StringDesc => DataTableManager.StringTable.Get(Desc);
	public string StringType => DataTableManager.StringTable.Get(Type.ToString());
	public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");
}