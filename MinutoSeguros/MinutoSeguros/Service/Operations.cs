using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MinutosSeguros.Model;

namespace MinutosSeguros.Service
{
    public static class Operations
    {
        public static List<string> ReturnDataAnalys(string pageContent)
        {
            var re = new Regex(@"(?ms)<item>(.*?)</item>");
            var listItens = new List<string>();

            foreach (Match match in re.Matches(pageContent))
            {
                listItens.Add(match.ToString());
            }

            return listItens;
        }

        public static string ReturnFullTextTopic(string textItem)
        {
            string text = string.Empty;
            var re = new Regex(@"(?ms)<content:encoded>(.*?)</content:encoded>");
            var match = re.Match(textItem);

            if (match.Success)
                text = re.Match(textItem).Groups[0].ToString();

            re = new Regex(@"(?ms)<!\[CDATA\[(.*?)]]>");
            text = re.Match(text).Groups[1].ToString();

            re = new Regex(@"(?i)(<[^>]*?>|No related posts|&nbsp;|\(foto: divulgação\)" +
                           @"|\ba\b|\bante\b|\baté\b|\bapós\b|\bcom\b|\bcontra\b|\bde\b" +
                           @"|\bdesde\b|\bem\b|\bentre\b|\bpara\b|\bper\b|\bperante\b" +
                           @"|\bpor\b|\bsem\b|\bsob\b|\bsobre\b|\btrás\b|\bo\b|\ba\b" +
                           @"|\bos\b|\basb\|\bum\b|\buma\b|\buns\b|\bumas\b|\be\b|\bum\b" +
                           @"|\bdos\b|\bé\b|\bque\b|\bnão\b|\bsão\b|\bmais\b|\bna\b|\bdo\b" +
                           @"|\besse\b|\bno\b|\bda\b|\bse\b|\bseu\b|\bou\b|\bas\b|\bdas\b|\bcom\b" +
                           @"|\bcomo\b|\bser\b|\bsua|\b\d+?\b|\bessa\b)");

            text = re.Replace(text, "");
            return text;

        }

        public static List<WordTopic> ReturnTopTenWords(string text)
        {
            var regex = new Regex(@"\b(\w+?)\b");

            var listWord = (from Match match in regex.Matches(text) select match.Groups[1].ToString()).ToList();
            var listReturn = new List<WordTopic>();

            foreach (var grupo in listWord.GroupBy(x => x))
            {
                var w = new WordTopic();
                w.word = grupo.Key;
                w.ocurrenc = grupo.Count(
                    );

                listReturn.Add(w);
            }


            return listReturn.OrderByDescending(x => x.ocurrenc).Take(10).ToList();
        }


        public static int ReturnQtdWordTopic(string textTopic)
        {
            var regex = new Regex(@"\b(\w+?)\b");
            return regex.Matches(textTopic).Count;
        }

        public static object GetTitleTopic(string item)
        {
            var regex = new Regex(@"<title>([^<]*?)</title>");
            return regex.Match(item).Groups[1].ToString();

        }
    }

}
