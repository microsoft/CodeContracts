// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Xml.Linq;

/*
 * You can configure the list of sources and projects in the file
 * 
 * like this one:
<Config>
  <RootPath>d:\</RootPath>
	<Sources>
		<Source Name="vstfcodebox/Psi" Type="TFS">
			<Config>
				<protocol>http</protocol>
				<server>vstfcodebox</server>
				<port>8080</port>
				<path>tfs/Psi</path>
				<subpath>cci</subpath>
			</Config>
			<Projects>
				<Project Name="cci" Path="cci.sln" />
			</Projects>
		</Source>
		<Source Name="Facebook C# SDK" Type="HG">
			<Config>
				<protocol>https</protocol>
				<server>hg01.codeplex.com</server>
				<path>facebooksdk</path>
			</Config>
			<Projects>
				<Project Name="Facebook-Net40" Path="Source\Facebook-Net40.sln" />
			</Projects>
		</Source>
	</Sources>
</Config>
*/

namespace CCVersions
{
  class Config
  {
    readonly string FileName;
    readonly XDocument XDoc;
    readonly XElement XRoot;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(!String.IsNullOrWhiteSpace(this.FileName));
      Contract.Invariant(this.XDoc != null);
    }

    public Config(string fileName)
    {
      Contract.Requires(!String.IsNullOrWhiteSpace(fileName));

      this.FileName = fileName;

      if (File.Exists(FileName))
        this.XDoc = XDocument.Load(this.FileName, LoadOptions.PreserveWhitespace);
      else
        this.XDoc = new XDocument(new XElement("Config"));

      this.XRoot = this.XDoc.Element("Config");
    }

    public XElement XConfig { get { return XRoot; } }

    public void Save()
    {
      this.XDoc.Save(this.FileName);
    }
  }
}
