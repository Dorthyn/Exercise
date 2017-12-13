using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace addEchoToTarget
{
    class Program
    {
        static void Main(string[] args)
        {
            TargetWrite(@"D:\dita-ot-2.5.4\build.xml");
        }

        public static void TargetWrite(string xmlFullPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //XmlReaderSettings settings = new XmlReaderSettings();
            //settings.IgnoreComments = true;//忽略文档里面的注释
            //XmlReader reader = XmlReader.Create(xmlFullPath, settings);
            xmlDoc.Load(xmlFullPath);

            XmlNodeList targetList = xmlDoc.SelectNodes("//target");//查找<images>
            

            //获取当前文件的完整位置以及当前target的name属性
            foreach (var target in targetList)
            {
                XmlElement targetElement = (XmlElement)target;
                XmlElement xe1 = xmlDoc.CreateElement("echo");//创建一个<thumb>节点
                string targetName = targetElement.GetAttribute("name");
                xe1.InnerText = "执行" + xmlFullPath +"的"+ targetName;

                targetElement.AppendChild(xe1);
            }
            //reader.Close();
            xmlDoc.Save(xmlFullPath);
            

        }
    }
}
