﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codecepticon.Modules.CSharp.Profiles.Certify.Rewriters
{
    class CommandLine : CSharpSyntaxRewriter
    {
        protected SyntaxTreeHelper Helper = new SyntaxTreeHelper();

        public override SyntaxNode VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            string text = node.GetFirstToken().ValueText.Trim();
            if (text.Length > 0 && text[0] == '/')
            {
                return Helper.RewriteCommandLineArg(node, text, true);
            }

            if (Helper.GetPropertyDeclaration(node) == "CommandName")
            {
                return Helper.RewriteCommandLineArg(node, text, false);
            }

            return base.VisitLiteralExpression(node);
        }
    }
}
