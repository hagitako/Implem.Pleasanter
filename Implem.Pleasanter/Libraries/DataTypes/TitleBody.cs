﻿using Implem.Pleasanter.Interfaces;
using Implem.Pleasanter.Libraries.Settings;
using Implem.Pleasanter.Libraries.ViewParts;
namespace Implem.Pleasanter.Libraries.DataTypes
{
    public class TitleBody : Title, IConvertable
    {
        public string Body = string.Empty;

        public TitleBody()
        {
        }

        public TitleBody(long id, string title, string displayValue, string body)
        {
            Id = id;
            Value = title;
            DisplayValue = displayValue;
            Body = body;
        }

        public override string ToString()
        {
            return Value + "\r\n" + Body;
        }

        public override HtmlBuilder Td(HtmlBuilder hb, Column column)
        {
            return hb.Td(action: () => hb
                .Div(css: "grid-title-body", action: () => hb
                    .P(css: "title", action: () => TdTitle(hb, column))
                    .P(css: "body markup", action: () => hb
                         .Text(text: Body))));
        }

        public override string ToExport(Column column)
        {
            return ToString();
        }
    }
}