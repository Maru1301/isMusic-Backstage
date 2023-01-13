using iSMusic.Models.EFModels;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iSMusic.Models.Infrastructures
{
	public abstract class BaseSortInfo<T> where T : class
	{
		public abstract string[] ColumnNames { get; }

		protected string[] directionNames = new string[] { "Asc", "Desc" };

		public abstract IQueryable<T> ApplySort(IQueryable<T> data);

		public BaseSortInfo(string columnName, string direction, string defaultColumnName)
		{
			//this.ColumnName = Enum.TryParse(columnName, out P111Controller.SortInfo.EnumColumn colValue) ? colValue : P111Controller.SortInfo.EnumColumn.CityDisplayOrder;

			this.ColumnName = this.ColumnNames.Contains(columnName) ? columnName : defaultColumnName;

			this.Direction = Enum.TryParse(direction, out EnumDirection directionValue)
				? directionValue
				: EnumDirection.Asc;
		}


		public string UrlTemplate { get; set; }

		public MvcHtmlString RenderHiddenInfo()
		{
			var tag01 = new TagBuilder("input");
			tag01.Attributes.Add("type", "hidden");
			tag01.Attributes.Add("name", "ColumnName");
			tag01.Attributes.Add("value", this.ColumnName.ToString());

			var tag02 = new TagBuilder("input");
			tag02.Attributes.Add("type", "hidden");
			tag02.Attributes.Add("name", "Direction");
			tag02.Attributes.Add("value", this.Direction.ToString());

			return new MvcHtmlString(tag01.ToString() + tag02.ToString());
		}

		public MvcHtmlString RenderItem(string column)
		{
			string template = "<a href=\"{0}\">{1}</a>&nbsp;<a href=\"{2}\">{3}</a>";
			string urlAsc = GetUrl(column, EnumDirection.Asc);
			string urlDesc = GetUrl(column, EnumDirection.Desc);
			string iconAsc = RenderIcon(column, EnumDirection.Asc);
			string iconDesc = RenderIcon(column, EnumDirection.Desc);

			return new MvcHtmlString(string.Format(template, urlAsc, iconAsc, urlDesc, iconDesc));
		}

		public string GetQueryString()
		{
			string template = "ColumnName={0}&Direction={1}";
			return string.Format(template, this.ColumnName, this.Direction);
		}

		private string RenderIcon(string column, EnumDirection direction)
		{
			if (this.ColumnName == column && this.Direction == direction)
			{
				return (direction == EnumDirection.Asc)
					? "<i style='color:red;'>↑</i>"
					: "<i style='color:red;'>↓</i>";
			}

			return (direction == EnumDirection.Asc)
				? "<i>↑</i>"
				: "<i>↓</i>";
		}

		private string GetUrl(string column, EnumDirection direction)
		{
			if (this.ColumnName == column && this.Direction == direction)
			{

				return "";
			}

			string args = $"columnName={column}&direction={direction}";
			return string.Format(UrlTemplate, args);
		}

		public string ColumnName { get; set; }
		public EnumDirection Direction { get; set; }

		public bool IsAsc() //EnumDirection direction)
		{
			return this.Direction == EnumDirection.Asc;
		}
		public bool IsDesc() //EnumDirection direction)
		{
			return this.Direction == EnumDirection.Desc;
		}

		public enum EnumDirection
		{
			Asc, Desc
		}
	}
}