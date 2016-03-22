using System;
using System.Text;
using System.Web.Mvc;
using BookStore.WebUI.Models;

namespace BookStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            // For each page, create a link to that page.
            for (int i = 1; i <= pagingInfo.TotalPages; ++i)
            {
                // Generate tag and url.
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));

                // Write out the page number.
                tag.InnerHtml = i.ToString();

                // If dealing with the link to the current page, add some CSS so that it can be styled.
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }

                // Add a CSS class and then append this link to the string.
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
                result.Append("  ");
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}
