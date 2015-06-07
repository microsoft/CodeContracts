using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UpdateManifestVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            string version = args[0];
            string filePath;

            if (args[1].StartsWith("VSIX"))
            {
                filePath = Path.GetFullPath(args[1]);

                XDocument doc = XDocument.Load(filePath);
                if (Path.GetFileName(filePath).StartsWith("VS10"))
                {
                    doc.Element(XName.Get("Vsix", "http://schemas.microsoft.com/developer/vsx-schema/2010")).Element(XName.Get("Identifier", "http://schemas.microsoft.com/developer/vsx-schema/2010")).Element(XName.Get("Version", "http://schemas.microsoft.com/developer/vsx-schema/2010")).Value = version;
                }
                else
                {
                    doc.Element(XName.Get("PackageManifest", "http://schemas.microsoft.com/developer/vsx-schema/2011")).Element(XName.Get("Metadata", "http://schemas.microsoft.com/developer/vsx-schema/2011")).Element(XName.Get("Identity", "http://schemas.microsoft.com/developer/vsx-schema/2011")).Attribute("Version").Value = version;
                }

                doc.Save(filePath);
            }
            else
            {
                filePath = Path.GetFullPath(Path.Combine(@"..\ContractAdornments", args[1]));
                XDocument doc = XDocument.Load(filePath);
                if (args[1].StartsWith("VS2010"))
                {
                    doc.Element(XName.Get("Vsix", "http://schemas.microsoft.com/developer/vsx-schema/2010")).Element(XName.Get("Identifier", "http://schemas.microsoft.com/developer/vsx-schema/2010")).Element(XName.Get("Version", "http://schemas.microsoft.com/developer/vsx-schema/2010")).Value = version;
                }
                else
                {
                    doc.Element(XName.Get("PackageManifest", "http://schemas.microsoft.com/developer/vsx-schema/2011")).Element(XName.Get("Metadata", "http://schemas.microsoft.com/developer/vsx-schema/2011")).Element(XName.Get("Identity", "http://schemas.microsoft.com/developer/vsx-schema/2011")).Attribute("Version").Value = version;
                }

                doc.Save(filePath);
            }
        }
    }
}
