using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace _20181005_opendata
{
    class Program
    {
        static void Main(string[] args)
        {
            var nodes = findOpenDatas();
            ShowOpenData(nodes);
            Console.ReadKey();
        }

        //12121212


        static List<OpenData> findOpenDatas()
        {
            List<OpenData> result = new List<OpenData>();
            var xml = XElement.Load(@"https://apiservice.mol.gov.tw/OdService/download/A17000000J-020047-jb6");
            var nodes = xml.Descendants("row").ToList();

            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                OpenData item = new OpenData();

                item.工會名稱 = getValue(node, "工會名稱");
                item.地址 = getValue(node, "地址");
                item.電話 = getValue(node, "電話");
                item.傳真 = getValue(node, "傳真");

                result.Add(item);
            }
            return result;
        }

        private static string getValue(XElement node, string propertyName)
        {
            return node.Element(propertyName)?.Value?.Trim();
        }

        public static void ShowOpenData(List<OpenData> nodes)
        {
            Console.WriteLine(string.Format("共收到{0}筆資料", nodes.Count));
            nodes.GroupBy(node => node.工會名稱).ToList()
                .ForEach(group =>
                {
                    var key = group.Key;
                    var groupDatas = group.ToList();
                    var message = $"工會名稱:{key},共有{groupDatas.Count()}筆資料";
                    Console.WriteLine(message);
                });
        }


    }
}