using System.Text;
using System.Collections.Generic;

namespace Core
{
    public class Navigation
    {
        /// <summary>
        /// Builds website navigation urls by given names and urls
        /// </summary>
        /// <param name="Names">Collection of names</param>
        /// <param name="Urls">Collection of urls</param>
        /// <returns>Navigation menu</returns>
        public static string BuildWebsiteNavigation(string[] Names, string[] Urls = null)
        {
            var sb = new StringBuilder();
            if (Names.Length > 0)
            {
                for (int i = 0; i < Names.Length - 1; i++)
                {
                    sb.Append("<li><a href=\"")
                      .Append(Urls[i])
                      .Append("\">")
                      .Append(Names[i])
                      .Append("</a></li>");
                }
                sb.Append("<li>")
                  .Append(Names[Names.Length - 1])
                  .Append("</li>");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Builds navigation menu by given links and captions
        /// </summary>
        /// <param name="Links">Array of links</param>
        /// <param name="Captions">Array of captions</param>
        /// <returns>Navigation menu HTML</returns>
        public static string GetNavigationMenu(List<string> Captions, List<string> Links)
        {
            StringBuilder sb = new StringBuilder();
            int length = Links.Count;

            for (int i = 0; i < length; i++)
            {
                sb.AppendFormat("<a {0} >{1}</a> ➛ ", (string.IsNullOrEmpty(Links[i]) ? string.Empty : "href=\"" + Links[i] + "\""), Captions[i]);
            }
            if (Captions.Count > 0)
            {
                sb.Append(Captions[length]);
            }
            return sb.ToString();
        }
    }
}