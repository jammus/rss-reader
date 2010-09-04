using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RSSReader.Tests.Fakes
{
    class FakeXMLFeed
    {
        public static XmlDocument GetFakeXMLFeed(string feedName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"../../../RSSReader.Tests/TestData/" + feedName + ".xml");
            return xmlDoc;
        }
    }
}
