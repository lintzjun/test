using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using XML_Analysis.Models;
using XML_Analysis.repositories;

namespace XML_Analysis.Service
{
    class ImportService
    {



        public List<OpenData> findOpenData()
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

        public void ImportToDb(List<OpenData> openDatas)
        {
            repository DBoperation = new repository();
            var SqlConn = DBoperation.Connection();
            openDatas.ForEach(new_items =>
            {
                DBoperation.Insert_Data(SqlConn, new_items);
            });

        }

        public List<OpenData> Find_Data_From_Db(string name)
        {
            repository DBoperation = new repository();
            var SqlConn = DBoperation.Connection();
            return DBoperation.Select_All_Data(SqlConn, name);

        }

        private static string getValue(XElement Ebhsdata, string propertyName)
        {
            return Ebhsdata.Element(propertyName)?.Value?.Trim();
        }
    }
}