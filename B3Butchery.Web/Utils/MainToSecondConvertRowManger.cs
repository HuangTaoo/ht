using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebControls2;

namespace BWP.Web.Utils
{
	public class MainToSecondConvertRowManger
	{
		readonly DFEditGridColumn<DFTextBox> _mainNumberInput;
		readonly DFEditGridColumn<DFTextBox> _secondNumberInput;
		readonly DFEditGridColumn<DFTextBox> _priceInput;
		readonly DFEditGridColumn<DFTextBox> _moneyInput;
		readonly string _mainNumberField;
		readonly string _secondNumberField;
		readonly string _moneyField;
		readonly bool _hasPrice;
		readonly bool _hasMoney;
		public MainToSecondConvertRowManger(DFGridBase grid)
			: this(grid, "Number", "SecondNumber", "Money")
		{

		}


		public MainToSecondConvertRowManger(DFGridBase grid, string mainfield, string secondfield, string moneyfield)
		{
			_mainNumberField = mainfield;
			_secondNumberField = secondfield;
			_moneyField = moneyfield;
			_hasMoney = _hasPrice = false;
			foreach (DFGridColumn column in grid.Columns)
			{
				if (column is DFEditGridColumn<DFTextBox>)
				{
					var c = (DFEditGridColumn<DFTextBox>)column;
					if (c.Name == mainfield)
						_mainNumberInput = c;
					else if (c.Name == secondfield)
						_secondNumberInput = c;
					else if (c.Name == moneyfield)
					{
						_moneyInput = c;
						_hasMoney = true;
					}
					else if (c.Name == "Price")
					{
						_priceInput = c;
						_hasPrice = true;
					}
				}
				else if (column is DFEditGridColumn)
				{
					var c1 = (DFEditGridColumn)column;
					if (c1.Name == _moneyField)
					{
						_hasMoney = true;
					}
					else if (c1.Name == "Price")
					{
						_hasPrice = true;
					}
				}
			}
			SetClientScript();
		}

		string SetMainNumber
		{
			get
			{
				return string.Format("dfContainer.setValue('{0}', dfContainer.getValue('{1}')*{2}/{3})", _mainNumberField, _secondNumberField, MainUnitRatio, SecondUnitRatio);
			}
		}

		string SetSecondNumber
		{
			get
			{
				return string.Format("dfContainer.setValue('{0}', dfContainer.getValue('{1}')*{2}/{3})", _secondNumberField, _mainNumberField, SecondUnitRatio, MainUnitRatio);
			}
		}

		string SetMoney
		{
			get
			{
				return string.Format("dfContainer.setValue('{0}', dfContainer.getValue('Price')*dfContainer.getValue('{1}'));", _moneyField, _mainNumberField);
			}
		}

		string SetMainNumberByMoney
		{
			get
			{
				return string.Format("dfContainer.setValue('{0}', dfContainer.getValue('{1}')/dfContainer.getValue('Price'));", _mainNumberField, _moneyField);
			}
		}

		private const string ConvertDirection = "dfContainer.getValue('Goods_UnitConvertDirection')";
		private const string MainUnitRatio = "dfContainer.getValue('Goods_MainUnitRatio')";
		private const string SecondUnitRatio = "dfContainer.getValue('Goods_SecondUnitRatio')";


		private void SetClientScript()
		{
			SetMainNumberChanged();
			SetSecondNumberChanged();
			SetPriceChanged();
			SetMoneyChanged();
		}

		private void SetMoneyChanged()
		{
			if (_moneyInput == null)
				return;
			var builder = new StringBuilder();

			if (_hasPrice && _mainNumberInput != null)
			{
				builder.Append(SetMainNumberByMoney);
			}

			_moneyInput.InitEditControl += delegate(object sender, InitEditControlEventArgs<DFTextBox> e)
			{
				e.Control.Attributes["onchange"] = builder.ToString();
			};
		}

		private void SetSecondNumberChanged()
		{
			if (_secondNumberInput == null)
				return;

			var builder = new StringBuilder();

			if (_mainNumberInput != null)
			{
				builder.Append(@"if({convertDirection}=='双向转换'||{convertDirection}=='由辅至主'){ if({ratioRight}>0)
				{setMainNumber}}"
					.Replace("{convertDirection}", ConvertDirection)
					.Replace("{ratioRight}", SecondUnitRatio)
					.Replace("{setMainNumber}", SetMainNumber));
				if (_hasPrice && _hasMoney)
				{
					builder.Append(SetMoney);
				}
			}

			_secondNumberInput.InitEditControl += delegate(object sender, InitEditControlEventArgs<DFTextBox> e)
			{
				e.Control.Attributes["onchange"] = builder.ToString();
			};
		}

		private void SetMainNumberChanged()
		{
			if (_mainNumberInput == null)
				return;

			var builder = new StringBuilder();

			if (_secondNumberInput != null)
			{
				builder.Append(@"if({convertDirection}=='双向转换'||{convertDirection}=='由主至辅'){ if({ratioLeft}>0)
				{setSecondNumber}}"
					.Replace("{convertDirection}", ConvertDirection)
					.Replace("{ratioLeft}", MainUnitRatio)
					.Replace("{setSecondNumber}", SetSecondNumber));
			}
			if (_hasPrice && _hasMoney)
			{
				builder.Append(SetMoney);
			}
			_mainNumberInput.InitEditControl += delegate(object sender, InitEditControlEventArgs<DFTextBox> e)
			{
				e.Control.Attributes["onchange"] = builder.ToString();
			};
		}


		private void SetPriceChanged()
		{
			if (_priceInput == null)
				return;

			var builder = new StringBuilder();

			if (_hasMoney && _mainNumberInput != null)
			{
				builder.Append(SetMoney);
			}
			_priceInput.InitEditControl += delegate(object sender, InitEditControlEventArgs<DFTextBox> e)
			{
				e.Control.Attributes["onchange"] = builder.ToString();
			};
		}
	}
}
