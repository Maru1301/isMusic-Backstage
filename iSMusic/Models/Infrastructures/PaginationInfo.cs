using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iSMusic.Models.Infrastructures
{
	public class PaginationInfo
	{
		public PaginationInfo(int totalRecords, int pageSize, int pageNumber)
		{
			TotalRecords = totalRecords < 0 ? 0 : totalRecords;
			PageSize = pageSize < 1 ? 1 : pageSize;
			PageNumber = pageNumber < 1 ? 1 : pageNumber;
		}

		public int TotalRecords { get; set; }
		public int PageSize { get; set; }
		public int PageNumber { get; set; }

		public int Pages => (int)Math.Ceiling((double)TotalRecords / PageSize);

		public int PageItemCount => 5;

		public int PageBarStartNumber
		{
			get
			{
				int startNumber = PageNumber - ((int)Math.Floor((double)this.PageItemCount / 2));
				return startNumber < 1 ? 1 : startNumber;
			}
		}

		public IEnumerable<T> GetPagedData<T>(IEnumerable<T> query)
		{
			int recordStartIndex = (PageNumber - 1) * PageSize;

			return query.Skip(recordStartIndex).Take(PageSize);
		}

		public int PageItemPrevNumber => (PageBarStartNumber <= 1) ? 1 : PageBarStartNumber - 1;

		public int PageBarItemCount => PageBarStartNumber + PageItemCount > Pages
			? Pages - PageBarStartNumber + 1
			: PageItemCount;
		public int PageItemNextNumber => (PageBarStartNumber + PageItemCount >= Pages) ? Pages : PageBarStartNumber + PageItemCount;
	}

	public static class PaginationInfoExt
	{
		public static MvcHtmlString RenderPager(this PaginationInfo pagedInfo, Func<int, string> urlGenerator)
		{
			string result = @"
			<nav aria-label=""Page navigation"">
				<ul class=""pagination"">";

			if (pagedInfo.Pages > 0 && pagedInfo.PageNumber > 1)
			{
				string prevUrl = urlGenerator(pagedInfo.PageItemPrevNumber);
				result += $@"<li class=""page-item"">
                <a class=""page-link"" href=""{prevUrl}"" aria-label=""Previous"">
                    <span aria-hidden=""true"">&laquo;</span>
                </a>
            </li>";
			}
			else
			{
				string prevUrl = urlGenerator(pagedInfo.PageItemPrevNumber);
				result += $@"<li class=""page-item"">
                <a class=""page-link disabled"" href=""{prevUrl}"" aria-label=""Previous"">
                    <span aria-hidden=""true"">&laquo;</span>
                </a>
            </li>";
			}

			for (int i = 0; i < pagedInfo.PageBarItemCount; i++)
			{
				int currentPageNumber = pagedInfo.PageBarStartNumber + i;
				string url = urlGenerator(currentPageNumber);

				string className = pagedInfo.PageBarStartNumber + i == pagedInfo.PageNumber ? "active" : "";

				result += $@"
            <li class=""page-item {className}""><a class=""page-link"" href=""{url}"">{currentPageNumber}</a></li>";
			}

			if (pagedInfo.PageNumber < pagedInfo.Pages && pagedInfo.Pages != 1)
			{
				string nextUrl = urlGenerator(pagedInfo.PageItemNextNumber);
				result += $@"
				<li class=""page-item"">
					<a class=""page-link"" href=""{nextUrl}"" aria-label=""Next"">
						<span aria-hidden=""true"">&raquo;</span>
					</a>
				</li>";
			}
			else
			{
				string nextUrl = urlGenerator(pagedInfo.PageItemNextNumber);
				result += $@"
				<li class=""page-item disabled"">
					<a class=""page-link"" href=""{nextUrl}"" aria-label=""Next"">
						<span aria-hidden=""true"">&raquo;</span>
					</a>
				</li>";
			}

			result += @"
				</ul>
			</nav>";

			return new MvcHtmlString(result);
		}
	}
}