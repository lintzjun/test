using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using XML_Analysis.Models;
using XML_Analysis.repositories;
using XML_Analysis.Service;

namespace XML_Analysis
{
    class Program
    {
        static void Main(string[] args)
        {
            //var Ebhsdata_Count = findOpenData();
            //repository DBoperation = new repository();
            // var SqlConn = DBoperation.Connection();
            //ImportService ImportToDb
            ImportService importservice = new ImportService();
            string name = "工會名稱";
            var nodes = importservice.Find_Data_From_Db(name);

            ShowOpenData(nodes);


            Console.ReadKey();
        }

        static List<OpenData> findOpenData()
        {
            List<OpenData> result = new List<OpenData>();

            var xml = XElement.Load(@"C:\Users\linja\Source\Repos\test\ConsoleApp1\ConsoleApp1\App_Data\datagovtw_dataset_20181009.xml");

            var Ebhsdata_count = xml.Descendants("row").ToList();

            Ebhsdata_count.ToList()
                .ForEach(Ebhsdata =>
                {
                    OpenData item = new OpenData();
                    item.工會名稱 = Ebhsdata.Descendants("Col1").FirstOrDefault().Value;
                    item.地址 = Ebhsdata.Descendants("Col2").FirstOrDefault().Value;
                    item.電話 = Ebhsdata.Descendants("Col3").FirstOrDefault().Value;
                    item.傳真 = Ebhsdata.Descendants("Col4").FirstOrDefault().Value;
                    result.Add(item);

                });

            return result;
        }

        private static string getValue(XElement Ebhsdata, string propertyName)
        {
            return Ebhsdata.Element(propertyName)?.Value?.Trim();
        }

        public static void ShowOpenData(List<OpenData> Ebhsdata_count)
        {
            Console.WriteLine(string.Format("共收到{0}筆的資料 ", Ebhsdata_count.Count));
            Ebhsdata_count.GroupBy(Ebhsdata => Ebhsdata.工會名稱).ToList()
                .ForEach(group =>
                {
                    var key = group.Key;
                    var groupDatas = group.ToList();
                    var message = $"工會名稱:{key},共有{groupDatas.Count()}筆資料";
                    Console.WriteLine(message);
                }
                );
        }

    }
}