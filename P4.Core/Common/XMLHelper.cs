using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;

namespace P4.Common
{
    public class XMLHelper
    {
        private object lockObj = new object();

        private string path = HttpContext.Current.Server.MapPath(@"..\Localization\P4\");

        /// <summary>
        /// 更新P4.xml
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="val"></param>
        public void UpdateP4NodeEN(string nodeName, string val)
        {
            lock (lockObj)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path+ "P4.xml");
                var root = xmlDoc.SelectSingleNode("localizationDictionary");
                var texts = root.SelectSingleNode("texts");
                XmlNodeList nodeList = texts.ChildNodes;//texts节点
                XmlNode currentNode = null;
                foreach(XmlNode node in nodeList)
                {
                    if(node.Attributes==null|| node.Attributes.Count == 0)
                    {
                        continue;
                    }
                    var xmlName = node.Attributes["name"];
                    if (xmlName.Value == nodeName)
                    {
                        currentNode = node;
                        break;
                    }
                }
                if (currentNode == null)
                {
                    XmlElement insertNode = xmlDoc.CreateElement("text");
                    insertNode.SetAttribute("name", nodeName);
                    insertNode.SetAttribute("value", val);
                    texts.AppendChild(insertNode);
                }
                else
                {
                    currentNode.Attributes["value"].Value = val;
                }
                xmlDoc.Save(path+ "P4.xml");
            }
        }


        /// <summary>
        /// 更新P4-zh-CN.xml
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="val"></param>
        public void UpdateP4NodeZh(string nodeName, string val)
        {
            lock (lockObj)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path + "P4-zh-CN.xml");
                var root = xmlDoc.SelectSingleNode("localizationDictionary");
                var texts = root.SelectSingleNode("texts");
                XmlNodeList nodeList = texts.ChildNodes;//texts节点
                XmlNode currentNode = null;
                foreach (XmlNode node in nodeList)
                {
                    if (node.Attributes == null || node.Attributes.Count == 0)
                    {
                        continue;
                    }
                    var xmlName = node.Attributes["name"];
                    if (xmlName.Value == nodeName)
                    {
                        currentNode = node;
                        break;
                    }
                }
                if (currentNode == null)
                {
                    XmlElement insertNode = xmlDoc.CreateElement("text");
                    insertNode.SetAttribute("name", nodeName);
                    insertNode.SetAttribute("value", val);
                    texts.AppendChild(insertNode);
                }
                else
                {
                    currentNode.Attributes["value"].Value = val;
                }
                xmlDoc.Save(path + "P4-zh-CN.xml");
            }
        }
    }
}
