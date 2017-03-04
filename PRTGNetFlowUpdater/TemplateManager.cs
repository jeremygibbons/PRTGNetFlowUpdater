// <copyright file="TemplateManager.cs" company="None">
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the 
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//
// Copyright (c) 2017 Jeremy Gibbons. All rights reserved
// </copyright>

namespace PRTGNetFlowUpdater
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    class TemplateManager
    {
        List<RuleTemplate> templates = new List<RuleTemplate>();

        enum TemplateLoadResult
        {
            Success = 0,
            FileDoesNotExist = -1,
            EmptyFileName = -2
        }

        public TemplateManager()
        {

        }

        private TemplateLoadResult LoadXMLTemplateFile(string filename)
        {
            if (filename.Equals(string.Empty))
            {
                return TemplateLoadResult.EmptyFileName;
            }

            if (!System.IO.File.Exists(filename))
            {
                return TemplateLoadResult.FileDoesNotExist;
            }

            XDocument xdoc = XDocument.Load(filename);

            foreach (XElement xel in xdoc.Root.Element("templates").Elements("template"))
            {
                RuleTemplate rt = this.ParseTemplate(xel);
                if(rt != null)
                {
                    templates.Add(rt);
                }
            }

            return TemplateLoadResult.Success;
        }

        private RuleTemplate ParseTemplate(XElement templElt)
        {
            RuleTemplate rt = new RuleTemplate();

            rt.TemplateName = (string)templElt.Attribute("name");
            rt.AppName = (string)templElt.Element("ruleName");
            rt.AppRule = (string)templElt.Element("rule");

            return rt;
        }
    }
}
