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

    public class TemplateManager
    {
        public delegate void TemplateEventHandler(RuleTemplate rt);
        public event TemplateEventHandler OnAddTemplate;
        public event TemplateEventHandler OnRemoveTemplate;
        public event TemplateEventHandler OnModifyTemplate;

        private readonly Dictionary<string, RuleTemplate> templates = new Dictionary<string, RuleTemplate>();

        public string TemplateFileName { get; set; }

        public enum TemplateLoadResult
        {
            Success = 0,
            FileDoesNotExist = -1,
            EmptyFileName = -2
        }

        public IEnumerable<RuleTemplate> Templates
        {
            get
            {
                return this.templates.Values;
            }
        }

        public enum TemplateModResult
        {
            TemplateAddSuccess = 0,
            TemplateModSuccess = 1,
            TemplateDelSuccess = 2,
            TemplateIncomplete = -1,
            TemplateNameExists = -2,
            TemplateNameDoesNotExist = -3,
            TemplateNameInvalid = -4
        }

        public TemplateManager(string file)
        {
            this.TemplateFileName = file;
        }

        public TemplateLoadResult LoadXMLTemplateFile()
        {
            if (this.TemplateFileName.Equals(string.Empty))
            {
                return TemplateLoadResult.EmptyFileName;
            }

            if (!System.IO.File.Exists(this.TemplateFileName))
            {
                return TemplateLoadResult.FileDoesNotExist;
            }

            XDocument xdoc = XDocument.Load(this.TemplateFileName);

            foreach (XElement xel in xdoc.Root.Elements("template"))
            {
                RuleTemplate rt = this.ParseTemplate(xel);
                if(rt != null)
                {
                    templates.Add(rt.TemplateName, rt);
                }
            }

            return TemplateLoadResult.Success;
        }

        private RuleTemplate ParseTemplate(XElement templElt)
        {
            RuleTemplate rt = new RuleTemplate(
                templateName: (string)templElt.Attribute("name"),
                appName: (string)templElt.Element("ruleName"),
                appRule: (string)templElt.Element("rule")
                );

            return rt;
        }

        public RuleTemplate GetRule(string ruleName)
        {
            if (string.IsNullOrEmpty(ruleName))
            {
                return null;
            }

            if (!templates.ContainsKey(ruleName))
            {
                return null;
            }

            return templates[ruleName];
        }

        public TemplateModResult AddRule(RuleTemplate rule)
        {
            if(rule.TemplateName.Equals(string.Empty)
                || rule.AppName.Equals(string.Empty)
                || rule.AppRule.Equals(string.Empty))
            {
                return TemplateModResult.TemplateIncomplete;
            }

            if(templates.ContainsKey(rule.TemplateName))
            {
                return TemplateModResult.TemplateNameExists;
            }

            templates.Add(rule.TemplateName, rule);

            return TemplateModResult.TemplateAddSuccess;
        }

        public TemplateModResult DeleteRule(string templateName)
        {
            if(string.IsNullOrEmpty(templateName))
            {
                return TemplateModResult.TemplateNameInvalid;
            }

            if(!templates.ContainsKey(templateName))
            {
                return TemplateModResult.TemplateNameDoesNotExist;
            }

            templates.Remove(templateName);

            return TemplateModResult.TemplateDelSuccess;
        }

        public TemplateModResult ModifyRule(RuleTemplate rt)
        {
            if (rt.TemplateName.Equals(string.Empty))
            {
                return TemplateModResult.TemplateNameInvalid;
            }

            if (rt.AppName.Equals(string.Empty)
                || rt.AppRule.Equals(string.Empty))
            {
                return TemplateModResult.TemplateIncomplete;
            }

            templates[rt.TemplateName] = rt;

            return TemplateModResult.TemplateModSuccess;
        }
    }
}
