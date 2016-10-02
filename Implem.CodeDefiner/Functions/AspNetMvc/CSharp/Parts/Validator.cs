﻿using Implem.CodeDefiner.Utilities;
using Implem.DefinitionAccessor;
using Implem.Libraries.Utilities;
using System.Collections.Generic;
using System.Linq;
namespace Implem.CodeDefiner.Functions.AspNetMvc.CSharp.Parts
{
    internal class Validator
    {
        internal static void SetCodeCollection(
            CodeDefinition codeDefinition,
            List<string> codeCollection,
            DataContainer dataContainer)
        {
            var code = string.Empty;
            Def.ColumnDefinitionCollection
                .Where(o => o.TableName == dataContainer.TableName)
                .Where(o => o.FormName == dataContainer.FormName)
                .Where(o => o.Validators != string.Empty)
                .ForEach(columnDefinition =>
            {
                Creators.SetCodeCollection(
                    ref code,
                    codeCollection,
                    codeDefinition,
                    dataContainer,
                    () => ReplaceCode(ref code, columnDefinition));
            });
        }

        private static void ReplaceCode(ref string code, ColumnDefinition columnDefinition)
        {
            foreach (var placeholder in code.RegexValues(CodePatterns.ReplacementPlaceholder))
            {
                switch (placeholder)
                {
                    case "ColumnName":
                        code = code.Replace(
                            "#ColumnName#", columnDefinition.ColumnName.PublicVariableName());
                        break;
                    case "Validators":
                        code = code.Replace(
                            "#Validators#", columnDefinition.Validators);
                        break;
                    case "ValidatorMessages":
                        code = code.Replace(
                            "#ValidatorMessages#", ValidatorMessages(columnDefinition));
                        break;
                    default:
                        break;
                }
            }
        }

        private static string ValidatorMessages(ColumnDefinition columnDefinition)
        {
            return columnDefinition.Validators
                .Split(',')
                .Select(o => o.Split(':')._1st())
                .Select(o => o + ":$p.display('" + ValidatorMessage(o) + "')")
                .Join(",");
        }

        private static string ValidatorMessage(string type)
        {
            switch (type)
            {
                case "required": return "ValidateRequired";
                case "c_num": return "ValidateNumber";
                case "date": return "ValidateDate";
                case "email": return "ValidateMail";
                case "equalTo": return "ValidateEqualTo";
                default: return null;
            }
        }
    }
}
