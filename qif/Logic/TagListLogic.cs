using QifApi.Transactions;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using QifApi.Config;

namespace QifApi.Logic
{
    internal static class TagListLogic
    {
        public static List<TagListTransaction> Import(string transactionItems, Configuration config)
        {
            List<TagListTransaction> tagListTransactionList = new List<TagListTransaction>();
            TagListTransaction tagListTransaction = new TagListTransaction();
            foreach (string str1 in Regex.Split(transactionItems, "$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace))
            {
                string str2 = str1.Replace("\r", "").Replace("\n", "");
                if (str2.Length > 0)
                {
                    switch (str2[0].ToString())
                    {
                        case "N":
                            tagListTransaction.TagName = str2.Substring(1);
                            break;
                        case "D":
                            tagListTransaction.Description = str2.Substring(1);
                            break;
                        case "^":
                            tagListTransactionList.Add(tagListTransaction);
                            tagListTransaction = new TagListTransaction();
                            break;
                    }
                }
            }
            return tagListTransactionList;
        }

        internal static void Export(StreamWriter writer, List<TagListTransaction> list, Configuration config)
        {
            if (list == null || list.Count <= 0)
                return;
            writer.WriteLine("!Type:Tag");
            foreach (TagListTransaction tagListTransaction in list)
            {
                if (!string.IsNullOrEmpty(tagListTransaction.TagName))
                    writer.WriteLine("N" + tagListTransaction.TagName);
                if (!string.IsNullOrEmpty(tagListTransaction.Description))
                    writer.WriteLine("D" + tagListTransaction.Description);
                writer.WriteLine("^");
            }
        }
    }
}
