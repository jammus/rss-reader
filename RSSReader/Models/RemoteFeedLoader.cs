using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace RSSReader.Models
{
    public class RemoteFeedLoader:IFeedLoader
    {
        public XmlDocument LoadXML(string source)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(source);
                return xmlDoc;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
