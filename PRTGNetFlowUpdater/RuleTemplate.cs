// <copyright file="RuleTemplate.cs" company="None">
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
    using System.Text.RegularExpressions;

    public class RuleTemplate
    {
        public string TemplateName { get; set; }
        public string AppName { get; set; }
        public string AppRule { get; set; }

        private static Regex SingleWord = new Regex(@"^[a-zA-Z0-9\-_]+$");

        public RuleTemplate(string templateName, string appName, string appRule)
        {
            this.TemplateName = templateName;
            this.AppName = appName;
            this.AppRule = appRule;
        }

        public bool IsValidRule()
        {
            

            if (string.IsNullOrWhiteSpace(TemplateName)
                || string.IsNullOrWhiteSpace(AppName)
                || string.IsNullOrWhiteSpace(AppRule)
                || !SingleWord.IsMatch(this.AppName))
            {
                return false;
            }

            return true;
        }

        public string GetError()
        {
            if (string.IsNullOrWhiteSpace(TemplateName))
            {
                return "Template name cannot be empty!";
            }

            if (string.IsNullOrWhiteSpace(AppName))
            {
                return "Application name cannot be empty!";
            }

            if (string.IsNullOrWhiteSpace(AppRule))
            {
                return "Rule cannot be empty";
            }

            if (!SingleWord.IsMatch(this.AppName))
            {
                return "Application name must be a single word!";
            }

            return string.Empty;
        }
    }
}
