using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace RSSReader.Models
{
    public interface IFeedLoader
    {
        XmlDocument LoadXML(string source);
    }
}
