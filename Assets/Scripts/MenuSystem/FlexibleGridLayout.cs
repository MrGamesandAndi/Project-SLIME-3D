using UnityEngine;
using UnityEngine.UI;

public enum FitType
{
	UNIFORM,
	WIDTH,
	HEIGHT,
	FIXEDROWS,
	FIXEDCOLUMNS
}

public class FlexibleGridLayout : LayoutGroup
{
	#region Variables
	public FitType fitType;
	public int rows;
	public int columns;
	public Vector2 cellSize;
	public Vector2 spacing;
	public bool fitX;
	public bool fitY;
	#endregion

	#region Basic Methods
	public override void CalculateLayoutInputVertical()
	{
		base.CalculateLayoutInputHorizontal();

		if (fitType == FitType.WIDTH || fitType == FitType.HEIGHT || fitType==FitType.UNIFORM)
		{
			fitX = true;
			fitY = true;
			float sqrt = Mathf.Sqrt(transform.childCount);
			rows = Mathf.CeilToInt(sqrt);
			columns = Mathf.CeilToInt(sqrt);
		}

		if (fitType == FitType.WIDTH || fitType == FitType.FIXEDCOLUMNS)
		{
			rows = Mathf.CeilToInt(transform.childCount / columns);
		}

		if (fitType == FitType.HEIGHT || fitType == FitType.FIXEDROWS)
		{
			columns = Mathf.CeilToInt(transform.childCount / rows);
		}

		float parentWidth = rectTransform.rect.width;
		float parentHeight = rectTransform.rect.height;
		float cellWidth = parentWidth / columns - ((spacing.x / columns) * (columns - 1)) - (padding.left / columns) - (padding.right / columns);
		float cellHeight = parentHeight / rows - ((spacing.y / rows) * (rows - 1)) - (padding.top / rows) - (padding.bottom / rows);
		cellSize.x = fitX ? cellWidth : cellSize.x;
		cellSize.y = fitY ? cellHeight : cellSize.y;
		int columnCount = 0;
		int rowCount = 0;

		for (int i = 0; i < rectChildren.Count; i++)
		{
			rowCount = i / columns;
			columnCount = i % columns;
			var item = rectChildren[i];
			var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
			var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;
			SetChildAlongAxis(item, 0, xPos, cellSize.x);
			SetChildAlongAxis(item, 1, yPos, cellSize.y);
		}
	}

	public override void SetLayoutHorizontal()
	{
		
	}

	public override void SetLayoutVertical()
	{

	}
	#endregion
}
