using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace App
{
    public class ExtendedXmlConfigurationProvider : XmlConfigurationProvider
    {
        public ExtendedXmlConfigurationProvider(XmlConfigurationSource source) :
            base(source)
        { }

        public override void Load(Stream stream)
        {
            //加载源文件并创建一个XmlDocument        
            var sourceDoc = new XmlDocument();
            sourceDoc.Load(stream);

            //添加索引
            AddIndexes(sourceDoc.DocumentElement);

            //根据添加的索引创建一个新的XmlDocument
            var newDoc = new XmlDocument();
            var documentElement =
                newDoc.CreateElement(sourceDoc.DocumentElement.Name);
            newDoc.AppendChild(documentElement);

            foreach (XmlElement element in sourceDoc.DocumentElement.ChildNodes)
            {
                Rebuild(element, documentElement,
                    name => newDoc.CreateElement(name));
            }

            //根据新的XmlDocument初始化配置字典
            using (Stream newStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(newStream))
                {
                    newDoc.WriteTo(writer);
                }
                newStream.Position = 0;
                base.Load(newStream);
            }
        }

        private void AddIndexes(XmlElement element)
        {
            if (element.ChildNodes.OfType<XmlElement>().Count() > 1)
            {
                if (element.ChildNodes.OfType<XmlElement>()
                    .GroupBy(it => it.Name).Count() == 1)
                {
                    var index = 0;
                    foreach (XmlElement subElement in element.ChildNodes)
                    {
                        subElement.SetAttribute("append_index", (index++).ToString());
                        AddIndexes(subElement);
                    }
                }
            }
        }

        private void Rebuild(XmlElement source, XmlElement destParent,
            Func<string, XmlElement> creator)
        {
            var index = source.GetAttribute("append_index");
            var elementName = string.IsNullOrEmpty(index) ? source.Name : $"{source.Name}_index_{index}";
            var element = creator(elementName);
            destParent.AppendChild(element);
            foreach (XmlAttribute attribute in source.Attributes)
            {
                if (attribute.Name != "append_index")
                {
                    element.SetAttribute(attribute.Name, attribute.Value);
                }
            }

            foreach (XmlElement subElement in source.ChildNodes)
            {
                Rebuild(subElement, element, creator);
            }
        }
    }

}
